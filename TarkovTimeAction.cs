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
        public CancellationTokenSource _cancelSource;
        public CancellationToken _token;
        public int delayInSeconds = 7;
        public bool debug = false; // Set this to true to get logging in the file at %AppData%\Roaming\Elgato\StreamDeck\logs

        /**This event triggers when an instance of an action is displayed on Stream Deck, for example, 
         * when the hardware is first plugged in or when a folder containing that action is entered, 
         * the plugin will receive a willAppear event. It also triggers when an action is dragged 
         * from the Action list to the canvas.
         */
        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            if (debug) await WriteToLog("OnWillAppear triggered.");
            await base.OnWillAppear(args);
            _cancelSource = new CancellationTokenSource();
            await StartTimer(args, TimeSpan.FromSeconds(delayInSeconds), _token = _cancelSource.Token);
        }

        /**When the user presses a key, the plugin will receive the keyDown event.
         */
        public override async Task OnKeyDown(StreamDeckEventPayload args)
        {
            if (debug) await WriteToLog("OnKeyDown triggered.");
            // Clear the stored Tarkov Time.
            SettingsModel.SavedTarkovTime = "";
            // Update the PluginTitle with the empty string
            await Manager.SetTitleAsync(args.context, SettingsModel.SavedTarkovTime.ToString());
            // Cancel the running timer
            _cancelSource.Cancel();
        }

        /** When the user releases a key, the plugin will receive the keyUp event as a JSON structure
         */
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            if (debug) await WriteToLog("OnKeyUp triggered.");
            _cancelSource = new CancellationTokenSource();
            await StartTimer(args, TimeSpan.FromSeconds(delayInSeconds), _token = _cancelSource.Token);
        }

        /**The didReceiveSettings event is received after calling the getSettings API to 
         * retrieve the persistent data stored for the action. 
         */
        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            if (debug) await WriteToLog("OnDidReceiveSettings triggered.");
            await base.OnDidReceiveSettings(args);
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

            if (debug) await WriteToLog(SettingsModel.SavedTarkovTime.ToString());
        }
        public async Task WriteToLog(string message)
        {
            await Manager.LogMessageAsync(" SDTT ", message);
        }
    }
}