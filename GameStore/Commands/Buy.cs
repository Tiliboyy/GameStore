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
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Commanad : ICommand
    {
        public string Command { get; } = "buy";
       
        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "buy";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(sender);
            if (player.DoNotTrack)
            {
                response = Plugin.Instance.Translation.Dntmessage;
                return true;
            }
            if (!Plugin.Enablegamestore)
            {
                response = Plugin.Instance.Translation.Disabledstore;
                return true;
            }
            if (player == null || player.IsHost)
            {
                response = "You can only execute this as a Player";
                return false;
            }
            if(arguments.Count == 0)
            {
                response = Builders.CategoryBuilder();
                return true;
            }
            if(arguments.Count == 1)
            {
                int.TryParse(arguments.Array[1], out int argument1);
                if (argument1 > Plugin.Instance.Config.Categorys.Count)
                {
                    response = Plugin.Instance.Translation.Categorydoesnotexist;
                    return true;
                }
                response = Builders.ItemListBuilder(argument1);
                return true;

            }
            if(arguments.Count == 2)
            {
                if (!Round.IsStarted)
                {
                    response = Plugin.Instance.Translation.Roundnotstarted;
                    return true;
                }
                int.TryParse(arguments.Array[1], out int category);
                int.TryParse(arguments.Array[2], out int item);
                int i = 1;
                if(category > Plugin.Instance.Config.Categorys.Count)
                {
                    response = Plugin.Instance.Translation.Categorydoesnotexist;
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
                                response = Plugin.Instance.Translation.Fullinventory;
                                return true;
                            }
                            else
                            {
                                if (!itemnum.Roles.Contains(player.Role.Type) && !itemnum.Roles.Contains(RoleType.None))
                                {
                                    response = Plugin.Instance.Translation.WrongeRole;
                                    return true;
                                }
                                if(!database.CanRemoveMoneyFromPlayer(player, itemnum.Price))
                                {
                                    response = Plugin.Instance.Translation.Cantafford;
                                    return true;
                                }
                                if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems.ContainsKey(itemnum.Id))
                                {
                                    if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems[itemnum.Id] >= itemnum.Maxbuys)
                                    {
                                        response = Plugin.Instance.Translation.Maxamountreached;
                                        return true;

                                    }
                                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems[itemnum.Id]++; 
                                }
                                else
                                {
                                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems.Add(itemnum.Id, 1);
                                }
                                database.BuyItem(player, itemnum.Item, itemnum.Price);
                                response = Plugin.Instance.Translation.Boughtitem.Replace("(itemname)", itemnum.Name).Replace("(itemprice)", itemnum.Price.ToString());
                                return true;
                            }

                        }
                        i++;

                    }
                }
                response = "Dieses Item exisitert nicht";
                return true;
            }
            response = Builders.CategoryBuilder();
            return true;










        }
    }
}
