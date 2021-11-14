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
    public TrashName trash_name;

    public float height = 1.5f;

    private void Awake()
    {
        if (this.tag != "Trash" || this.gameObject.layer != LayerMask.NameToLayer("Trash"))
        {
            Debug.LogWarning("L'objet " + this.name + "DOIT avoir le tag Trash et le layer Trash");
        }

        UpdateTrashColor();
    }

    public void UpdateTrashColor()
    {
        switch (cat_color)
        {
            case CatColor.RED:
                SetTrashColor(Color.red);
                break;
            case CatColor.BLUE:
                SetTrashColor(Color.blue);
                break;
            case CatColor.GREEN:
                SetTrashColor(Color.green);
                break;
            case CatColor.PURPLE:
                SetTrashColor(new Color(0.5f, 0, 0.5f));
                break;
            case CatColor.YELLOW:
                SetTrashColor(Color.yellow);
                break;
        }
    }

    private void SetTrashColor(Color color)
    {
        foreach(MeshRenderer mesh_renderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            foreach(Material mat in mesh_renderer.materials)
            {
                mat.SetColor("_Color", color);
            }
        }
    }
}
