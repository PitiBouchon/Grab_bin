using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;

public class BlackMarket : MonoBehaviour
{
    [SerializeField]
    private int minDuration = 120;
    [SerializeField]
    private int maxDuration = 240;
    [SerializeField]
    private int clientSpawnCooldown = 100;
    [SerializeField]
    private int baseReward = 50;
    public void SpawnClient()
    {
        //new Client(minDuration, maxDuration, baseReward);

    }
}
