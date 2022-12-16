using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using GameStore;
using System.Collections.Generic;
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;
using database = GameStore.GameStoreSEDatabase.Database;

public class EventHandlers : Plugin<Config>
{


    public static List<CoroutineHandle> coroutines = new();


    public void OnVerified(VerifiedEventArgs ev)
    {
        if (!ev.Player.DoNotTrack)
        {
            database.AddPlayer(ev.Player);
            ev.Player.GameObject.AddComponent<GameStoreComponent>();
        }
        else
        {
            database.RemovePlayer(ev.Player);
        }

    }
    public void OnEscaping(EscapingEventArgs ev)
    {
        database.AddMoneyToPlayer(ev.Player, 1000);
        if(ev.Player.Cuffer != null)
            database.AddMoneyToPlayer(ev.Player.Cuffer, 2000);

    }
    public void OnUsedItem(UsedItemEventArgs ev)
    {
        database.AddMoneyToPlayer(ev.Player, 100);
    }
    public void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.Player.IsScp)
            database.AddMoneyToPlayer(ev.Player, 200);
        else
            database.AddMoneyToPlayer(ev.Player, 50);
    }

    public void OnDeath(DyingEventArgs ev)
    {
        database.AddMoneyToPlayer(ev.Target, 50);
        if (ev.Killer != null || ev.Killer.Id != ev.Target.Id)
            database.AddMoneyToPlayer(ev.Killer, 50);


    }

}

