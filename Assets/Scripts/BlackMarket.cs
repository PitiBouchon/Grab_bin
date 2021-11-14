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
    private List<Client> collectedDemand = new List<Client>();

    public GameObject demande_prefab;

    private GameObject viewport;

    private void Start()
    {
        collectionManager = GameObject.Find("CollectedItemDetector");
        collectedTrash = collectionManager.GetComponent<CollectionManager>().collectedTrash;

        viewport = GameObject.Find("Viewport").gameObject;
        // canvas.GetComponent<RectTransform>().transform.position = new Vector3(0, 10, -5f);
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
        collectedDemand.Add(client);

        //afficher la nouvelle demande
        string voiceLine = client.voiceLine;
        // demand = GameObject.Find("Demand");
        //TextMeshProUGUI textmeshPro = demand.GetComponent<TextMeshProUGUI>();
        //textmeshPro.SetText(voiceLine);
        GameObject new_dm_prefab = Instantiate(demande_prefab, viewport.transform);
        new_dm_prefab.transform.position = new Vector3(0, -(collectedDemand.Count-1) * 20 - 170, -50);
        new_dm_prefab.transform.rotation = Quaternion.Euler(0, 180, 0);
        TextMeshProUGUI txt_mesh_pro = new_dm_prefab.transform.Find("Demand").GetComponent<TextMeshProUGUI>();
        Button bt = new_dm_prefab.transform.Find("Button").GetComponent<Button>();

        txt_mesh_pro.SetText(client.voiceLine);
        bt.interactable = CanBuy(collectedTrash, client);
    }

    public bool CanBuy(List<GameObject> list_trash, Client client)
    {
        foreach(GameObject go in list_trash)
        {
            if(CheckBuy(go, client))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckBuy(GameObject trash, Client client)
    {
        if (client.askedColor == null || client.askedColor == trash.GetComponent<TrashScript>().cat_color)
        {
            if (client.askedType == null || client.askedType == trash.GetComponent<TrashScript>().cat_type)
            {
                if (client.askedTrash == null || client.askedTrash == trash.GetComponent<TrashScript>().trash_name)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
