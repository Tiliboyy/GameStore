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

namespace GameStore.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Subclassing : ICommand
    {
        public string Command { get; } = "buy";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "buy";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(sender);
            if(!Round.IsStarted)
            {
                response = "Das spiel hat noch nicht gestartet";
                return true;
            }
            if (player == null || player.IsHost)
            {
                response = "You can only execute this as a Player";
                return false;
            }
            if(arguments.Count == 0)
            {
                response = Builder.CategoryBuilder();
                return true;
            }
            if(arguments.Count == 1)
            {
                int.TryParse(arguments.Array[1], out int argument1);
                if (argument1 > Plugin.Instance.Config.Categorys.Count)
                {
                    response = "Diese Kategorie existiert nicht!";
                    return true;
                }
                response = Builder.ItemListBuilder(argument1);
                return true;

            }
            if(arguments.Count == 2)
            {
                int.TryParse(arguments.Array[1], out int category);
                int.TryParse(arguments.Array[2], out int item);
                int i = 1;
                if(category > Plugin.Instance.Config.Categorys.Count)
                {
                    response = "Diese Kategorie existiert nicht!";
                    return true;
                }
                foreach (var itemnum in Plugin.Instance.Config.Items)
                {
                    if (category == itemnum.Category)
                    {
                        if (item == i)
                        {
                            if (player.Items.Count >= 8)
                            {
                                response = "Dein Inventar ist voll!";
                                return true;
                            }
                            else
                            {
                                if (!itemnum.Roles.Contains(player.Role.Type) && !itemnum.Roles.Contains(RoleType.None))
                                {
                                    response = "Deine Rolle erlaubt es dir nicht dieses Item zu kaufen,";
                                    return true;
                                }
                                if(!database.CanRemoveMoneyFromPlayer(player, itemnum.Price))
                                {
                                    response = "Du kannst dir dieses Item nicht leisten";
                                    return true;

                                }
                                if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems.ContainsKey(itemnum.Item))
                                {
                                    if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems[itemnum.Item] >= itemnum.Maxbuys)
                                    {
                                        response = "Du hast schon genug davon gekauft.";
                                        return true;

                                    }
                                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems[itemnum.Item]++; 
                                }
                                else
                                {
                                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems.Add(itemnum.Item, 1);
                                }
                                database.BuyItem(player, itemnum.Item, itemnum.Price);
                                response = "Du hast " + itemnum.Name + " für " + itemnum.Price + " " + Plugin.Instance.Translation.Currencyname + " gekauft";
                                return true;
                            }

                        }
                        i++;

                    }
                }
                response = "Dieses Item exisitert nicht";
                return true;
            }
            response = Builder.CategoryBuilder();
            return true;










        }
    }
}
