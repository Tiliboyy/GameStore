using Exiled.API.Features;
using Exiled.Events.Handlers;
using GameStore.Configs;
using GameStore.Events.EventArgs;
using System;
using System.IO;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace GameStore;

public class GameStorePlugin : Plugin<Config, Translation>
{
    public static bool EnableGamestore = true;
    
    public static int MoneyMuliplier = 1;

    public static GameStorePlugin Instance;

    public EventHandlers.EventHandlers EventHandler;
    public override string Author => "Tiliboyy";

    public override string Name => "GameStore";

    public override string Prefix => "GameStore";
    public override Version Version => new(1, 0, 0);

    public override Version RequiredExiledVersion => new(6, 0, 0, 0);
    

    public override void OnEnabled()
    {
        try
        {
            Instance = this;
            EventHandler = new EventHandlers.EventHandlers();
            if (!Directory.Exists(Path.Combine(Paths.Configs, "Gamestore/")))
                Directory.CreateDirectory(Path.Combine(Paths.Configs, "Gamestore/"));
            Server.WaitingForPlayers += EventHandlers.EventHandlers.OnWaitingForPlayers;
            Player.Dying += EventHandlers.EventHandlers.OnDying;
            Player.Escaping += EventHandlers.EventHandlers.OnEscaping;
            Player.Spawned += EventHandlers.EventHandlers.OnSpawned;
            Player.Verified += EventHandlers.EventHandlers.OnVerified;
            Player.UsedItem += EventHandlers.EventHandlers.OnUsedItem;
            Player.ThrownProjectile += EventHandlers.EventHandlers.OnThownItem;
            Player.EscapingPocketDimension += EventHandlers.EventHandlers.OnEscapingPocketDimension;
            Player.FailingEscapePocketDimension += EventHandlers.EventHandlers.OnFailingEscapePocketDimension;
            Player.EnteringPocketDimension += EventHandlers.EventHandlers.OnEnteringPocketDimension;
            Scp079.GainingLevel += EventHandlers.EventHandlers.OnGainingLevel;
        }
        catch (Exception e)
        {
            Log.Error("Error: " + e);
        }
    }


    public override void OnDisabled()
    {
        Server.WaitingForPlayers -= EventHandlers.EventHandlers.OnWaitingForPlayers;
        Player.Escaping -= EventHandlers.EventHandlers.OnEscaping;
        Player.Dying -= EventHandlers.EventHandlers.OnDying;
        Player.Spawned -= EventHandlers.EventHandlers.OnSpawned;
        Player.Verified -= EventHandlers.EventHandlers.OnVerified;
        Player.UsedItem -= EventHandlers.EventHandlers.OnUsedItem;
        Player.ThrownProjectile -= EventHandlers.EventHandlers.OnThownItem;
        Player.EscapingPocketDimension -= EventHandlers.EventHandlers.OnEscapingPocketDimension;
        Player.FailingEscapePocketDimension -= EventHandlers.EventHandlers.OnFailingEscapePocketDimension;
        Player.EnteringPocketDimension -= EventHandlers.EventHandlers.OnEnteringPocketDimension;
        Scp079.GainingLevel -= EventHandlers.EventHandlers.OnGainingLevel;
        Instance = null;
        EventHandler = null;
    }
}