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
    private int clientSpawnCooldown;
    [SerializeField]
    private int baseReward = 50;
    private bool paused=false;

    private GameObject collectionManager;
    private List<GameObject> collectedTrash;
    private List<(Client, GameObject)> collectedDemand = new List<(Client, GameObject)>();

    public GameObject demande_prefab;

    private GameObject viewport;

    private static BlackMarket _instance;
    public static BlackMarket Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        collectionManager = GameObject.Find("CollectedItemDetector");
        collectedTrash = collectionManager.GetComponent<CollectionManager>().collectedTrash;

        viewport = GameObject.Find("Viewport").gameObject;
        // canvas.GetComponent<RectTransform>().transform.position = new Vector3(0, 10, -5f);

        StartCoroutine("delay");

        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    /*private void Update()
    {
        
        if (paused) { return; }
        SpawnClient();
        //collectedTrash = collectionManager.GetComponent<CollectionManager>().collectedTrash;
    }*/

    IEnumerator delay()
    {
        // SpawnClient();
        while (true)
        {
            SpawnClient();
            yield return new WaitForSeconds(clientSpawnCooldown);
        }
    }

    public void SpawnClient()
    {
        UpdateDemands();
        if (collectedDemand.Count > 10)
            return;
        Client client = new Client(minDuration, maxDuration, baseReward);

        //afficher la nouvelle demande
        string voiceLine = client.voiceLine;
        // demand = GameObject.Find("Demand");
        //TextMeshProUGUI textmeshPro = demand.GetComponent<TextMeshProUGUI>();
        //textmeshPro.SetText(voiceLine);
        GameObject new_dm_prefab = Instantiate(demande_prefab, viewport.transform);
        // new_dm_prefab.transform.position = new Vector3(0, -(collectedDemand.Count-1) * 20 - 170, -50);
        // new_dm_prefab.transform.rotation = Quaternion.Euler(0, 180, 0);
        RectTransform rt = new_dm_prefab.GetComponent<RectTransform>();
        rt.localPosition += Vector3.up * (collectedDemand.Count+1) * 50;
        //Debug.Log(rt.localPosition);
        //Debug.Log(rt.position);
        //Debug.Log(rt.anchoredPosition);
        // new_dm_prefab.GetComponent<RectTransform>().rect.yMin += 20;
        TextMeshProUGUI txt_mesh_pro = new_dm_prefab.transform.Find("Demand").GetComponent<TextMeshProUGUI>();
        Button bt = new_dm_prefab.transform.Find("Button").GetComponent<Button>();

        txt_mesh_pro.SetText(client.voiceLine);
        bt.interactable = CanBuy(collectedTrash, client) != null;


        //il faut stocker la demande dans la liste des demandes
        new_dm_prefab.transform.Find("Button").GetComponent<DemandButton>().client = client;
        collectedDemand.Add((client, new_dm_prefab));
    }

    public void UpdateDemands()
    {
        collectedTrash = GameObject.Find("CollectedItemDetector").GetComponent<CollectionManager>().collectedTrash;
        foreach ((Client client, GameObject go) in collectedDemand)
        {
            go.transform.Find("Button").GetComponent<Button>().interactable = CanBuy(collectedTrash, client) != null;
        }
    }

    public GameObject CanBuy(List<GameObject> list_trash, Client client)
    {
        foreach(GameObject go in list_trash)
        {
            if(CheckBuy(go, client))
            {
                return go;
            }
        }
        return null;
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

    public void Sell(Client sell_client)
    {
        int index = 0;
        foreach ((Client client, GameObject go) in collectedDemand)
        {
            if (sell_client.voiceLine == client.voiceLine)
            {
                GameObject to_destroy = CanBuy(collectedTrash, client);
                if (to_destroy != null)
                {
                    GameManager.Instance.money += client.reward;
                    collectedDemand.Remove((client, go));
                    GameObject.Find("CollectedItemDetector").GetComponent<CollectionManager>().collectedTrash.Remove(to_destroy);
                    Destroy(to_destroy);
                    Destroy(go.gameObject);
                    UpdatePos();
                    collectedTrash = GameObject.Find("CollectedItemDetector").GetComponent<CollectionManager>().collectedTrash;
                    UpdateDemands();
                    return;
                }
            }
            index++;
        }
    }

    public void UpdatePos()
    {
        int i = 0;
        foreach ((Client client, GameObject go) in collectedDemand)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, -220 + i * 50);
            i++;
        }
    }
}
