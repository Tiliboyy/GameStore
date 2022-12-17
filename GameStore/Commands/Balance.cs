using CommandSystem;
using CustomPlayerEffects;
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

        public string Description { get; } = "Zeigt dir deinen Kontostand an";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null || player.IsHost)
            {
                response = "You can only execute this as a Player";
                return false;
            }
            if (player.DoNotTrack)
            {
                response = Plugin.Instance.Translation.Dntmessage;
                return true;
            }
            float balance = GameStoreSEDatabase.Database.GetPlayerMoney(player); 
            response = Plugin.Instance.Translation.balmessage.Replace("(balance)", balance.ToString());
            return true;


        }
    }
}
