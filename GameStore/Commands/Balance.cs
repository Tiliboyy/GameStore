using System;
using System.Globalization;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using UnityEngine;

namespace GameStore.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
internal class GameStore : ICommand
{
    public string Command { get; } = "balance";

    public string[] Aliases { get; } = { "bal" };

    public string Description { get; } = "Zeigt dir deinen Kontostand an";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);
        if (player == null)
        {
            response = "You can only execute this as a Player";
            return false;
        }

        if (player.DoNotTrack)
        {
            response = Plugin.Instance.Translation.Dntmessage;
            return true;
        }

        var balance = GameStoreDatabase.Database.GetPlayerMoney(player);
        response = Plugin.Instance.Translation.balmessage.Replace("(balance)", balance.ToString(CultureInfo.InvariantCulture));
        return true;
    }
}