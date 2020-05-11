using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public TileType tileType;
    public Transform numberHolder;

    Color originalColour;
    Material material;

    int numberOfTIle;

    public List<Transform> nearTownPosition;

    public void Init(bool isSea, int number)
    {
        nearTownPosition = new List<Transform>();
        numberHolder.GetComponent<TextMesh>().text = number.ToString();
        numberOfTIle = number;
    }

    void SetColor()
    {
        switch (tileType)
        {
            case TileType.SHEEP:
                break;
            case TileType.BRICK:
                break;
            case TileType.WOOD:
                break;
            case TileType.WHEAT:
                break;
            case TileType.SEA:
                break;
            default:
                break;
        }
    }
}

