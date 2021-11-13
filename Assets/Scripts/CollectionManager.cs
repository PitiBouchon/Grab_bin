using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    private static CollectionManager _instance;
    public static CollectionManager Instance { get { return _instance; } }
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

    public List<GameObject> collectedTrash= new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        collectedTrash.Add(other.gameObject);
        other.gameObject.layer = 7;
    }

    public void PurgeCOllection()
    {
        foreach(GameObject go in collectedTrash)
        {
            Destroy(go);
        }
    }
}

