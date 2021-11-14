using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;
using UnityEngine.UI;
using TMPro;

public class Client : MonoBehaviour
{
    private int lifeSpan;
    private CatColor? askedColor = null;
    private CatType? askedType = null;
    private TrashName? askedTrash = null;
    private int reward;
    private string voiceLine;
    private int demandLevel ;
    public GameObject demand;

    public Client(int minLifespan, int maxLifeSpan, int baseReward)
    {
        generateReward(baseReward);
        lifeSpan = Random.Range(minLifespan, maxLifeSpan + 1);
        demandLevel = Random.Range(1, 4);

        int rd = Random.Range(0, 2);
        if (demandLevel == 1)
        {
            // Color or Type
            if (rd == 0)
            {
                // Color
                askedColor = GetRandomCatColor();
            }
            else
            {
                // Type
                askedType = GetRandomCatType();
            }
        }
        else if (demandLevel == 2)
        {
            // Color and Type or specific item
            if (rd == 0)
            {
                askedColor = GetRandomCatColor();
                askedType = GetRandomCatType();
            }
            else
            {
                askedTrash = GetRandomTrashName();
            }
        }
        else
        {
            // demandLevel == 3: specfic item and color or type
            askedTrash = GetRandomTrashName();
            askedColor = GetRandomCatColor();
        }

        voiceLine = generateTextMessage();
        demand = GameObject.Find("Demand");
        print(demand);
        print(voiceLine);
        //print(voiceLine);
        //demand.GetComponent<TextMeshPro>().text = voiceLine;
        TextMeshProUGUI textmeshPro = demand.GetComponent<TextMeshProUGUI>();
        Debug.Log("OI" + demand + " | " + textmeshPro);
        // Debug.Log(demand.GetComponents(TextMeshPro));
        textmeshPro.SetText(voiceLine);
        //demand.BroadcastMessage(voiceLine);


    }

    private CatColor GetRandomCatColor()
    {
        System.Random random = new System.Random();
        Type type = typeof(CatColor);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        CatColor value = (CatColor)values.GetValue(index);
        return value;
    }

    private CatType GetRandomCatType()
    {
        System.Random random = new System.Random();
        Type type = typeof(CatType);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        CatType value = (CatType)values.GetValue(index);
        return value;
    }

    private TrashName GetRandomTrashName()
    {
        System.Random random = new System.Random();
        Type type = typeof(TrashName);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        TrashName value = (TrashName)values.GetValue(index);
        return value;
    }

    private void generateReward(int baseReward)
    {
        reward = baseReward * demandLevel;
        int rdm = Random.Range(-1 * reward * 20 / 100, reward * 20 / 100);
        reward += rdm;
    }

    private string generateTextMessage()
    {
        voiceLine = "Hello! I would like ";
        if (askedTrash != null)
        {
            voiceLine += "a " + askedTrash.ToString() + " ";
        }
        else
        {
            voiceLine += "an item ";
        }
        if (askedColor != null && askedType == null)
        {
            voiceLine += "of color " + askedColor.ToString();
        }
        else if (askedColor == null && askedType != null)
        {
            voiceLine += "of type " + askedType.ToString();
        }
        else if (askedType != null && askedColor != null)
        {
            voiceLine += "of color " + askedColor.ToString() + " and of type " + askedType.ToString();
        }
        return voiceLine;
    }
}
