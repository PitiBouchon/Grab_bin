using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public float maxHP=100f;
    [SerializeField]
    public float curHP=100f;
    [SerializeField]
    public Image HeatlBarImage;
    void Start()
    {
        HeatlBarImage.fillAmount = 1;
    }

    public void SetCurHP(float HP)
    {
        curHP = HP;
    }

   
    void Update()
    {
        {
            HeatlBarImage.fillAmount = curHP/maxHP;
        }
    }
}
