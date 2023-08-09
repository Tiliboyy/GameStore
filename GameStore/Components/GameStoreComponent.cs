using System.Collections.Generic;
using UnityEngine;

namespace GameStore.Components;

public class GameStoreComponent : MonoBehaviour
{
    public Dictionary<int, int> BoughtItems = new();
    public Dictionary<string, int> RewardLimit = new();
    public void ResetBuyLimits()
    {
        BoughtItems = new Dictionary<int, int>();
        RewardLimit = new Dictionary<string, int>();
    }

}