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

        public override async Task OnKeyDown(StreamDeckEventPayload args)
        {
            // Clear the stored Tarkov Time.
            SettingsModel.JsonData = "";
            // Update the PluginTitle with the empty string
            await Manager.SetTitleAsync(args.context, SettingsModel.JsonData.ToString());
            // Save the empty Tarkov Time string.
            await Manager.SetSettingsAsync(args.context, SettingsModel.JsonData.ToString());
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await UpdateTTAsync(args);
        }

        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            await base.OnDidReceiveSettings(args);
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            // Start timer her måske??
            await Run(args, TimeSpan.FromSeconds(7), CancellationToken.None);
        }

        public async Task UpdateTTAsync(StreamDeckEventPayload args)
        {
            // Fetch the current Tarkov Times
            var TTleft = tt.RealTimeToTarkovTime(true);
            var TTright = tt.RealTimeToTarkovTime(false);
            // Combine the two the current Tarkov Times and store it in SettingsModel
            SettingsModel.JsonData = TTleft + "\n" + TTright;
            // Update the PluginTitle with the current Tarkov Times
            await Manager.SetTitleAsync(args.context, SettingsModel.JsonData.ToString());
            // Save the updated TArkov Times
            await Manager.SetSettingsAsync(args.context, SettingsModel.JsonData.ToString());
        }

        public async Task Run(StreamDeckEventPayload args, TimeSpan period, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(period, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                    await UpdateTTAsync(args);
            }
        }
    }
}