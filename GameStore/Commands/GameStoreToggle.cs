using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using database = GameStore.GameStoreDatabase.Database;


namespace GameStore.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
internal class GameStoreToggle : ICommand
{
    public string Command { get; } = "ToggleGameStore";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Turn the GameStore on or off";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission("gs.toggle"))
        {
            response = "You do not have permission to use this command";
            return false;
        }

        if (Plugin.Enablegamestore)
        {
            response = "GameStore wurde Deaktiviert";
            Plugin.Enablegamestore = false;
        }
        else
        {
            response = "GameStore wurde Aktiviert";
            Plugin.Enablegamestore = true;
        }

        return true;
    }
}