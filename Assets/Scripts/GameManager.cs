using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;
using UnityEngine.SceneManagement;

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

    private int money = 1000;
    private int depense;
    public int spawnedObjects;
    public int sortedObjects;
    private int salaire = 250;

    private bool isAtDayEnd = false;
    private int keptObjects
    {
        get { return CollectionManager.Instance.collectedTrash.Count; }
    }

    [SerializeField] public float dayLength = 90f;
    private float startDayTime = -1f;

    private int day = 0;

    public CatColor ruleTarget;

    [SerializeField]
    private Texture2D[] textures;

    private void Start()
    {
        StartDay();
    }

    private void StartDay()
    {
        foreach (GameObject gm in GameObject.FindGameObjectsWithTag("Trash"))
        {
            Destroy(gm);
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
        money += (int)(sortedObjects * 1f / spawnedObjects) * salaire;
        depense = Random.Range(230, 270);
        money -= depense;

        //update stress
        //100-90->baisse de 10%
        //90-75->10%
        //75-50->20%
        //50-30->30%
        //30-10->50%
        //0-10->100%
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Time.timeScale == 0)
            {
                if (money < 0)
                {
                    SceneManager.LoadScene("TitleScreen");
                }
                Time.timeScale = 1;
                StartDay();
            }
        }
    }

    private void ChangeRule()
    {
        ruleTarget = (CatColor)(Random.Range(0, System.Enum.GetValues(typeof(CatColor)).Length));
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
        if (!turnOn)
        {
            Time.timeScale = 0;
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - 64, 0, 64, 64), textures[6]);
        GUI.DrawTexture(new Rect(Screen.width - 128, 0, 64, 64), textures[5]);
        GUI.DrawTexture(new Rect(Screen.width - 192, 0, 64, 64), textures[(int)GameManager.Instance.ruleTarget]);
        if (isAtDayEnd)
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "");
            //%dechets tri�
            GUI.Label(new Rect(Screen.width / 4+10, Screen.height / 4+10, Screen.width / 2, Screen.height / 2), $"% Déchets bien triés : {(int)((sortedObjects * 100f / spawnedObjects))}%");
            //salaire
            GUI.Label(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 50, Screen.width / 2, Screen.height / 2), $"Salaire: {(int)(sortedObjects * 1f / spawnedObjects * salaire)}");

            //GUI.Label(new Rect(), $"% dechets bien trie : {(int)((sortedObjects / spawnedObjects) * 100)}%");
            //salaire
            //GUI.Label(new Rect(), $"Salaire : {(int)(sortedObjects / spawnedObjects * salaire)}");
            //argent kipar
            GUI.Label(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 80, Screen.width / 2, Screen.height / 2), $"Dépense : {depense}");
            // ARGENT
            GUI.Label(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 110, Screen.width / 2, Screen.height / 2), $"Total : {money}");

            GUI.Label(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 130, Screen.width / 2, Screen.height / 2), $"T1 : {sortedObjects}");
            GUI.Label(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 150, Screen.width / 2, Screen.height / 2), $"T2 : {spawnedObjects}");
        }
    }
}
