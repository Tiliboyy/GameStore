using CommandSystem;
using CustomPlayerEffects;
using GameStore.UnityMethods;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameStore.GameStoreSEDatabase;
using Player = Exiled.API.Features.Player;
using database = GameStore.GameStoreSEDatabase.Database;
using System.Linq;
using static Config;
using MEC;

namespace GameStore.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class AddMoney : ICommand
    {
        public string Command { get; } = "AddMoney";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Gives Player Money";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 2)
            {
                response = "Usage: AddMoney ((player id / name) or (all / *)) (amount)";
                return false;
            }
            switch (arguments.At(0))
            {
                case "*":
                case "all":
                    if (!float.TryParse(arguments.At(1), out float amount) && amount <= 0)
                    {
                        response = $"Money argument invalid: {arguments.At(1)}";
                        return false;
                    }
                    foreach (Player ply in Player.List)
                        Database.AddMoneyToPlayer(ply, amount);

                    response = $"Alle haben {amount} {Plugin.Instance.Translation.Currencyname} erhalten";
                    return true;
                default:
                    Player pl = Player.Get(arguments.At(0));
                    if (pl == null)
                    {
                        response = $"Player not found: {arguments.At(0)}";
                        return false;
                    }
                    if (!float.TryParse(arguments.At(1), out float amountsingle) && amountsingle <= 0)
                    {
                        response = $"Money argument invalid: {arguments.At(1)}";
                        return false;
                    }
                    Database.AddMoneyToPlayer(pl, amountsingle);  

                    response = $"Player {pl.Nickname} has been given {amountsingle} {Plugin.Instance.Translation.Currencyname}";
                    return true;
            }
        }
    }
}
