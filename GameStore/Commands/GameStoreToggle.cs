using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameStore.GameStoreSEDatabase;
using Player = Exiled.API.Features.Player;
using database = GameStore.GameStoreSEDatabase.Database;
using System.Linq;


namespace GameStore.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class GameStoreToggle : ICommand
    {
        public string Command { get; } = "ToggleGameStore";

        public string[] Aliases { get; } = new string[0];

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
}
