using GameStore;
using Exiled.API.Features;
using System;
using System.IO;
using MapEvent = Exiled.Events.Handlers.Map;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

public class Plugin : Plugin<Config, Translation>
{
    public override string Author => "Tiliboyy";

    public override string Name => "GameStore";

    public override string Prefix => "GameStore";
    public override Version Version => new(1, 0, 0);

    public override Version RequiredExiledVersion => new(5, 0, 0, 0);

    public static bool Enablegamestore = true;

    public EventHandlers EventHandler;

    public static Plugin Instance;

    public override void OnEnabled()
    {
        try
        {
            if (!Directory.Exists(Path.Combine(Paths.Configs, "Gamestore/")))
                Directory.CreateDirectory(Path.Combine(Paths.Configs, "Gamestore/"));
            Plugin.Instance = this;
            EventHandler = new EventHandlers();
            Server.WaitingForPlayers += EventHandler.OnWaitingForPlayers; 
            Player.Died += EventHandler.OnDeath;
            Player.Escaping += EventHandler.OnEscaping;
            Player.Spawned += EventHandler.OnSpawned;
            Player.Verified += EventHandler.OnVerified;
            Player.UsedItem += EventHandler.OnUsedItem;
        } catch(Exception e)
        {
            Log.Error("Tiliboyy hat eine Skill Issue   " + e);
        }

    }


    public override void OnDisabled()
    {
        Plugin.Instance = null;
        EventHandler = null;
        Player.Escaping -= EventHandler.OnEscaping;
        Player.Died -= EventHandler.OnDeath;
        Player.Spawned -= EventHandler.OnSpawned;
        Player.Verified -= EventHandler.OnVerified;
        Player.UsedItem -= EventHandler.OnUsedItem;

    }
}
