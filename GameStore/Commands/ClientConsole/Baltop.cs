using System;
using System.Linq;
using CommandSystem;
using GameStore;


[CommandHandler(typeof(ClientCommandHandler))]
internal class Baltop : ICommand
{
    public string Command { get; } = "Baltop";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Shows Baltop";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = GameStoreDatabase.Database.GetLeaderBoard();
        return true;
    }
}