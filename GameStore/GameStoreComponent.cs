using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameStore;

public class GameStoreComponent : MonoBehaviour
{
    public Dictionary<int, int> boughtitems = new();
    public Dictionary<string, int> rewardlimit = new();

    public float RoundSpentMoney = 0;
    public float LifeSpentMoney = 0;
    
    public float RoundGainedMoney = 0;
    public float LifeGainedMoney = 0;
    public void ResetBuyLimits()
    {
        boughtitems = new Dictionary<int, int>();
        rewardlimit = new Dictionary<string, int>();
    }

}