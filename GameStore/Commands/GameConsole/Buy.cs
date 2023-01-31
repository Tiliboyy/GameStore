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
            response = Plugin.Instance.Translation.DntMessage;
            return true;
        }
        if (!Plugin.Enablegamestore)
        {
            response = Plugin.Instance.Translation.DisabledStore;
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
                response = player.GetAvailableCategories();
                return true;
            case 1:
            {
                if (int.TryParse(arguments.Array[1], out var argument1))
                {
                    response = player.GetAvailableItems(argument1);
                    return true;
                }

                response = player.BuyItemFromName(arguments.At(0));
                return true;

            }
            case 2 when !Round.IsStarted:
                response = Plugin.Instance.Translation.RoundNotStarted;
                return true;
            case 2:
            {
                int.TryParse(arguments.Array[1], out var category);
                int.TryParse(arguments.Array[2], out var item);
                
                response = player.BuyItemFromId(category, item);
                return true;
            }
            default:
                response = player.GetAvailableCategories();
                return true;
        }
    }
}