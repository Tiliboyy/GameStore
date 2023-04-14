using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using GameStore;

/*
[CommandHandler(typeof(ClientCommandHandler))]
internal class Pay : ICommand
{
    public string Command { get; } = "pay";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Usage: pay <Player> <Amount>";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!GameStorePlugin.Instance.Config.EnablePay)
        {
            response = $"Dieses Feature ist Disabled";
            return true;
        }
        if (arguments.Count != 2)
        {
            response = $"Usage: pay <Player> <Amount>";
            return true;
        }
        
        var paysender = Player.Get(sender);
        var payreciever = Player.Get(arguments.At(0));
        if (payreciever == null || paysender == null)
        {
            response = $"Spieler wurde nicht gefunden";
            return true;
        }

        if (payreciever == paysender)
        {
            response = $"No just no";
            return true;
        }
        if (!float.TryParse(arguments.At(1), out var amount))
        {
            response = $"Error: Die menge muss eine Zahl sein";
            return true;
        }

        if (!GameStoreDatabase.Database.CanPay(paysender, amount))
        {
            response = $"Du kannst nur {GameStorePlugin.Instance.Config.MaxDailyPayAmount.ToString()} {GameStorePlugin.Instance.Translation.CurrencyName} am Tag senden!";
            return true;
        }

        if (!GameStoreDatabase.Database.CanRemoveMoneyFromPlayer(paysender, amount))
        {
            response = $"Du hast nicht genügend {GameStorePlugin.Instance.Translation.CurrencyName}!";
            return true;
        }
        GameStoreDatabase.Database.PayToPlayer(paysender, payreciever, amount);
        response = $"{payreciever.Nickname} wurde {amount} {GameStorePlugin.Instance.Translation.CurrencyName} hinzugefügt";
        return true;
    }
}
*/