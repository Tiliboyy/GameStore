using Exiled.API.Features;
using MEC;

namespace GameStore;

public static class Extenstions
{
    public static void GameStoreRewardPlayer(this Player player, Structs.Reward reward)
    {
        GameStoreDatabase.Database.AddRewardToPlayer(player, reward);
    }
    public static void GameStoreMoneyPlayer(this Player player, float money)
    {
        GameStoreDatabase.Database.AddMoneyToPlayer(player, money);
    }
    public static void SendHintWhenNone(this Player player, string message, float duration)
    {
        if (player != null) Timing.RunCoroutine(GameStoreDatabase.HintWaitUntilFalse(player, message, duration));
    }
}