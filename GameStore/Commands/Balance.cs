using CommandSystem;
using CustomPlayerEffects;
using GameStore.UnityMethods;
using Exiled.Permissions.Extensions;
using GameStore;
using System;
using Player = Exiled.API.Features.Player;

namespace GameStore.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class GameStore : ICommand
    {
        public string Command { get; } = "balance";

        public string[] Aliases { get; } = new[] { "bal" };

        public string Description { get; } = "give you your Balance";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            float balance = GameStoreSEDatabase.Database.GetPlayerMoney(player); 
            response = "Du hast " + balance + " Money";
            return true;


        }
    }
}
