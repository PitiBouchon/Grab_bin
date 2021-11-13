using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;

public class Machine : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnPos;
    [SerializeField]
    private Vector3 spawnRot;
    [SerializeField]
    private Vector3 spawnForce;

    [SerializeField]
    private float spawnDelay;

    //public Dictionary<CatType, GameObject> trashPrefab;
    [SerializeField] private GameObject trashPrefab;

    private Dictionary<MachineType, Dictionary<CatType, Dictionary<CatType, float>>> dropRates = new Dictionary<MachineType, Dictionary<CatType, Dictionary<CatType, float>>>();

    private void Awake()
    {
        foreach (int i in System.Enum.GetValues(typeof(MachineType)))
        {
            dropRates.Add((MachineType)i, new Dictionary<CatType, Dictionary<CatType, float>>());
            foreach (int j in System.Enum.GetValues(typeof(CatType)))
            {
                dropRates[(MachineType)i].Add((CatType)j, new Dictionary<CatType, float>());
                foreach (int k in System.Enum.GetValues(typeof(CatType)))
                {
                    dropRates[(MachineType)i][(CatType)j].Add((CatType)k, 0f);
                }
            }
        }
        //add drop rates here following this example:
        // set float f this way: f=min spawn amount + probability of extra spawning
        dropRates[MachineType.RAFFINEUR][CatType.ORGANIC][CatType.CARDBOARD] = 0.6f;
    }

    public enum MachineType
    {
        LAVOMATIQUE,
        RAFFINEUR,
        DECONTAMINATOR,
        INCINERATOR
    }

    public MachineType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            StartCoroutine(Treat(other.GetComponent<TrashScript>()));
        }
    }

    private IEnumerator Treat(TrashScript trash)
    {
        foreach (KeyValuePair<CatType, float> frequency in dropRates[type][trash.cat_type])
        {
            float temp = 0f;
            while (frequency.Value - temp > 0)
            {
                yield return new WaitForSeconds(spawnDelay);
                Output(frequency.Key, trash.cat_color);
                temp += Random.Range(1f, 2f);
                Destroy(trash.gameObject);
            }
        }
    }

    private void Output(CatType trashType, CatColor trashColor)
    {
        GameObject temp = Instantiate(trashPrefab/*trashPrefab[trashType]*/, spawnPos, Quaternion.Euler(spawnRot));
        temp.GetComponent<TrashScript>().cat_type = trashType;
        temp.GetComponent<TrashScript>().cat_color = trashColor;
        temp.GetComponent<Rigidbody>().AddForce(spawnForce);
    }
}
