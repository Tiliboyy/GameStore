using CommandSystem;
using GameStore.Components;
using System;
using Player = Exiled.API.Features.Player;

namespace GameStore.Commands.RemoteAdmin.SubCommands;

internal class ResetLimits : ICommand
{
    public string Command { get; } = "resetlimits";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Resets a players buy Limits";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 1)
        {
            response = "Usage: gamestore resetlimits <PlayerID>";
            return true;
        }

        var player = Player.Get(arguments.At(0));
        if (player == null)
        {
            response = "Player was not found";
            return true;
        }
        
        player.GameObject.GetComponent<GameStoreComponent>().ResetBuyLimits();
        response = $"Limits reset!";
        return true;

    }
}
