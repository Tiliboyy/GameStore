using Exiled.API.Features;
using GameStore.Configs;
using GameStore.Events.EventArgs;
using System;

namespace GameStore.Events;

public static class Handlers
{
    public static event Exiled.Events.Events.CustomEventHandler<GainedMoneyEventArgs> GainingMoney;
    public static event Exiled.Events.Events.CustomEventHandler<BuyingItemsEventArgs> BuyingItems;

    public static void OnGainingMoney(Player player, Structs.Reward reward, int amount)
    {
        GainingMoney?.Invoke(new GainedMoneyEventArgs(player, reward, amount));
    }
    public static void OnBuyingItem(Player player, Structs.ItemPrice reward, int price)
    {
        BuyingItems?.Invoke(new BuyingItemsEventArgs(player, reward, price));
    }
}
