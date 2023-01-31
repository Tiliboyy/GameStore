using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Exiled.API.Features;
using GameStore;
using GameStore.EventHandlers;
using PlayerRoles;
using MapEvent = Exiled.Events.Handlers.Map;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

public class Plugin : Plugin<Config, Translation>
{
    public static bool Enablegamestore = true;

    public static Plugin Instance;

    public EventHandlers EventHandler;
    public override string Author => "Tiliboyy";

    public override string Name => "GameStore";

    public override string Prefix => "GameStore";
    public override Version Version => new(1, 0, 0);

    public override Version RequiredExiledVersion => new(6, 0, 0, 0);

    public override void OnEnabled()
    {
        try
        {
            if (!Directory.Exists(Path.Combine(Paths.Configs, "Gamestore/")))
                Directory.CreateDirectory(Path.Combine(Paths.Configs, "Gamestore/"));
            Instance = this;
            EventHandler = new EventHandlers();
            Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            Player.Died += EventHandlers.OnDeath;
            Player.Escaping += EventHandlers.OnEscaping;
            Player.Spawned += EventHandlers.OnSpawned;
            Player.Verified += EventHandlers.OnVerified;
            Player.UsedItem += EventHandlers.OnUsedItem;
        }
        catch (Exception e)
        {
            Log.Error("Error :" + e);
        }
    }


    public override void OnDisabled()
    {
        Instance = null;
        EventHandler = null;
        Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
        Player.Escaping -= EventHandlers.OnEscaping;
        Player.Died -= EventHandlers.OnDeath;
        Player.Spawned -= EventHandlers.OnSpawned;
        Player.Verified -= EventHandlers.OnVerified;
        Player.UsedItem -= EventHandlers.OnUsedItem;
    }
}