using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField]
    public float maxStess = 100f;
    [SerializeField]
    public float curStress;
    [SerializeField]
    public Image StressBarImage;
    void Start()
    {
       StressBarImage.fillAmount = 1;
    }


    void Update()
    {
        {
            StressBarImage.fillAmount = curStress / maxStess;
        }
    }

    public void SetCurStress(float Stress)
    {
        if (Stress < maxStess)
        {
            curStress = Stress;
        }

    }
}