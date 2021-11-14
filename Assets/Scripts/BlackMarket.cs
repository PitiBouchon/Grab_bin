using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;
using UnityEngine.UI;
using TMPro;
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
    public GameObject demand;

    private GameObject collectionManager;
    private List<GameObject> collectedTrash;
    private List<GameObject> collectedDemand;

    private void Start()
    {
        collectionManager = GameObject.Find("CollectedItemDetector");
        
    }
    private void Update()
    {
        
        if (paused) { return; }
        StartCoroutine("delay");
        SpawnClient();
        collectedTrash = collectionManager.GetComponent<CollectionManager>().collectedTrash;


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

        //il faut stocker la demande dans la liste des demandes
        List<GameObject> collectedDemand.Add(client);

        //afficher la nouvelle demande
        string voiceLine = client.voiceLine;
        demand = GameObject.Find("Demand");
        TextMeshProUGUI textmeshPro = demand.GetComponent<TextMeshProUGUI>();
        textmeshPro.SetText(voiceLine);



    }
}
