using Exiled.API.Features;
using GameStore.Configs;
using GameStore.Events.EventArgs;
using System;

namespace GameStore.Events;

public static class Handlers
{
    public static Exiled.Events.Features.Event<GainedMoneyEventArgs> GainingMoney = new();
    public static Exiled.Events.Features.Event<BuyingItemsEventArgs> BuyingItems = new();
    

    public static void OnGainingMoney(Player player, Structs.Reward reward, int amount)
    {
        GainingMoney?.InvokeSafely(new GainedMoneyEventArgs(player,reward, amount));
    }
    public static void OnBuyingItem(Player player, Structs.ItemPrice reward, int price)
    {
        BuyingItems?.InvokeSafely(new BuyingItemsEventArgs(player, reward, price));
    }
}
