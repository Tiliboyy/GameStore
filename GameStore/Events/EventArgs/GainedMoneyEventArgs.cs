using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace GameStore.Events.EventArgs;

public class GainedMoneyEventArgs : IExiledEvent
{
    public Player Player { get; }
    
    public int Amount { get; }
    public Configs.Structs.Reward Reward { get; }

    public GainedMoneyEventArgs(Player player, Configs.Structs.Reward reward, int amount)
    {
        Reward = reward;
        Player = player;
        Amount = amount;
    }
}
