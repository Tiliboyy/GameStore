using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Loader;
using GameStore;

public class EventHandlers
{
    public static bool PlayerHintsLoaded;

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
        GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, Plugin.Instance.Config.Escapeamount);
        if (ev.Player.Cuffer != null)
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player.Cuffer, Plugin.Instance.Config.Escapecufferamount);
    }

    public static void OnWaitingForPlayers()
    {
        foreach (var unused in Loader.Plugins.Where(plugins => plugins.Name == "PlayerHints"))
        {
            PlayerHintsLoaded = true;
            Log.Info("PlayerHints found");
        }
        GameStoreDatabase.Database.CreatePlayers();
    }

    public static void OnUsedItem(UsedItemEventArgs ev)
    {
        GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, 50);
    }

    public static void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.Player.IsScp)
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, Plugin.Instance.Config.Scpspawnamount);
        else
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, Plugin.Instance.Config.Spawnamount);
    }

    public static void OnDeath(DiedEventArgs ev)
    {
        if (ev.Target == null) return;
        GameStoreDatabase.Database.AddMoneyToPlayer(ev.Target, Plugin.Instance.Config.Deathamount);
        if (ev.Killer == null || ev.Killer.Id == ev.Target.Id)
            return;
        if (ev.Killer.Role.Team == Team.SCP && ev.Killer.Role == RoleType.Scp0492)
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Killer, Plugin.Instance.Config.Scpkillamount);
        else
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Killer, Plugin.Instance.Config.Killamount);
    }
}