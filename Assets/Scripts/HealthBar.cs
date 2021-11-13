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

   
    void Update()
    {
        {
            HeatlBarImage.fillAmount = curHP/maxHP;
        }
    }
}
