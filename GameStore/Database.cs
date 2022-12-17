using Exiled.API.Features;
using Exiled.Events.Commands.Show;
using Exiled.Events.Handlers;
using Exiled.Loader;
using FMODUnity;
using LiteDB;
using MEC;
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

                if (!players.Exists(x => true))
                {
                    players.EnsureIndex(x => x._id);
                }
            }

            public static void AddPlayer(Player player)
            {
                if (player.DoNotTrack || player == null) return;
                string playerID = player.RawUserId.Split('@')[0];
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
                if (player.DoNotTrack || player == null) return;
                string playerID = player.RawUserId.Split('@')[0];
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
                if (player.DoNotTrack || player == null) return;
                string playerID = player.RawUserId.Split('@')[0];
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
                if (player.DoNotTrack || player == null) return false;

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
                if (player.DoNotTrack || player == null || money == 0) return;
                string playerID = player.RawUserId.Split('@')[0];
                var players = db.GetCollection<DatabasePlayer>("players");

                var dbplayer = players.FindOne(x => x._id == playerID);

                if (dbplayer != null)
                {
                    Log.Debug(player.Nickname + " has been given " + money + " " + Plugin.Instance.Translation.Currencyname, Plugin.Instance.Config.Debug);
                    if (EventHandlers.PlayerHintsLoaded)
                    {
                        Log.Info("called");
                        Timing.RunCoroutine(PlayerHints.UnityMethods.UnityMethods.DisableHintsForTime(2, player));
                    }
                    player.ShowHint(Plugin.Instance.Translation.Givemoneytext.Replace("(moneyamount)", money.ToString()), 2);
                    dbplayer.Money += money;
                    if (dbplayer.Money < 0)
                    {
                        dbplayer.Money = 0;
                    }
                    players.Update(dbplayer);
                }

            }

            public static float GetPlayerMoney(Player player)
            {
                if (player.DoNotTrack || player == null) return 0;
                string playerID = player.RawUserId.Split('@')[0];
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
                if (player == null) return;
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
