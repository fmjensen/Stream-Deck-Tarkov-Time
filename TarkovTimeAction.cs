using System;
using StreamDeckLib;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using StreamDeckLib.Messages;
using System.Threading.Tasks;

namespace TarkovTime
{
    [ActionUuid(Uuid = "com.cmdrflemming.tarkovtime.action")]
    public class TarkovTimeAction : BaseStreamDeckActionWithSettingsModel<Models.TarkovTimeSettingsModel>
    {
        readonly TarkovTime tt = new();

        /**This event triggers when an instance of an action is displayed on Stream Deck, for example, 
         * when the hardware is first plugged in or when a folder containing that action is entered, 
         * the plugin will receive a willAppear event. It also triggers when an action is dragged 
         * from the Action list to the canvas.
         */
        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            await StartTimer(args, TimeSpan.FromSeconds(7), CancellationToken.None);
        }

        /**When the user presses a key, the plugin will receive the keyDown event.
         */
        public override async Task OnKeyDown(StreamDeckEventPayload args)
        {
            // Clear the stored Tarkov Time.
            SettingsModel.SavedTarkovTime = "";
            // Update the PluginTitle with the empty string
            await Manager.SetTitleAsync(args.context, SettingsModel.SavedTarkovTime.ToString());
            // Save the empty Tarkov Time string.
            await Manager.SetSettingsAsync(args.context, SettingsModel);
        }

        /** When the user releases a key, the plugin will receive the keyUp event as a JSON structure
         */
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await StartTimer(args, TimeSpan.FromSeconds(7), CancellationToken.None);
        }

        /**The didReceiveSettings event is received after calling the getSettings API to 
         * retrieve the persistent data stored for the action. 
         */
        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            await base.OnDidReceiveSettings(args);
            await StartTimer(args, TimeSpan.FromSeconds(7), CancellationToken.None);
        }

        // Tarkov Time calculation and timer for auto updateing the button follows
        public async Task StartTimer(StreamDeckEventPayload args, TimeSpan period, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await UpdateTTAsync(args);
                }

                await Task.Delay(period, cancellationToken);
            }
        }
        public async Task UpdateTTAsync(StreamDeckEventPayload args)
        {
            // Fetch the current Tarkov Times
            var TTleft = tt.RealTimeToTarkovTime(true);
            var TTright = tt.RealTimeToTarkovTime(false);
            // Combine the two the current Tarkov Times and store it in SettingsModel
            SettingsModel.SavedTarkovTime = TTleft + "\n\n" + TTright;
            // Update the PluginTitle with the current Tarkov Times
            await Manager.SetTitleAsync(args.context, SettingsModel.SavedTarkovTime.ToString());
            // Save the updated Tarkov Time persistent.
            await Manager.SetSettingsAsync(args.context, SettingsModel);
        }
    }
}