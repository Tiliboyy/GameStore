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
        if (ev.Player.Role == RoleTypeId.Overwatch || ev.Player.Role == RoleTypeId.Spectator || ev.Player.Role == RoleTypeId.Tutorial) return;
        
        if (ev.Player.IsScp)
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, Plugin.Instance.Config.Scpspawnamount);
        else
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, Plugin.Instance.Config.Spawnamount);
    }

    public static void OnDeath(DiedEventArgs ev)
    {
        if (ev.Player == null) return;
        GameStoreDatabase.Database.AddMoneyToPlayer(ev.Player, Plugin.Instance.Config.Deathamount);
        if (ev.Attacker == null || ev.Attacker.Id == ev.Player.Id)
            return;
        if (ev.Attacker.Role.Team == Team.SCPs && ev.Attacker.Role.Type == RoleTypeId.Scp0492)
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Attacker, Plugin.Instance.Config.Scpkillamount);
        else
            GameStoreDatabase.Database.AddMoneyToPlayer(ev.Attacker, Plugin.Instance.Config.Killamount);
    }
}