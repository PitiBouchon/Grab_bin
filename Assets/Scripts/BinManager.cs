using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinManager : MonoBehaviour
{
    public Category.CatColor BinCatColor;
    public Category.CatType BinCatType;

    Category.CatColor TrashCatColor;
    Category.CatType TrashCatType;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            //CatColor trashColor = GetComponent<TrashScript>.cat_color;
            TrashCatColor = collision.gameObject.GetComponent<TrashScript>().cat_color;
            TrashCatType = collision.gameObject.GetComponent<TrashScript>().cat_type;
            if (BinCatColor == TrashCatColor && BinCatType == TrashCatType)
            {
                Destroy(collision.gameObject);
                Bonus();
            }
            else
            {
                Destroy(collision.gameObject);
                Malus();
            }
        }
    }

    public void Bonus()
    {
        //Appelé, il donne des malus de vie et de stress
        print("bg gros");
    }

    public void Malus()
    {
        //Appelé, il donne des malus de vie et de stress
        print("Eclaté au sol");
    }
}
