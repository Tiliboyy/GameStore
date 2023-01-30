using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using static GameStore.GameStoreDatabase;
using database = GameStore.GameStoreDatabase.Database;

namespace GameStore.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
internal class AddMoney : ICommand
{
    public string Command { get; } = "AddMoney";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Gives Player Money";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission("gs.add"))
        {
            response = "You do not have permission to use this command";
            return false;
        }

        if (arguments.Count != 2)
        {
            response = "Usage: AddMoney ((player id / name) or (all / *)) (amount)";
            return false;
        }

        switch (arguments.At(0))
        {
            case "*":
            case "all":
                if (!float.TryParse(arguments.At(1), out var amount) && amount <= 0)
                {
                    response = $"Money argument invalid: {arguments.At(1)}";
                    return false;
                }

                foreach (var ply in Player.List)
                    ply.GameStoreMoneyPlayer(amount);

                response = $"Alle haben {amount} {Plugin.Instance.Translation.Currencyname} erhalten";
                return true;
            default:
                var pl = Player.Get(arguments.At(0));
                if (pl == null)
                {
                    response = $"Player not found: {arguments.At(0)}";
                    return false;
                }

                if (!float.TryParse(arguments.At(1), out var amountsingle) && amountsingle <= 0)
                {
                    response = $"Money argument invalid: {arguments.At(1)}";
                    return false;
                }
                pl.GameStoreMoneyPlayer(amountsingle);

                response =
                    $"Player {pl.Nickname} has been given {amountsingle} {Plugin.Instance.Translation.Currencyname}";
                return true;
        }
    }
}