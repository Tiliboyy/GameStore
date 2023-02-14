using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.ThrowableProjectiles;
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
        ev.Player.GiveReward(Plugin.Instance.Config.EscapeReward);
        if (ev.Player.Cuffer != null)
            ev.Player.GiveReward(Plugin.Instance.Config.CufferReward);
    }
    public static void OnWaitingForPlayers()
    {
        GameStoreDatabase.Database.CreatePlayers();
    }

    public static void OnUsedItem(UsedItemEventArgs ev)
    {
        ev.Player.GiveReward(Plugin.Instance.Config.UsingItemReward);
    }

    public static void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.OldRole.Type is not RoleTypeId.ClassD or RoleTypeId.Scientist)
        {
            ev.Player.GiveReward(Plugin.Instance.Config.SpawnReward);
        }
    }

    public static void OnThownItem(ThrownProjectileEventArgs ev)
    {
        ev.Player.GiveReward(Plugin.Instance.Config.UsingItemReward);
    }

    public static void OnDying(DyingEventArgs ev)
    {
        if (ev.Player == null) 
            return;
        
        ev.Player.GiveReward(Plugin.Instance.Config.DeathReward);
        if (ev.DamageHandler.Type == DamageType.PocketDimension)
            foreach (var ply in Player.List.Where(x => x.Role.Type == RoleTypeId.Scp106))
                ply.GiveReward(Plugin.Instance.Config.KillReward);
        
        if (ev.Attacker == null || ev.Attacker.Id == ev.Player.Id)
            return;
        
        if (ev.Player.Role.Team == Team.SCPs)
            ev.Attacker.GiveReward(Plugin.Instance.Config.ScpKillReward);
        else
            ev.Attacker.GiveReward(Plugin.Instance.Config.KillReward);
    }


}