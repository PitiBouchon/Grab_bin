using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    private static TrashManager _instance;
    public static TrashManager Instance { get { return _instance; } }
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

    public Vector3 spawn_pos;

    public GameObject[] trashes;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawn_pos, 0.5f);
    }

    public void SpawnTrash(GameObject trash)
    {
        // Debug.Log("Create new Trash !");
        GameObject spawned_trash = Instantiate(trash, this.transform);
        SetTrash(spawned_trash);
        GameManager.Instance.spawnedObjects++;
    }

    public void SetTrash(GameObject trash)
    {
        trash.transform.position = spawn_pos;
        trash.transform.rotation = Random.rotation;
        trash.GetComponent<Rigidbody>().velocity = Vector3.zero;
        trash.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void Start()
    {
        StartCoroutine("SpawnContinously");
    }

    IEnumerator SpawnContinously()
    {
        while (true)
        {
            SpawnTrash(trashes[Random.Range(0, trashes.Length)]);
            yield return new WaitForSeconds(Random.Range(1.6f, 3.5f));
        }
    }
}
