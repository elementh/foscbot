using Incremental.Common.Random;

namespace FOSCBot.Core.Common;

public static class QuestionsInteractiveResponseData
{
    public static List<string> ChillResponses = new()
    {
        "Que pasa maquina, fiera mastodonte torbellino",
        "Dime mozo",
        "Alguien ha dicho mi nombre?",
        "WHOMS'N'T'VE'D DARED TO MENTION MY NAME",
        "Sup",
        "Os estoy leyendo, si"
    };

    public static List<string> DramaticResponses = new()
    {
        "Oh good heavens, what now",
        "QUE",
        "Ahora qué?",
        "Acho qué",
        "CAACAgQAAxkBAAEDVMthmadn3gdrWzNbLnS2PBiZczwAAZAAAj4IAALztvBRfOVJPfUEHSwiBA" //Cat tired
    };

    public static List<string> OutOfMindResponses = new()
    {
        "Bro, ya no?",
        "Este grupo ha sido proclamado por el bot",
        "Acho que pesaos",
        "Acho para yaaaaREEEEEEEEEEEEE",
        "CAACAgQAAxkBAAEDJMthdZQwLAIyUcECwynw-TuPe_87fAACUgMAApjnowABWVTvcB6NosQhBA", // Me aburris tio
        "CAACAgIAAxkBAAEDVMdhmacf4Ek0lDgK7aWQJKDkmeaW8wACHwwAAr9_OUl5W2p_J1WxryIE", //Captain Whale Keyboard
        "CAACAgQAAxkBAAEDVMlhmadGsqKvkgqTkmyBdWe_T2baIAACOQkAAnqK8VEFmr6NRSkbfSIE", //Cat angry
        "CAACAgQAAxkBAAEDVM1hmaeRRjtyTybHxHfynQXRiW8BHgACsw0AAj_0ZgABimuvHem5f9giBA" //Pepe ree
    };

    public static string GetRandomFromList(this List<string> list)
    {
        return list[RandomProvider.GetThreadRandom()!.Next(0, list.Count)];
    }
}