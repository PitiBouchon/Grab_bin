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
    private bool AskForColor = false;

    public void SpawnClient()
    {
        int clientLifeSpan = Random.Range(minDuration, maxDuration);
        //

    }
}
