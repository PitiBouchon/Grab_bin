using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private int money;

    [SerializeField] private float dayLength = 300f;
    private float startDayTime = -1f;

    private int day = 0;

    private enum ruleTarget
    {
        OBJECT,
        CATEGORY
    }

    private ruleTarget currTarget;

    private void StartDay()
    {
        day++;
        Invoke("EndDay", dayLength);
        startDayTime = Time.time;
        //Change rules
        //UnlockMachine();
        //change drop rates
        //add
    }

    private void ChangeRule()
    {
        currTarget = (ruleTarget)(Random.Range(0, 2));
    }

    private string GetTime()
    {
        float temp = (Time.time - startDayTime) / dayLength * 24;
        int hours = Mathf.FloorToInt(temp);
        int min = (int)((temp - hours) * 60);
        return $"{hours}:{min}";
    }
}
