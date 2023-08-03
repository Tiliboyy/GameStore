using CommandSystem;
using System;

namespace GameStore.Commands.ClientConsole;

[CommandHandler(typeof(ClientCommandHandler))]
internal class Baltop : ICommand
{
    public string Command { get; } = "Baltop";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Shows Baltop";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = GameStoreDatabase.Database.GetLeaderboard();
        return true;
    }
}