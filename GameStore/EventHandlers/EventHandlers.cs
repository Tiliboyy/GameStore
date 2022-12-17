using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using GameStore;
using System.Collections.Generic;
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;
using Database = GameStore.GameStoreSEDatabase.Database;
using Exiled.Loader;

public class EventHandlers
{


    public static List<CoroutineHandle> coroutines = new();

    public static bool PlayerHintsLoaded = false;
    public void OnVerified(VerifiedEventArgs ev)
    {
        if (!ev.Player.DoNotTrack)
        {
            Database.AddPlayer(ev.Player);
            ev.Player.GameObject.AddComponent<GameStoreComponent>();
        }
        else
        {
            Database.RemovePlayer(ev.Player);
        }

    }
    public void OnEscaping(EscapingEventArgs ev)
    {
        Database.AddMoneyToPlayer(ev.Player, 1000);
        if(ev.Player.Cuffer != null)
            Database.AddMoneyToPlayer(ev.Player.Cuffer, 500);

    }
    public void OnWaitingForPlayers()
    {
        foreach (var plugins in Loader.Plugins)
        {
            Log.Info(plugins.Name);
            if (plugins.Name == "PlayerHints")
            {
                PlayerHintsLoaded = true;
                Log.Info("PlayerHints found");
            }
        }
    }
    public void OnUsedItem(UsedItemEventArgs ev)
    {
        Database.AddMoneyToPlayer(ev.Player, 100);
    }
    public void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.Player.IsScp)
            Database.AddMoneyToPlayer(ev.Player, 200);
        else
            Database.AddMoneyToPlayer(ev.Player, 50);
    }

    public void OnDeath(DiedEventArgs ev)
    {
        if (ev.Target == null) return;
        Database.AddMoneyToPlayer(ev.Target, Plugin.Instance.Config.Deathamount );
        if (ev.Killer == null || ev.Killer.Id == ev.Target.Id)
            return;
        Log.Info(ev.Killer);
        if(ev.Killer.Role.Team == Team.SCP && ev.Killer.Role == RoleType.Scp0492)
        {
            Database.AddMoneyToPlayer(ev.Killer, Plugin.Instance.Config.Scpkillamount);
        }
        else
        {
            Database.AddMoneyToPlayer(ev.Killer, Plugin.Instance.Config.Killamount);
        }


    }

}

