using UnityEngine;
using System.Collections;

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
        if (other.tag == "Thrash")
        {
            TryAdd(other.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.tag == "Thrash" && trashs.Contain(other.transform))
        {
            trashs.Remove(other.transform);
        }
    }

    private void TryAdd(Trash trash)
    {
        if (trashs.Contain(trash))
            return;

        trashs.Add(trash);
    }

    private void Update()
    {
        foreach (Thrash trash in thrashs)
        {
            trash.Translate(conveyorBeltDir * Time.deltaTime * speed);
        }
    }
}
