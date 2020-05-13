using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class TileGenerate : MonoBehaviour, ITileGenerator
{
    Vector2 mapSize;
    Dictionary<Tuple<int, int>, Transform> tileMap;
    Transform mapGeneratorTransform;
    List<Tile> allTile;

    public TileGenerate(Vector2 mapSize, Transform transform, List<Tile> allTile)
    {
        this.mapSize = mapSize;
        this.mapGeneratorTransform = transform;
        this.allTile = allTile;
    }

    public Transform Transform { get; }

    [Obsolete]
    public Dictionary<Tuple<int, int>, Transform> Generate()
    {
        Debug.Log("Creating Map");

        int numberNonSeaTile = NumberNonSeaTiles();


        tileMap = new Dictionary<Tuple<int, int>, Transform>();
        List<Tile> allTile = RandomGenerateTile();
        int[] numbersOfTIles = NumberGenerator.GenerateNumber(allTile.Count);

        // Create map holder object
        string holderName = "Generated Map";
        if (mapGeneratorTransform.Find(holderName))
        {
            DestroyImmediate(mapGeneratorTransform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = mapGeneratorTransform;
        mapHolder.position = new Vector3(0, 0, 0);

        int k = 0;
        Tile tile;
        Debug.Log("generate Tiles");
        for (int x = 0; x < (int)mapSize.x; x++)
        {
            for (int y = 0; y < (int)mapSize.y; y++)
            {
                if (x != (int)mapSize.x - 1 || Helper.IsOdd(y))
                {
                    tile = allTile[k];
                    Vector3 tilePosition = CoordOfTilePosition(x, y);
                    
                    Tile newTile = Instantiate(tile, tilePosition, Quaternion.Euler(Vector3.right)) as Tile;

                    newTile.transform.parent = mapHolder;
                    newTile.Init(false, numbersOfTIles[k]);

                    tileMap.Add(Tuple.Create(x, y), newTile.transform);
                    k++;
                }
            }
        }

        return tileMap;
    }

    private List<Tile> RandomGenerateTile()
    {
        Debug.Log("Random Tiles");
        //plusDesert
        int offset = 0;
        if (Helper.IsOdd((int)mapSize.y))
        {
            offset = 1;
        }

        int numberOfTile = (int)(mapSize.x * mapSize.y) - (int)mapSize.y / 2 - offset;
        allTile.Shuffle();
        List<Tile> newList = new List<Tile>();
        int k = 0;

        for (int i = 0; i < numberOfTile; i++)
        {
            newList.Add(allTile[k]);
            k++;

            if (k == allTile.Count)
                k = 0;
        }

        newList.Shuffle();

        return newList;

    }

    private int NumberNonSeaTiles()
    {
        return (int)(mapSize.x * mapSize.y - mapSize.x * 2 - mapSize.y * 2 - mapSize.x / 2);
    }

    private Vector3 CoordOfTilePosition(int x, int y)
    {
        float xLocal = x;
        float yLocal = CalculYCoor(y) - 0.25f * CalculYCoor(y);


        if (Helper.IsOdd(y))
        {
            xLocal -= 0.5f;
        }

        return Helper.ShiftObject(new Vector3(xLocal, 0, yLocal), mapSize);
    }

    private float CalculYCoor(int y)
    {
        return y * 2 / (float)Math.Sqrt(3);
    }
}
