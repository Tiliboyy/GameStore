using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using database = GameStore.GameStoreDatabase.Database;

namespace GameStore.Commands;

internal class Toggle : ICommand
{
    public string Command { get; } = "toggle";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Turn the GameStore on or off";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission($"gs.{Command}"))
        {
            response = "You do not have permission to use this command";
            return false;
        }

        if (GameStorePlugin.EnableGamestore)
        {
            response = "GameStore wurde deaktiviert";
            GameStorePlugin.EnableGamestore = false;
        }
        else
        {
            response = "GameStore wurde aktiviert";
            GameStorePlugin.EnableGamestore = true;
        }

        return true;
    }
}