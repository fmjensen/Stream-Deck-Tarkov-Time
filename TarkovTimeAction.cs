using System;
using StreamDeckLib;
using Newtonsoft.Json;
using System.Net.Http;
using StreamDeckLib.Messages;
using System.Threading.Tasks;

namespace TarkovTime
{
    [ActionUuid(Uuid = "com.cmdrflemming.tarkovtime.action")]
    public class TarkovTimeAction : BaseStreamDeckActionWithSettingsModel<Models.TarkovTimeSettingsModel>
    {
        public string btnText;
        TarkovTime tt = new TarkovTime();

        public override async Task OnKeyDown(StreamDeckEventPayload args)
        {
            btnText = "";

            await Manager.SetTitleAsync(args.context, btnText);

            SettingsModel.JsonData = btnText;
            await Manager.SetSettingsAsync(args.context, btnText);
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            var TTleft = tt.RealTimeToTarkovTime(true);
            var TTright = tt.RealTimeToTarkovTime(false);

            btnText = TTleft + "\n" + TTright;

            await Manager.SetTitleAsync(args.context, btnText);

            SettingsModel.JsonData = btnText;
            await Manager.SetSettingsAsync(args.context, btnText);
        }

        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            await base.OnDidReceiveSettings(args);
            await Manager.SetTitleAsync(args.context, SettingsModel.JsonData.ToString());
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            await Manager.SetTitleAsync(args.context, SettingsModel.JsonData.ToString());
            await Manager.SetSettingsAsync(args.context, SettingsModel.JsonData.ToString());
        }
    }
}