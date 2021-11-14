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

    public int money;
    public int spawnedObjects;
    public int sortedObjects;
    private int salaire = 250;

    private bool isAtDayEnd = false;
    private int keptObjects
    {
        get { return CollectionManager.Instance.collectedTrash.Count; }
    }

    [SerializeField] private float dayLength = 90f;
    private float startDayTime = -1f;

    private int day = 0;

    private enum ruleTarget
    {
        OBJECT,
        CATEGORY
    }

    private ruleTarget currTarget;

    private void Start()
    {
        StartCoroutine(StartDay());
    }

    private IEnumerator StartDay(bool first=true)
    {
        if (!first)
        {
            yield return new WaitForSeconds(10f);
        }
        isAtDayEnd = false;
        FadeLight(true);

        day++;
        Invoke("EndDay", dayLength);
        startDayTime = Time.time;
        ChangeRule();
        sortedObjects = 0;
        spawnedObjects = 0;
        //UnlockMachine();
        //change drop rates
    }

    private void EndDay()
    {
        isAtDayEnd = true;
        FadeLight(false);
        //update money
        money += sortedObjects / spawnedObjects * salaire;
        //update stress
        //100-90->baisse de 10%
        //90-75->10%
        //75-50->20%
        //50-30->30%
        //30-10->50%
        //0-10->100%
        StartCoroutine(StartDay(false));
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
    private void FadeLight(bool value)
    {
        Light light = GameObject.Find("Directional Light").GetComponent<Light>();
        StartCoroutine(Fade(light, value));
    }

    private IEnumerator Fade(Light light, bool turnOn)
    {
        for (int i = 0; i < 100; i++)
        {
            light.intensity = ((turnOn?(i):(100 - i))) / 100f;
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void SortTrash()
    {
        sortedObjects++;
    }

    private void OnGUI()
    {
        if (isAtDayEnd)
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "");
            //%dechets tri�
            GUI.Label(new Rect(Screen.width / 4+10, Screen.height / 4+10, Screen.width / 2, Screen.height / 2), $"% de d�chets tri�s correctement: {(int)((sortedObjects / spawnedObjects) * 100)}%");
            //salaire
            GUI.Label(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 50, Screen.width / 2, Screen.height / 2), $"Salaire: {(int)(sortedObjects / spawnedObjects * salaire)}");

            GUI.Label(new Rect(), $"% de d�chets tri�s correctement: {(int)((sortedObjects / spawnedObjects) * 100)}%");
            //salaire
            GUI.Label(new Rect(), $"Salaire: {(int)(sortedObjects / spawnedObjects * salaire)}");
            //argent kipar

        }
    }
}
