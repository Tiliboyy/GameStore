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
            if (player == null)
            {
                response = "Falls du diese nachricht bekommst bitte geh @Tiliboyy#6969 auf discord anschreien";
                return false;
            }
            if (player.DoNotTrack)
            {
                response = Plugin.Instance.Translation.Dntmessage;
                return true;
            }
            float balance = GameStoreSEDatabase.Database.GetPlayerMoney(player); 
            response = "Du hast " + balance + " " + Plugin.Instance.Translation.Currencyname;
            return true;


        }
    }
}
