using FOSCBot.Common.Helper;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Uwu;

public class UwuMiscellaneousActionHandler : ActionHandler<UwuMiscellaneousAction>
{
    public UwuMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(UwuMiscellaneousAction action, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);

        return Success();
    }

    public static readonly string[] Stickers =
    {
        "CAACAgEAAxkBAAI5JF59w6XM_AcpKByOoe1DtXyJuAr0AAL4AgACzcclBQ7jFynAjnWwGAQ",
        "CAACAgEAAxkBAAI5JV59w6Vh9gyqRgRr0l0OWEtDLfT8AAL5AgACzcclBeHXi8c4JWp_GAQ",
        "CAACAgEAAxkBAAI5Jl59w6ZP94FPDlRGHkZOHCjduqDTAAL6AgACzcclBVRLfoZ5hPHJGAQ",
        "CAACAgEAAxkBAAI5K159w6dKWVAdIhgC5LBnVpqCsb2AAAIEAwACzcclBbf0eAdlZt5RGAQ",
        "CAACAgEAAxkBAAI5KF59w6YChoJ_RPRv1iSKOuwkilshAAIBAwACzcclBU_zIjPWHIMjGAQ",
        "CAACAgEAAxkBAAI5Kl59w6dnWCFLuEAxsywnFePrepevAAIiAwACzcclBfSPg197ugNkGAQ",
        "CAACAgEAAxkBAAI5J159w6buPfY7zD3NftMEB1AAAeS6YwADAwACzcclBTtAZlst9z6MGAQ",
        "CAACAgEAAxkBAAI5KV59w6cxHS1kuD4leFV2xFrk_4KBAAICAwACzcclBZnYLaPmqi5MGAQ",
        "CAACAgEAAxkBAAI5NF59w-iSDd7hrnYKOxMCXg8anOFHAAIFAwACzcclBX_KTiXzl-z5GAQ",
        "CAACAgEAAxkBAAI5NV59w-gHM75fhv_f20Qpl2_0GL2aAAIHAwACzcclBbd8te3xicWBGAQ",
        "CAACAgEAAxkBAAI5O159w-rX4Bz5FcQb3Pi5_36fHujuAAIdAwACzcclBYKV6alVSIzxGAQ",
        "CAACAgEAAxkBAAI5Nl59w-l5BqcaFXIKYQey7yMi251hAAIIAwACzcclBeLpDMOvSOemGAQ",
        "CAACAgEAAxkBAAI5OF59w-mVj6MnlNBimmdkZKUCnqu2AAIYAwACzcclBSODF1DAPF8FGAQ",
        "CAACAgEAAxkBAAI5N159w-m2sm9Dlu8BAjnwbsnypecpAAIXAwACzcclBfEy6NcNXNUHGAQ",
        "CAACAgEAAxkBAAI5Ol59w-rYoerxasA7GFMGzVifaimdAAIZAwACzcclBQZu-BbTBNFFGAQ",
        "CAACAgEAAxkBAAI5Ql59xCsg5KJQOORSoW_OVMi0j74eAAISAwACzcclBYatnDPp1g04GAQ",
        "CAACAgEAAxkBAAI5Q159xCzDgD8R4GZ58co_3EzRr3EKAAIUAwACzcclBRoFC-uQ5XxiGAQ",
        "CAACAgEAAxkBAAI5RF59xCxGX7uN2IsNwRdoAdxal3gpAAIKAwACzcclBTTqcqkPmpogGAQ",
        "CAACAgEAAxkBAAI5RV59xCzWonWaxzsEYB9FBugO54bjAAIJAwACzcclBSp-FC9DO8KxGAQ",
        "CAACAgEAAxkBAAI5R159xC15eomXH1giKyrFuFPPXXxSAAIVAwACzcclBXdzZl0sao99GAQ",
        "CAACAgEAAxkBAAI5Rl59xCxHiszC4D6vTFi6ql5BbsylAAITAwACzcclBa0f37s5EyJAGAQ",
        "CAACAgEAAxkBAAI5SF59xC3LpyRgPc-6DPMrOOCpILkMAAILAwACzcclBddMPGz3ku2WGAQ",
        "CAACAgEAAxkBAAI5UF59xHUkAvKd_W5v-Y3BVo8JYkyGAAIRAwACzcclBcoUY-7wmpB6GAQ",
        "CAACAgEAAxkBAAI5UV59xHXNu2SU-BKwM6ukuDeEvRFmAAINAwACzcclBRFymXUiMebZGAQ",
        "CAACAgEAAxkBAAI5U159xHZVCQABiodwNRMAAUZunkMhYEIAAhYDAALNxyUFzq8a3qfU03gYBA",
        "CAACAgEAAxkBAAI5Ul59xHVs2hkhetZDJZAOe8xDOg7oAAIPAwACzcclBc0c8lX3rIxrGAQ",
        "CAACAgEAAxkBAAI5WF59xHfCaBzYiKFcdNUQ1YIj6ObGAAIhAwACzcclBXW527t6r8htGAQ",
        "CAACAgEAAxkBAAI5VV59xHb7Tu2UVCo0Eql-CuuKPDWBAAIsAwACzcclBecYYzapF-RbGAQ",
        "CAACAgEAAxkBAAI5Vl59xHeij5VHpeprcl2840Y-2sQBAAIfAwACzcclBTi4rSKMevsgGAQ",
        "CAACAgEAAxkBAAI5VF59xHaB-gErBEkh9DXZaHDe9eLaAAIOAwACzcclBfGcKSkKZF2bGAQ",
        "CAACAgEAAxkBAAI5V159xHeYdoptiC57-xPpRa394tDgAAIgAwACzcclBdluT-36S2ydGAQ",
    };
}