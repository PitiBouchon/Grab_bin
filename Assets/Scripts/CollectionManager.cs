using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectedTrash= new List<GameObject>();

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

