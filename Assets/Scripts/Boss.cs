using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float translationSpeed = 0.5f;
    private StressBar stressManager;
    private GameObject collection;
    private GameObject trashManager;
    private List<GameObject> collectedTrash;
    private GameObject bossPlane;
    private Vector3 bossStartingPoint;
    private Vector3 trashSpawnPoint;
    private float unhappiness;
    private float dayLength;
    private int nbOfVisits;
    private int cooldown;
    [SerializeField]
    private AudioSource stepsSounds;
    [SerializeField]
    private AudioSource pasContent;
    Vector3 bossPosition;



    private void Start()
    {
        stressManager = GameObject.Find("StressManager").GetComponent<StressBar>();
        unhappiness = stressManager.curStress;
        collection = GameObject.Find("CollectedItemDetector");
        collectedTrash = collection.GetComponent<CollectionManager>().collectedTrash;
        trashManager = GameObject.Find("TrashManager");
        trashSpawnPoint = trashManager.GetComponent<TrashManager>().spawn_pos;
        dayLength = trashManager.GetComponent<GameManager>().dayLength;
        bossPlane = GameObject.Find("BossPlane");
        bossStartingPoint = bossPlane.transform.position;
        bossStartingPoint.z = 17;
        bossPosition = bossStartingPoint;
        nbOfVisits = (int)dayLength / 25;
        cooldown = (int) dayLength / nbOfVisits;
        InvokeRepeating("SpawnBoss", cooldown, cooldown);
    }

    private void SpawnBoss()
    {
        stepsSounds.Play();
        
        StartCoroutine(delay());
        
    }

    private void BossIsMad()
    {
        pasContent.Play();
        Debug.Log("Boss is pas content");
        
        foreach(GameObject collectedItem in collectedTrash)
        {
            collectedItem.transform.position = trashSpawnPoint;
            collectedItem.layer = 6;
        }
        stressManager.SetCurStress(stressManager.curStress + 10);
        collectedTrash = new List<GameObject>();
    }

    private IEnumerator delay()
    {
        for (float z = 17; z >= -50; z -= translationSpeed)
        {
            Debug.Log("On translate");
            bossPosition.z = z;
            bossPlane.transform.position = bossPosition;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3);
        Debug.Log("Boss is coming");
        if (collectedTrash.Count != 0)
        {
            BossIsMad();
        }
        bossPlane.transform.position = bossStartingPoint;
    }
}
