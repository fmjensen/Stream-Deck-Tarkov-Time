using Newtonsoft.Json;
using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Net.Http;
using System.Threading.Tasks;

namespace TarkovTime
{
    [ActionUuid(Uuid = "com.cmdrflemming.tarkovtime.action")]
    public class TarkovTimeAction : BaseStreamDeckActionWithSettingsModel<Models.TarkovTimeSettingsModel>
    {
        public string btnText;
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            using var httpClient = new HttpClient();
            var TarkovTimeJsonString = await httpClient.GetStringAsync("https://tarkov-time.adam.id.au/api");
            /**
             * Example of raw json data we expect to recieve
             * {
             * "left": "03:09:47",
             * "right": "15:09:47"
             * }
             * Now parse with JSON.Net
             */
            var myJsonObject = JsonConvert.DeserializeObject<TarkovTimeJsonType>(TarkovTimeJsonString);

            var leftTime = myJsonObject.left.ToString();
            var rightTime = myJsonObject.right.ToString();

            leftTime = leftTime.Remove(leftTime.Length - 3);
            rightTime = rightTime.Remove(rightTime.Length - 3);

            btnText = leftTime + "\n" + rightTime;

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

    internal class TarkovTimeJsonType
    {
        public string left { get; set; }
        public string right { get; set; }
    }
}