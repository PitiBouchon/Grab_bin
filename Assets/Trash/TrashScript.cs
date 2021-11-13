using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class TrashScript : MonoBehaviour
{
    public CatType cat_type;
    public CatColor cat_color;

    private void Awake()
    {
        if (this.tag != "Trash" || this.gameObject.layer != LayerMask.NameToLayer("Trash"))
        {
            Debug.LogWarning("L'objet " + this.name + "DOIT avoir le tag Trash et le layer Trash");
        }
    }
}
