using System;
using CommandSystem;
using PluginAPI.Core;
using static GameStore.Extensions;
using Player = Exiled.API.Features.Player;

namespace GameStore.Commands;

internal class Multiplier : ICommand
{
    public string Command { get; } = "multiplier";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Sets the Gamestore Money Muliplier";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 2)
        {
            response = "Usage: gamestore multiplier <multiplier>";
            return true;
        }

        if (!int.TryParse(arguments.At(0), out int i))
        {
            response = "Usage: gamestore multiplier <multiplier>";
            return true;
        }
        GameStorePlugin.MoneyMuliplier = i;
        response = $"Der Muliplier wurde auf {i} gesetzt.";
        return true;
    }
}
