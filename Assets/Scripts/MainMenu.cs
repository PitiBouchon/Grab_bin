using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    private Camera cam;
    private float xAngle;
    private float zAngle;
    private Quaternion target;
    private states state;

    private enum states
    {
        BlackMarket,
        Entrance,
        Facility
    }

    private void Start()
    {
        cam = Camera.main;
        xAngle = cam.transform.rotation.eulerAngles.x;
        zAngle = cam.transform.rotation.eulerAngles.z;
        state = states.Facility;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            this.transform.Find("EscapeMenu").gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetKeyUp(KeyCode.Q) && state == states.Facility)
        {
            // if player presses q and is in the facility
            ToBlackMarket();
        }
        if(Input.GetKeyUp(KeyCode.Q) && state == states.Entrance)
        {
            // if player presses q and is in the entrance
            ToTrashSortingFromRight();
        }
        if (Input.GetKeyUp(KeyCode.D) && state == states.Facility)
        {
            // if player presses d and is in the faciity
            ToEntrance();
        }
        if(Input.GetKeyUp(KeyCode.D) && state == states.BlackMarket)
        {
            // if player presses d and is in the black market
            ToTrashSortingFromLeft();
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("General"));
    }
    public void ToTrashSortingFromRight()
    {
        StartCoroutine(CameraRotationToTrashSortingFromRight());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(true);
        this.transform.Find("ToEntranceButton").gameObject.SetActive(true);
        this.transform.Find("ToSortingFacilityButtonFromLeft").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromRight").gameObject.SetActive(false);
        state = states.Facility;
    }

    public void ToTrashSortingFromLeft()
    {
        StartCoroutine(CameraRotationToTrashSortingFromLeft());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(true);
        this.transform.Find("ToEntranceButton").gameObject.SetActive(true);
        this.transform.Find("ToSortingFacilityButtonFromLeft").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromRight").gameObject.SetActive(false);
        state = states.Facility;
    }

    public void ToBlackMarket()
    {
        StartCoroutine(CameraRotationToBlackMarket());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(false);
        this.transform.Find("ToEntranceButton").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromLeft").gameObject.SetActive(true);
        state = states.BlackMarket;
    }

    public void ToEntrance()
    {
        StartCoroutine(CameraRotationToEntrance());
        this.transform.Find("ToEntranceButton").gameObject.SetActive(false);
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromRight").gameObject.SetActive(true);
        state = states.Entrance;
    }

    public void Resume()
    {
        this.transform.Find("EscapeMenu").gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadScene("TitleScreen"));
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator CameraRotationToEntrance()
    {

        for (float i = 0; i <= 90; i += speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
    }

    private IEnumerator CameraRotationToBlackMarket()
    {

        for (float i = 0; i >= -90; i -= speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
        BlackMarket.Instance.transform.GetChild(0).gameObject.SetActive(true);
        BlackMarket.Instance.UpdateDemands();
    }

    private IEnumerator CameraRotationToTrashSortingFromRight()
    {

        for (float i = 90; i >= 0; i -= speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
    }

    private IEnumerator CameraRotationToTrashSortingFromLeft()
    {

        for (float i = -90; i <= 0; i += speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
    }

    private IEnumerator CameraZoomToBlackMarket()
    {
        yield return null;
    }

    IEnumerator LoadScene(string level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
