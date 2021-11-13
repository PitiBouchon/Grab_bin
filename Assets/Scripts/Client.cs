using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Category;

public class Client : MonoBehaviour
{
    private int lifeSpan;
    private CatColor askedColor;
    private CatType askedType;
    private TrashName askedTrash;
    private int demandLevel = 0;

    public Client()
    {
        int rd = Random.Range(0, 2);
        demandLevel = Random.Range(1, 4);

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
    }

    public CatColor GetRandomCatColor()
    {
        System.Random random = new System.Random();
        Type type = typeof(CatColor);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        CatColor value = (CatColor)values.GetValue(index);
        return value;
    }

    public CatType GetRandomCatType()
    {
        System.Random random = new System.Random();
        Type type = typeof(CatType);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        CatType value = (CatType)values.GetValue(index);
        return value;
    }

    public TrashName GetRandomTrashName()
    {
        System.Random random = new System.Random();
        Type type = typeof(TrashName);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        TrashName value = (TrashName)values.GetValue(index);
        return value;
    }
}
