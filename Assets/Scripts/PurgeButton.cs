using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgeButton : MonoBehaviour
{
    private CollectionManager collectionManager;
    private AudioSource audioData;
    private void Start()
    {
        collectionManager = CollectionManager.Instance;
        audioData = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        collectionManager.PurgeCOllection();
        audioData.Play();
    }
}
