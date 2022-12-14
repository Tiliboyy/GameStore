using Exiled.API.Features;
using Exiled.Events.Handlers;
using HarmonyLib;
using LiteDB;
using RemoteAdmin.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Player = Exiled.API.Features.Player;

namespace GameStore
{
    public static class GameStoreSEDatabase
    {
        
        public static LiteDatabase db = new(Path.Combine(Paths.Configs, "GamestoreSE/GameStoreSE.db"));
        public class DatabasePlayer
        {
            public string _id { get; set; }
            public float Money { get; set; }
        }

        public static class Database
        {
            public static void Create()
            {
                var players = db.GetCollection<DatabasePlayer>("players");

                if (!players.Exists(x => true))
                {
                    players.EnsureIndex(x => x._id);
                }
            }

            public static void AddPlayer(Player player)
            {
                if (player.DoNotTrack) return;
                string playerID = player.RawUserId.Split('@')[0];
                Create();

                var players = db.GetCollection<DatabasePlayer>("players");

                if (players.FindOne(x => x._id == playerID) != null)
                {
                    return;
                }

                players.Insert(new DatabasePlayer
                {
                    _id = playerID,
                    Money = 0
                });
                var dbplayer = players.FindOne(x => x._id == playerID);
                players.Update(dbplayer);

            }


            public static void RemoveMoneyFromPlayer(Player player, float money)
            {
                if (player.DoNotTrack) return;
                string playerID = player.RawUserId.Split('@')[0];
                Create();
                var players = db.GetCollection<DatabasePlayer>("players");

                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null && CanRemoveMoneyFromPlayer(player, money))
                {
                    dbplayer.Money -= money;

                    players.Update(dbplayer);
                }
            }
            public static void BuyItem(Player player,ItemType item, float cost)
            {
                if (player.DoNotTrack) return;
                string playerID = player.RawUserId.Split('@')[0];
                Create();
                var players = db.GetCollection<DatabasePlayer>("players");

                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null)
                {

                    dbplayer.Money -= cost;
                    player.AddItem(item);
                    players.Update(dbplayer);
                }
            }
            public static bool CanRemoveMoneyFromPlayer(Player player, float money)
            {
                if (player.DoNotTrack) return false;
                string playerID = player.RawUserId.Split('@')[0];
                var players = db.GetCollection<DatabasePlayer>("players");
                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null)
                {
                    return dbplayer.Money >= money;
                }

                return false;
            }


            public static void AddMoneyToPlayer(Player player, float money)
            {
                if (player.DoNotTrack) return;
                string playerID = player.RawUserId.Split('@')[0];
                Create();
                var players = db.GetCollection<DatabasePlayer>("players");

                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null)
                {
                    player.ShowHint(Plugin.Instance.Translation.Givemoneytext.Replace("(moneyamount)", money.ToString()), 2);
                    dbplayer.Money += money;

                    players.Update(dbplayer);
                }
            }

            public static float GetPlayerMoney(Player player)
            {
                if (player.DoNotTrack) return 0;
                string playerID = player.RawUserId.Split('@')[0];
                Create();

                var players = db.GetCollection<DatabasePlayer>("players");

                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null)
                {
                    return dbplayer.Money;
                }
                else
                {
                    return 0;
                }
            }
            public static void RemovePlayer(Player player)
            {
                string playerID = player.RawUserId.Split('@')[0];
                var players = db.GetCollection<DatabasePlayer>("players");
                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null)
                {
                    players.Delete(dbplayer._id);
                }
            }
        }
    }
}
