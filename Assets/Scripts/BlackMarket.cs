using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;
using UnityEngine.UI;

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
    private bool paused=false;
    public Text demand;

    private void Update()
    {
        
        if (paused) { return; }
        StartCoroutine("delay");
        SpawnClient();
        

    }

    IEnumerator delay()
    {
        paused = true;
        yield return new WaitForSeconds(clientSpawnCooldown);
        paused = false;
    }

    public void SpawnClient()
    {
        Client client = new Client(minDuration, maxDuration, baseReward);
        client.GetComponent<Client>();
       
        
        
    }
}
