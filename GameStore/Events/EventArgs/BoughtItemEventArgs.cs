using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace GameStore.Events.EventArgs;

public class BuyingItemsEventArgs : IExiledEvent
{
    public Player Player { get; set; }
    public Configs.Structs.ItemPrice Item { get; set; }

    public int Price { get; }

    public BuyingItemsEventArgs(Player player,Configs.Structs.ItemPrice itemPrice, int price)
    {
        Item = itemPrice;
        Player = player;
        Price = price;
    }
}
