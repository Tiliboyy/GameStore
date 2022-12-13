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
    public void OnDying(DyingEventArgs ev)
    {
        database.AddMoneyToPlayer(ev.Target, 100);
    }

}

