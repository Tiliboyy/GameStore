using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Mirror;
using PlayerRoles;
using database = GameStore.GameStoreDatabase.Database;


namespace GameStore.Commands;
[CommandHandler(typeof(ClientCommandHandler))]
internal class Commanad : ICommand
{
    public string Command { get; } = "buy";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "buy";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);
        if (player == null)
        {
            response = "You can only execute this as a Player";
            return false;
        }
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

        if (arguments.Array == null)
        {
            response = "What the fuck did you do";
            return false;
        }

        switch (arguments.Count)
        {
            case 0:
                response = Builders.CategoryBuilder();
                return true;
            case 1:
            {
                if (int.TryParse(arguments.Array[1], out var argument1))
                {
                    response = Builders.ItemListBuilder(argument1);
                    return true;
                }

                response = player.BuyItemFromName(arguments.At(0));
                return true;

            }
            case 2 when !Round.IsStarted:
                response = Plugin.Instance.Translation.Roundnotstarted;
                return true;
            case 2:
            {
                int.TryParse(arguments.Array[1], out var category);
                int.TryParse(arguments.Array[2], out var item);
                if (category > Plugin.Instance.Config.Categorys.Count)
                {
                    response = Plugin.Instance.Translation.Categorydoesnotexist;
                    return true;
                }

                var list = Plugin.Instance.Config.Categorys.OrderBy(category1 => category1.id);
                foreach (var category1 in list)
                {
                    if (category1.id != category) continue;
                    var itemlist = category1.Items.OrderBy(price => price.Id);
                    foreach (var items in itemlist.Where(items => item == items.Id))
                    {
                        string num = $"{category1.id}{items.Id}";
                        if (!int.TryParse(num, out var result))
                        {
                            response = "Something went wrong please contact server staff";
                            return true;
                        }
                        if (!items.Roles.Contains(player.Role.Type) && !items.Roles.Contains(RoleTypeId.None) || player.IsScp)
                        {
                            response = Plugin.Instance.Translation.WrongeRole;
                            return true;
                        }
                        if (player.Items.Count >= 8)
                        {
                            response = Plugin.Instance.Translation.Fullinventory;
                            return true;
                        }


                        if (!database.CanRemoveMoneyFromPlayer(player, items.Price))
                        {
                            response = Plugin.Instance.Translation.Cantafford;
                            return true;
                        }

                        if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems.ContainsKey(result))
                        {
                            if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems[result] >=
                                items.Maxbuys)
                            {
                                response = Plugin.Instance.Translation.Maxamountreached;
                                return true;
                            }

                            player.GameObject.GetComponent<GameStoreComponent>().boughtitems[result]++;
                        }
                        else
                        {
                            player.GameObject.GetComponent<GameStoreComponent>().boughtitems.Add(result, 1);
                        }


                        database.BuyItem(player, items);
                        response = Plugin.Instance.Translation.Boughtitem.Replace("(itemname)", items.Name)
                            .Replace("(itemprice)", items.Price.ToString());
                        return true;
                    }
                }

                response = "Dieses Item exisitert nicht";
                return true;
            }
            default:
                response = Builders.CategoryBuilder();
                return true;
        }
    }
}