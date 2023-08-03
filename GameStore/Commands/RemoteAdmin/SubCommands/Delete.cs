using CommandSystem;
using System;
using Player = Exiled.API.Features.Player;

namespace GameStore.Commands.RemoteAdmin.SubCommands;

internal class Delete : ICommand
{
    public string Command { get; } = "delete";

    public string[] Aliases { get; } = new []{"del"};

    public string Description { get; } = "Deletes a Players database entry";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count >= 1)
        {
            response = "Usage: gamestore delete <player>";
            return true;
        }

        var player = Player.Get(arguments.At(0));
        if (player == null)
        {
            response = "Player was not found";
            return true;
        }

        if (arguments.Count == 2)
        {
            if (arguments.At(2) != "CONFIRM")
            {
                response = "WARNIMG: You are about to delete the Database entry of " + player.Nickname +
                           ",if you are sure you want to do that type gamestore delete <player> CONFIRM";
                return true;
            }
            GameStoreDatabase.Database.RemovePlayer(player);
            response = $"{player.Nickname}'s database entry was removed!";
            return true;

        }
        response = "WARNIMG: You are about to delete the Database entry of " + player.Nickname +
                   ",if you are sure you want to do that type gamestore delete <player> CONFIRM";
        return true;

    }
}
