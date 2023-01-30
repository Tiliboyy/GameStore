using System.Linq;
using Exiled.Events.EventArgs;
using Exiled;
using Exiled.Events.EventArgs.Player;
using Exiled.Loader;
using GameStore;
using PlayerRoles;

public class EventHandlers
{
    public static bool PlayerHintsLoaded = false;

    public static void OnVerified(VerifiedEventArgs ev)
    {
        if (!ev.Player.DoNotTrack)
        {
            GameStoreDatabase.Database.AddPlayer(ev.Player);
            ev.Player.GameObject.AddComponent<GameStoreComponent>();
        }
        else
        {
            GameStoreDatabase.Database.RemovePlayer(ev.Player);
        }
    }

    public static void OnEscaping(EscapingEventArgs ev)
    {
        ev.Player.GameStoreRewardPlayer(Plugin.Instance.Config.Escape);
        if (ev.Player.Cuffer != null)
            ev.Player.GameStoreRewardPlayer(Plugin.Instance.Config.Escapecuffer);
    }
    public static void OnWaitingForPlayers()
    {
        GameStoreDatabase.Database.CreatePlayers();
    }

    public static void OnUsedItem(UsedItemEventArgs ev)
    {
        ev.Player.GameStoreRewardPlayer(Plugin.Instance.Config.UsingItemsamount);
    }

    public static void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.OldRole.Type is not RoleTypeId.ClassD or RoleTypeId.Scientist)
        {
            ev.Player.GameStoreRewardPlayer(Plugin.Instance.Config.Spawnamount);
        }
    }

    public static void OnDeath(DiedEventArgs ev)
    {
        if (ev.Player == null) return;
            ev.Player.GameStoreRewardPlayer(Plugin.Instance.Config.Deathamount);
        if (ev.Attacker == null || ev.Attacker.Id == ev.Player.Id)
            return;
        if (ev.Player.Role.Team == Team.SCPs)
            ev.Attacker.GameStoreRewardPlayer(Plugin.Instance.Config.Scpkillamount);
        else
            ev.Attacker.GameStoreRewardPlayer(Plugin.Instance.Config.Killamount);
    }


}