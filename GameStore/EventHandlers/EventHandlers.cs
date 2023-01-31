using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace GameStore.EventHandlers;

public class EventHandlers
{
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
        ev.Player.GiveReward(Plugin.Instance.Config.Escape);
        if (ev.Player.Cuffer != null)
            ev.Player.GiveReward(Plugin.Instance.Config.EscapeCuffer);
    }
    public static void OnWaitingForPlayers()
    {
        GameStoreDatabase.Database.CreatePlayers();
    }

    public static void OnUsedItem(UsedItemEventArgs ev)
    {
        ev.Player.GiveReward(Plugin.Instance.Config.UsingItemAmount);
    }

    public static void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.OldRole.Type is not RoleTypeId.ClassD or RoleTypeId.Scientist)
        {
            ev.Player.GiveReward(Plugin.Instance.Config.SpawnAmount);
        }
    }

    public static void OnDeath(DiedEventArgs ev)
    {
        if (ev.Player == null) return;
        ev.Player.GiveReward(Plugin.Instance.Config.DeathAmount);
        if (ev.Attacker == null || ev.Attacker.Id == ev.Player.Id)
            return;
        if (ev.Player.Role.Team == Team.SCPs)
            ev.Attacker.GiveReward(Plugin.Instance.Config.ScpKillAmount);
        else
            ev.Attacker.GiveReward(Plugin.Instance.Config.KillAmount);
    }


}