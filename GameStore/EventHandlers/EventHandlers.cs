using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace GameStore.EventHandlers;

public class EventHandlers
{
    public static Dictionary<Player, Player> PocketPlayers = new();

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
    
    public static void OnGainingLevel(Exiled.Events.EventArgs.Scp079.GainingLevelEventArgs ev)
    {
        ev.Player?.GiveReward(Plugin.Instance.Config.Scp079LevelReward);
    }
    public static void OnEscaping(EscapingEventArgs ev)
    {
        ev.Player.GiveReward(Plugin.Instance.Config.EscapeReward);
        ev.Player.Cuffer?.GiveReward(Plugin.Instance.Config.CufferReward);
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
        if (ev.Reason is SpawnReason.Respawn or SpawnReason.RoundStart or SpawnReason.LateJoin)
            ev.Player.GiveReward(Plugin.Instance.Config.SpawnReward);
    }
    
    public static void OnThownItem(ThrownProjectileEventArgs ev)
    {
        ev.Player.GiveReward(Plugin.Instance.Config.UsingItemReward);
    }
    
    public static void OnDying(DyingEventArgs ev)
    {
        if(Round.IsEnded || !Round.IsStarted) return;
        if (ev.Player == null) 
            return;
        ev.Player.GiveReward(Plugin.Instance.Config.DeathReward);
        if (ev.Player.GameObject.TryGetComponent<GameStoreComponent>(out var gameStoreComponent))
        {
            gameStoreComponent.LifeSpentMoney = 0;
            gameStoreComponent.LifeGainedMoney = 0;
        }
        if (ev.DamageHandler.Type == DamageType.PocketDimension)
        {
            if (!PocketPlayers.ContainsKey(ev.Player)) return; 
            PocketPlayers[ev.Player].GiveReward(Plugin.Instance.Config.KillReward);
            PocketPlayers.Remove(ev.Player);
            return;
        }
        if (PocketPlayers.ContainsKey(ev.Player))
            PocketPlayers.Remove(ev.Player);
        
        
        if (ev.Attacker == null || ev.Attacker.Id == ev.Player.Id)
            return;
        if (ev.Player.Role.Team == Team.SCPs && ev.Player.Role.Type != RoleTypeId.Scp0492)
            ev.Attacker.GiveReward(Plugin.Instance.Config.ScpKillReward);
        else
            ev.Attacker.GiveReward(Plugin.Instance.Config.KillReward);

    }
    
    public static void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
    {
        if(ev.Scp106 != null && ev.Player != null)
            PocketPlayers.Add(ev.Player, ev.Scp106);
    }
    
    public static void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
    {
        if (!PocketPlayers.ContainsKey(ev.Player)) return;
        PocketPlayers[ev.Player].GiveReward(Plugin.Instance.Config.KillReward);
        PocketPlayers.Remove(ev.Player);
    }
    
    public static void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
    {
        if (PocketPlayers.ContainsKey(ev.Player))
            PocketPlayers.Remove(ev.Player);
    }
}