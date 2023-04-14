using System;
using CommandSystem;
using PluginAPI.Core;
using static GameStore.Extensions;
using Player = Exiled.API.Features.Player;

namespace GameStore.Commands;

internal class Set : ICommand
{
    public string Command { get; } = "set";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Sets a players money";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 2)
        {
            response = "Usage: gamestore set <PlayerID> <Money>";
            return true;
        }

        var player = Player.Get(arguments.At(0));
        if (player == null)
        {
            response = "Player was not found";
            return true;
        }

        if (!float.TryParse(arguments.At(1), out float result))
        {
            response = "Thats not a number!";
            return true;
        }

        var oldamount = player.GetMoney();
        if (player.SetMoney(result))
        {
            response = $"Die {GameStorePlugin.Instance.Translation.CurrencyName} von {player.Nickname} wurde von {oldamount} auf {result} gesetzt";
            return true;
        }

        response = $"Der Spieler wurde nicht gefunden oder er hat DNT aktiviert.";
        return true;
    }
}
