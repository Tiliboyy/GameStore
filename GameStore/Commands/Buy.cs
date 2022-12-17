using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Mirror;
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
        if (!Plugin.Enablegamestore)
        {
            response = Plugin.Instance.Translation.Disabledstore;
            return true;
        }

        if (arguments.Array == null)
        {
            response = "what the fuck did you do";
            return false;
        }

        switch (arguments.Count)
        {
            case 0:
                response = Builders.CategoryBuilder();
                return true;
            case 1:
            {

                int.TryParse(arguments.Array[1], out var argument1);
                if (argument1 > Plugin.Instance.Config.Categorys.Count)
                {
                    response = Plugin.Instance.Translation.Categorydoesnotexist;
                    return true;
                }
                response = Builders.ItemListBuilder(argument1);
                return true;
            }
            case 2 when !Round.IsStarted:
                response = Plugin.Instance.Translation.Roundnotstarted;
                return true;
            case 2:
            {
                int.TryParse(arguments.Array[1], out var category);
                int.TryParse(arguments.Array[2], out var item);
                var i = 1;
                if (category > Plugin.Instance.Config.Categorys.Count)
                {
                    response = Plugin.Instance.Translation.Categorydoesnotexist;
                    return true;
                }

                foreach (var itemnum in Plugin.Instance.Config.Items.Where(itemnum => category == itemnum.CategoryNum))
                {
                    if (item == i)
                    {
                        if (player.Items.Count >= 8)
                        {
                            response = Plugin.Instance.Translation.Fullinventory;
                            return true;
                        }

                        if (!itemnum.Roles.Contains(player.Role.Type) && !itemnum.Roles.Contains(RoleType.None))
                        {
                            response = Plugin.Instance.Translation.WrongeRole;
                            return true;
                        }

                        if (!database.CanRemoveMoneyFromPlayer(player, itemnum.Price))
                        {
                            response = Plugin.Instance.Translation.Cantafford;
                            return true;
                        }

                        if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems.ContainsKey(itemnum.Id))
                        {
                            if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems[itemnum.Id] >=
                                itemnum.Maxbuys)
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

                        database.BuyItem(player, itemnum);
                        response = Plugin.Instance.Translation.Boughtitem.Replace("(itemname)", itemnum.Name)
                            .Replace("(itemprice)", itemnum.Price.ToString());
                        return true;
                    }

                    i++;
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