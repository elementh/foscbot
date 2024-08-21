using FOSCBot.Infrastructure.Contract.Service;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace FOSCBot.Core.Old.Fallback.ChinesePolice;

public class ChinesePoliceFallbackActionHandler : ActionHandler<ChinesePoliceFallbackAction>
{

    public ChinesePoliceFallbackActionHandler(INavigatorContextAccessor navigatorContextAccessor, IGiphyService giphyService) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(ChinesePoliceFallbackAction action, CancellationToken cancellationToken)
    {
        var message = "`点击打开LINUXCT查看对应内容击打开HONG SPOTIFY应内容应内容TIANANMEN击打开应内容FENSHAO RISG*RE内容应内SIWANG死亡DESCENDANT查看对VUELING" +
            "内容查看WALLPAPER PORT查看对REQIN FUWU热情服务KITERIS内容查FOSC对应内TWITTER DA YONGHU大用户SAFETYNET没有电话MEIYOU DIANHUA卡尔佩SONY XPERIA ZAO TOULE" +
            "糟透HONG NETFLIX阿姆斯特丹AMSTERDAM火鸡TURKEY非常便宜FEICHANG PIANYI很暗HEN AN阴暗的空MARK ASS BROWNIE GALAXY Z FOLD 2 REVIEW里诺克斯蒂VERY DARK美国WANTED" +
            "通缉5星★★★★★`";
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!,message ,
            cancellationToken: cancellationToken);

        return Success();
    }
}