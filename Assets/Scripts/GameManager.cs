using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private float dayLength = 300f;

    private int day = 0;

    private void StartDay()
    {
        day++;
        Invoke("EndDay", dayLength);

        //Change rules
        //UnlockMachine();
        //change drop rates
        //add
    }
}
