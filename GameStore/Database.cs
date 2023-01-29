using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Exiled.API.Features;
using FMOD;
using LiteDB;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace GameStore;

public static class GameStoreDatabase
{

    public static IEnumerator<float> HintWaitUntilFalse(Player player, string message, float duration)
    {
        yield return Timing.WaitUntilFalse(() => player.HasHint && player.CurrentHint != null && !player.CurrentHint.Content.Contains("<size=69></size>"));
        player.ShowHint(message, duration);
        yield return 0;
    }

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

        
        public static void BuyItem(Player player, Structs.ItemPrice item)
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

        public static bool CanRemoveMoneyFromPlayer(Player player, float reward)
        {
            if (player == null) return false;
            if (player.DoNotTrack) return false;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null) return dbplayer.Money >= reward;

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
            player.SendHintWhenNone
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
        public static void AddRewardToPlayer(Player player, Structs.Reward reward)
        {
            if (player == null) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id != null && x._id == playerID);

            if (dbplayer == null) return;
            
            if (reward.Money.ContainsKey(player.Role.Type))
            {
                if (player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.ContainsKey(reward.Name))
                {
                    int amount = player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name];
                    if (amount >= reward.MaxPerRound  && reward.MaxPerRound != -1)
                    {
                        return;
                    }

                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.Add(reward.Name, 1);
                }
                
                player.SendHintWhenNone
                (Plugin.Instance.Translation.Givemoneytext.Replace("(moneyamount)", reward.Money[player.Role.Type].ToString(CultureInfo.InvariantCulture)), 2);
            }else if (reward.Money.ContainsKey(RoleTypeId.None))
            {
                if (player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.ContainsKey(reward.Name))
                {
                    int amount = player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name];
                    if (amount >= reward.MaxPerRound  && reward.MaxPerRound != -1)
                    {
                        return;
                    }

                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.Add(reward.Name, 1);
                }
                dbplayer.Money += reward.Money[RoleTypeId.None];
                player.SendHintWhenNone
                (
                    Plugin.Instance.Translation.Givemoneytext.Replace(
                        "(moneyamount)", 
                        reward.Money[RoleTypeId.None].ToString(CultureInfo.InvariantCulture)), 
                    2
                );
            }
            else
            {
                return;
            }
            if (dbplayer.Money < 0) dbplayer.Money = 0;
            if (dbplayer.Money > Plugin.Instance.Config.MaxMoney) 
                dbplayer.Money = Plugin.Instance.Config.MaxMoney;

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