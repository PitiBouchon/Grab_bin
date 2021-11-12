using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private List<Transform> trashs;

    [SerializeField]
    private Vector3 conveyorBeltDir = Vector3.right;

    [SerializeField]
    private float speed = 1f;

    private void Start()
    {
        trashs = new List<Transform>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Thrash")
        {
            TryAdd(other.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Thrash" && trashs.Contains(other.transform))
        {
            trashs.Remove(other.transform);
        }
    }

    private void TryAdd(Transform trash_transform)
    {
        if (trashs.Contains(trash_transform))
            return;

        trashs.Add(trash_transform);
    }

    private void Update()
    {
        foreach (Transform trash_transform in trashs)
        {
            trash_transform.Translate(conveyorBeltDir * Time.deltaTime * speed);
        }
    }
}
