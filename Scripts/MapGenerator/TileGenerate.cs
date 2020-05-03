using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerate : MonoBehaviour, ITileGenerator
{
    Vector2 mapSize;
    Dictionary<Tuple<int, int>, Transform> tileMap;
    Transform mapGeneratorTransform;
    Tile tile;

    public TileGenerate(Vector2 mapSize, Transform transform, Tile tile)
    {
        this.mapSize = mapSize;
        this.mapGeneratorTransform = transform;
        this.tile = tile;
    }

    public Transform Transform { get; }

    [Obsolete]
    public Dictionary<Tuple<int, int>, Transform> Generate()
    {
        Debug.Log("Creating Map");

        int numberNonSeaTile = NumberNonSeaTiles();

        tileMap = new Dictionary<Tuple<int, int>, Transform>();

        // Create map holder object
        string holderName = "Generated Map";
        if (mapGeneratorTransform.FindChild(holderName))
        {
            DestroyImmediate(mapGeneratorTransform.FindChild(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = mapGeneratorTransform;

        for (int x = 0; x < (int)mapSize.x; x++)
        {
            for (int y = 0; y < (int)mapSize.y; y++)
            {
                if (x != (int)mapSize.x - 1 || Helper.IsOdd(y))
                {
                    Vector3 tilePosition = CoordOfTilePosition(x, y);
                    Tile newTile = Instantiate(tile, tilePosition, Quaternion.Euler(Vector3.right)) as Tile;

                    newTile.transform.parent = mapHolder;

                    tileMap.Add(Tuple.Create(x, y), newTile.transform);
                }
            }
        }

        return tileMap;
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

        return new Vector3(xLocal, 0, yLocal);
    }

    private float CalculYCoor(int y)
    {
        return y * 2 / (float)Math.Sqrt(3);
    }
}
