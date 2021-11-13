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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPos, 0.5f);
    }

    private GameObject[] trashPrefab
    {
        get { return TrashManager.Instance.trashes; }
    }

    private Dictionary<MachineType, Dictionary<CatType, Dictionary<GameObject, float>>> dropRates = new Dictionary<MachineType, Dictionary<CatType, Dictionary<GameObject, float>>>();

    private void Start()
    {
        List<GameObject> temp = new List<GameObject>(TrashManager.Instance.trashes);
        foreach (int i in System.Enum.GetValues(typeof(MachineType)))
        {
            dropRates.Add((MachineType)i, new Dictionary<CatType, Dictionary<GameObject, float>>());
            foreach (int j in System.Enum.GetValues(typeof(CatType)))
            {
                dropRates[(MachineType)i].Add((CatType)j, new Dictionary<GameObject, float>());
                foreach (GameObject obj in temp)
                {
                    dropRates[(MachineType)i][(CatType)j].Add(obj, 1f);
                }
            }
        }
        //add drop rates here following this example:
        // set float f this way: f=min spawn amount + probability of extra spawning
        dropRates[MachineType.RAFFINEUR][CatType.ORGANIC][trashPrefab[0]] = 0.6f;
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
        Debug.LogWarning("heho la ca va pas ou quoi");
        foreach (KeyValuePair<GameObject, float> couple in dropRates[type][trash.cat_type])
        {
            float temp = Random.Range(0f, 1f);
            while (couple.Value > temp)
            {
                yield return new WaitForSeconds(spawnDelay);
                Output(couple.Key);
                temp += Random.Range(1f, 2f);
            }
        }
        Destroy(trash.gameObject);
    }

    private void Output(GameObject trash)
    {
        GameObject temp = Instantiate(trash, spawnPos, Quaternion.Euler(spawnRot));
        temp.GetComponent<TrashScript>().cat_type = trash.GetComponent<TrashScript>().cat_type;
        temp.GetComponent<TrashScript>().cat_color = trash.GetComponent<TrashScript>().cat_color;
        temp.GetComponent<Rigidbody>().AddForce(spawnForce);
    }
}
