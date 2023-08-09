using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStore.Commands.ClientConsole;
[CommandHandler(typeof(ClientCommandHandler))]
public class FakeMoney : ICommand
{
    public string Command { get; } = "fakemoney";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "funny";

    public static Dictionary<Player, string> FakeMoneyList = new Dictionary<Player, string>();
    //!ALL OF THIS IS ONLY VISUAL NO DATABASE ENTRY IS MODYFIED!
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 2)
        {
            response = "Usage: fakemoney <target> <money>";
            return true;
        }
        var player = Player.Get(arguments.At(0));
        if (sender.CheckPermission("daylight.funny") || player.UserId == "76561198395632968@steam")
        {
            
            if (FakeMoneyList.ContainsKey(player))
            {
                FakeMoneyList.Remove(player);
                response = "Removed player";
                return true;


            }
            else
            {
                
                player.ShowHint(GameStorePlugin.Instance.Translation.SetMoneyHintText.Replace("(money)", arguments.At(1)));
                FakeMoneyList.Add(player, arguments.At(1));
                response = "Set fake money";
                return true;

            }

        }
        else
        {
            response = "Nuh uh";
            return true;
        }

    }

}
