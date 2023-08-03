using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace GameStore.Commands.RemoteAdmin.SubCommands;

internal class Add : ICommand
{
    public string Command { get; } = "add";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Gives a player money";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission($"gs.{Command}"))
        {
            response = "You do not have permission to use this command";
            return false;
        }

        if (arguments.Count != 2)
        {
            response = "Usage: gamestore Add <<PlayerID> or <all / *>> <amount>";
            return false;
        }

        switch (arguments.At(0))
        {
            case "*":
            case "all":
                if (!int.TryParse(arguments.At(1), out int amount) && amount <= 0)
                {
                    response = "Thats not a number!";
                    return false;
                }

                foreach (var ply in Player.List)
                    ply.GiveMoney(amount);

                response = $"Everyone was given {amount} {GameStorePlugin.Instance.Translation.CurrencyName}";
                return true;
            default:
                var pl = Player.Get(arguments.At(0));
                if (pl == null)
                {
                    response = $"Player not found: {arguments.At(0)}";
                    return false;
                }

                if (!int.TryParse(arguments.At(1), out int amountsingle) && amountsingle <= 0)
                {
                    response = $"Money argument invalid: {arguments.At(1)}";
                    return false;
                }
                pl.GiveMoney(amountsingle);

                response =
                    $"Player {pl.Nickname} has been given {amountsingle} {GameStorePlugin.Instance.Translation.CurrencyName}";
                return true;
        }
    }
}