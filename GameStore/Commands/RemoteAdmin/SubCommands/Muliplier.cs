using CommandSystem;
using System;

namespace GameStore.Commands.RemoteAdmin.SubCommands;

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
