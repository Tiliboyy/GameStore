using System.Globalization;
using System.IO;
using LiteDB;
using MEC;
using PlayerHints.UnityMethods;

namespace GameStore;

public static class GameStoreDatabase
{
    public static LiteDatabase db = new(Path.Combine(Paths.Configs, "Gamestore/GameStore.db"));

    public class DatabasePlayer
    {
        public string _id { get; set; }
        public float Money { get; set; }
    }

    public static class Database
    {
        public static void CreatePlayers()
        {
            var players = db.GetCollection<DatabasePlayer>("players");

            if (!players.Exists(x => true)) players.EnsureIndex(x => x._id);
        }

        public static void AddPlayer(Player player)
        {
            if (player == null) return;
            if (player.DoNotTrack) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            if (players.FindOne(x => x._id == playerID) != null) return;

            players.Insert(new DatabasePlayer
            {
                _id = playerID,
                Money = 0
            });
            var dbplayer = players.FindOne(x => x._id == playerID);
            players.Update(dbplayer);
        }

        
        public static void BuyItem(Player player, Config.ItemPrice item)
        {
            if (player.DoNotTrack) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer == null) return;
            dbplayer.Money -= item.Price;
            if (item.IsAmmo)
                foreach (var items in item.AmmoTypes)
                    player.AddAmmo(items.Key, items.Value);
            else
                foreach (var items in item.ItemTypes)
                    player.AddItem(items);


            players.Update(dbplayer);
        }

        public static bool CanRemoveMoneyFromPlayer(Player player, float money)
        {
            if (player == null) return false;
            if (player.DoNotTrack) return false;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null) return dbplayer.Money >= money;

            return false;
        }


        public static void AddMoneyToPlayer(Player player, float money)
        {
            if (player == null) return ;
            if (player.DoNotTrack || money == 0) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id != null && x._id == playerID);

            if (dbplayer == null) return;
            //Log.Debug(player.Nickname + " has been given " + money + " " + Plugin.Instance.Translation.Currencyname,
            //    Plugin.Instance.Config.Debug);
            //if (EventHandlers.PlayerHintsLoaded) Timing.RunCoroutine(UnityMethods.DisableHintsForTime(2, player));
            player.ShowHint
            (
                Plugin.Instance.Translation.Givemoneytext.Replace(
                "(moneyamount)", 
                money.ToString(CultureInfo.InvariantCulture)), 
                2
            );
            dbplayer.Money += money;
            if (dbplayer.Money < 0) dbplayer.Money = 0;
            players.Update(dbplayer);
        }

        public static float GetPlayerMoney(Player player)
        {
            if (player.DoNotTrack) return 0;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null)
                return dbplayer.Money;
            return 0;
        }

        public static void RemovePlayer(Player player)
        {
            if (player == null) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null) players.Delete(dbplayer._id);
        }
    }
}