using System.Collections.Generic;
using UnityEngine;

namespace GameStore;

public class GameStoreComponent : MonoBehaviour
{
    public Dictionary<int, int> boughtitems = new();
    public Dictionary<string, int> rewardlimit = new();

}