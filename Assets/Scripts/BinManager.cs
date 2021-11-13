using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinManager : MonoBehaviour
{
    public Category.CatColor BinCatColor;
    public Category.CatType BinCatType;

    Category.CatColor TrashCatColor;
    Category.CatType TrashCatType;

    float curHP;
    float curStress;
    GameObject healthManager;
    GameObject stressManager;


    public AudioSource audio;
    private void Start()
    {
        //curHP = GetComponent<HealthBar>().curHP;
        //variablea = GameObject.Find("Interface").GetComponent("GuiManager").variableA;
        healthManager = GameObject.Find("HealthManager");
        curHP =healthManager.GetComponent<HealthBar>().curHP;

        stressManager = GameObject.Find("StressManager");
        curStress = stressManager.GetComponent<StressBar>().curStress;

        audio = GetComponent<AudioSource>();
    }
    
    

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            audio.Play();
            //CatColor trashColor = GetComponent<TrashScript>.cat_color;
            TrashCatColor = collision.gameObject.GetComponent<TrashScript>().cat_color;
            TrashCatType = collision.gameObject.GetComponent<TrashScript>().cat_type;
            if (BinCatType == TrashCatType)
            {
                Destroy(collision.gameObject);
                Bonus();
                GameManager.Instance.SortTrash();

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
        curHP += 5;
        healthManager.GetComponent<HealthBar>().SetCurHP(curHP);
        curStress += 5;
        stressManager.GetComponent<StressBar>().SetCurStress(curStress);

        print("bg gros");
    }

    public void Malus()
    {
        curHP -= 5;
        healthManager.GetComponent<HealthBar>().SetCurHP(curHP);
        curStress -= 5;
        stressManager.GetComponent<StressBar>().SetCurStress(curStress);

        print("t'es une merde");
    }
}
