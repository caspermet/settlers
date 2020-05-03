using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    TileType tileType;
    Color originalColour;
    Material material;
    public List<Transform> nearTownPosition;

    public void Init(bool isSea)
    {
        nearTownPosition = new List<Transform>();
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

