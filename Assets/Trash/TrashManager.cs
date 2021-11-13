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
        if (!trash.CompareTag("Trash"))
        {
            Debug.LogWarning("Ne peut pas spawn un objet qui n'a pas tag Trash");
            return;
        }

        Debug.Log("Spawned Trash !");
        GameObject spawned_trash = Instantiate(trash, this.transform);
        spawned_trash.transform.position = spawn_pos;
        spawned_trash.transform.rotation = Random.rotation;
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
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }
}
