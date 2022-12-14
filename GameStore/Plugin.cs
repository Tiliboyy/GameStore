using GameStore;
using Exiled.API.Features;
using System;
using System.IO;
using MapEvent = Exiled.Events.Handlers.Map;
using Player = Exiled.Events.Handlers.Player;

public class Plugin : Plugin<Config, Translation>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "GameStore";
    public override Version Version => new(1, 0, 0);

    public override Version RequiredExiledVersion => new(5, 0, 0, 0);

    public static bool Enablegamestore = true;
    public EventHandlers EventHandler;
    public static Plugin Instance;

    public override void OnEnabled()
    {
        if (!Directory.Exists(Path.Combine(Paths.Configs, "GamestoreSE/")))
            Directory.CreateDirectory(Path.Combine(Paths.Configs, "GamestoreSE/"));
        Plugin.Instance = this;
        EventHandler = new EventHandlers();
        Player.Dying += EventHandler.OnDying;
        Player.Verified += EventHandler.OnVerified;



    }


    public override void OnDisabled()
    {
        Plugin.Instance = null;
        EventHandler = null;
        Player.Dying -= EventHandler.OnDying;
        Player.Verified -= EventHandler.OnVerified;
    }
}
