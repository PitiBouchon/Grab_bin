using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    private Camera cam;
    private float xAngle;
    private float zAngle;
    private Quaternion target;

    private void Start()
    {
        cam = Camera.main;
        xAngle = cam.transform.rotation.eulerAngles.x;
        zAngle = cam.transform.rotation.eulerAngles.z;
    }
    public void ToTrashSorting()
    {
        StartCoroutine(CameraToTrashSorting());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(true);
        this.transform.Find("ToTrashSortingButton").gameObject.SetActive(false);
    }

    public void ToBlackMarket()
    {
        StartCoroutine(CameraToBlackMarket());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(false);
        this.transform.Find("ToTrashSortingButton").gameObject.SetActive(true);
    }

    private IEnumerator CameraToBlackMarket()
    {

        for (float i = 0; i <= 180; i += speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
    }

    private IEnumerator CameraToTrashSorting()
    {

        for (float i = 180; i >= 0; i -= speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
    }
}
