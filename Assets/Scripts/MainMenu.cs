using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float translationSpeed = 1.5f;
    private Camera cam;
    private float xAngle;
    private float zAngle;
    private float xPos;
    private float yPos;
    private Quaternion targetAngle;
    private Vector3 targetPosition;
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
        xPos = cam.transform.position.x;
        yPos = cam.transform.position.y;
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
        StartCoroutine(CameraTranslationForward());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(true);
        this.transform.Find("ToEntranceButton").gameObject.SetActive(true);
        this.transform.Find("ToSortingFacilityButtonFromLeft").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromRight").gameObject.SetActive(false);
        state = states.Facility;
    }

    public void ToTrashSortingFromLeft()
    {
        StartCoroutine(CameraRotationToTrashSortingFromLeft());
        StartCoroutine(CameraTranslationForward());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(true);
        this.transform.Find("ToEntranceButton").gameObject.SetActive(true);
        this.transform.Find("ToSortingFacilityButtonFromLeft").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromRight").gameObject.SetActive(false);
        state = states.Facility;
    }

    public void ToBlackMarket()
    {
        StartCoroutine(CameraRotationToBlackMarket());
        StartCoroutine(CameraTranslationBackwards());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(false);
        this.transform.Find("ToEntranceButton").gameObject.SetActive(false);
        this.transform.Find("ToSortingFacilityButtonFromLeft").gameObject.SetActive(true);
        state = states.BlackMarket;
    }

    public void ToEntrance()
    {
        StartCoroutine(CameraRotationToEntrance());
        StartCoroutine(CameraTranslationBackwards());
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
            targetAngle = Quaternion.Euler(xAngle, i, zAngle);
            targetPosition = new Vector3(xPos, yPos, i / 9);
            cam.transform.rotation = targetAngle;
            yield return null;
        }
    }

    private IEnumerator CameraRotationToBlackMarket()
    {

        for (float i = 0; i >= -90; i -= speed)
        {
            targetAngle = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = targetAngle;
            yield return null;
        }
        BlackMarket.Instance.transform.GetChild(0).gameObject.SetActive(true);
        BlackMarket.Instance.UpdateDemands();
    }

    private IEnumerator CameraRotationToTrashSortingFromRight()
    {

        for (float i = 90; i >= 0; i -= speed)
        {
            targetAngle = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = targetAngle;
            yield return null;
        }
    }

    private IEnumerator CameraRotationToTrashSortingFromLeft()
    {
        BlackMarket.Instance.transform.GetChild(0).gameObject.SetActive(false);
        for (float i = -90; i <= 0; i += speed)
        {
            targetAngle = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = targetAngle;
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

    private IEnumerator CameraTranslationBackwards()
    {
        for (float y = 10; y >= 0; y -= translationSpeed)
        {
            targetPosition = new Vector3(xPos, yPos, y);
            cam.transform.position = targetPosition;
            yield return null;
        }
    }

    private IEnumerator CameraTranslationForward()
    {
        for (float y = 0; y <= 10; y += translationSpeed)
        {
            targetPosition = new Vector3(xPos, yPos, y);
            cam.transform.position = targetPosition;
            yield return null;
        }
    }
}
