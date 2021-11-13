using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField]
    public float maxStess = 100f;
    [SerializeField]
    public float curStress = 100f;
    [SerializeField]
    public Image StressBarImage;
    void Start()
    {
       StressBarImage.fillAmount = 1;
    }


    void Update()
    {
        if (Input.GetKey("z"))
        {
            StressBarImage.fillAmount = curStress / maxStess;
        }
    }
}