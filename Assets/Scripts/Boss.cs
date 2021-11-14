using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private StressBar stressManager;
    private GameObject collection;
    private GameObject trashManager;
    private List<GameObject> collectedTrash;
    private Vector3 trashSpawnPoint;
    private float unhappiness;
    private float dayLength;
    private int nbOfVisits;
    private int cooldown;

    private void Start()
    {
        stressManager = GameObject.Find("StressManager").GetComponent<StressBar>();
        unhappiness = stressManager.curStress;
        collection = GameObject.Find("CollectedItemDetector");
        collectedTrash = collection.GetComponent<CollectionManager>().collectedTrash;
        trashManager = GameObject.Find("TrashManager");
        trashSpawnPoint = trashManager.GetComponent<TrashManager>().spawn_pos;
        dayLength = trashManager.GetComponent<GameManager>().dayLength;

        nbOfVisits = (int)dayLength / 25;
        cooldown = (int) dayLength / nbOfVisits;
        InvokeRepeating("SpawnBoss", cooldown, cooldown);
    }

    private void SpawnBoss()
    {
        Debug.Log("Boss is coming");
        if (collectedTrash.Count != 0)
        {
            BossIsMad();
        }
    }

    private void BossIsMad()
    {
        Debug.Log("Boss is pas content");
        foreach(GameObject collectedItem in collectedTrash)
        {
            collectedItem.transform.position = trashSpawnPoint;
            collectedItem.layer = 6;
        }
        collectedTrash = new List<GameObject>();
    }



}
