using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Mirror;
using NorthwoodLib.Pools;
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
            response = GameStorePlugin.Instance.Translation.DntMessage;
            return true;
        }
        if (!GameStorePlugin.EnableGamestore)
        {
            response = GameStorePlugin.Instance.Translation.DisabledStore;
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
                
                if (!player.IsAlive)
                {
                    response = player.GetAvailableCategories(true);
                    return true;
                }
                response = player.GetAvailableCategories();
                return true;
            case 1:
            {
                

                if (int.TryParse(arguments.Array[1], out var argument1))
                {
                    if (!player.IsAlive)
                    {
                        response = player.GetAvailableItems(argument1);
                        return true;
                    }
                    response = player.GetAvailableItems(argument1);
                    return true;
                }
                if (!player.IsAlive)
                {
                    response = GameStorePlugin.Instance.Translation.CategoryDoesNotExist;
                    return true;
                }
                string name = FormatArguments(arguments, 0);
                response = player.BuyItemFromName(name);
                return true;

            }
            case 2:
            {
                
                if (!player.IsAlive)
                {
                    response = player.GetAvailableCategories(true);
                    return true;
                }
                if (int.TryParse(arguments.Array[1], out var category) &&
                    int.TryParse(arguments.Array[2], out var item))
                {
                    response = player.BuyItemFromId(category, item);
                    return true;
                }
                string name1 = FormatArguments(arguments, 0);
                response = player.BuyItemFromName(name1);
                return true;
                
            }
            case >2:                 
                string name2 = FormatArguments(arguments, 0);
                response = player.BuyItemFromName(name2);
                return true;
            default:
                response = player.GetAvailableCategories();
                return true;
        }
        
    }
    public static string FormatArguments(ArraySegment<string> sentence, int index)
    {
        var sb = StringBuilderPool.Shared.Rent();
        foreach (string word in sentence.Segment(index))
        {
            sb.Append(word);
            sb.Append(" ");
        }
        string msg = sb.ToString();
        StringBuilderPool.Shared.Return(sb);
        return msg;
    }
}