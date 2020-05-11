using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementGenerator : MonoBehaviour, ITileGenerator
{
    const string settlementName = "Settlement";
   
    Vector2 mapSize;
    Dictionary<Tuple<int, int>, Transform> tileMap;
    Transform mapGeneratorTransform;
    SettlementLocalization townLocalization;
    Dictionary<Tuple<int, int>, Transform> settlementMap;

    public SettlementGenerator(Vector2 mapSize, Transform transform, SettlementLocalization townLocalization, Dictionary<Tuple<int, int>, Transform> tileMap)
    {
        this.mapSize = mapSize;
        this.mapGeneratorTransform = transform;
        this.townLocalization = townLocalization;
        this.tileMap = tileMap;

        settlementMap = new Dictionary<Tuple<int, int>, Transform>();
    }

    [Obsolete]
    public Dictionary<Tuple<int, int>, Transform> Generate()
    {
        Debug.Log("Creating settlers places");       

        string holderName = "Generated Settler";
        if (mapGeneratorTransform.Find(holderName))
        {
            DestroyImmediate(mapGeneratorTransform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = mapGeneratorTransform;

        float yShift = -2 / (float)Math.Sqrt(3) * 0.5f;
        float yShiftValueOdd = (float)Math.Sqrt(Math.Pow(1 / (float)Math.Sqrt(3), 2) - Math.Pow(0.5f, 2));
        int maxYCoord = (int)mapSize.y * 2 + 2;

        for (int y = 0; y < maxYCoord; y++)
        {
            for (int x = 0; x <= (int)mapSize.x; x++)
            {
                if (!((y == 0 && x == 0) || (y == 0 && x == (int)mapSize.x) || (y == maxYCoord - 1 && x == 0) || (y == maxYCoord - 1 && x >= (int)mapSize.x && Helper.IsOdd(y)) || ((y % 4 == 1 || y % 4 == 2) && x > (int)mapSize.x - 1)))
                {
                    Vector3 tilePosition = CoordOfSettler(x, y, yShift);
                    SettlementLocalization settlement = Instantiate(townLocalization, tilePosition, Quaternion.Euler(Vector3.right)) as SettlementLocalization;

                    settlement.transform.parent = mapHolder;
                    settlement.name = settlementName;

                    settlementMap.Add(Tuple.Create(x, y), settlement.transform);
                    ConnectSettlementToAllTiles(x, y, settlement);
                }
            }

            if (!Helper.IsOdd(y))
            {
                yShift += yShiftValueOdd;
            }
            else
            {
                yShift += 1 / (float)Math.Sqrt(3);
            }
        }
        return settlementMap;
    }

    private void ConnectSettlementToAllTiles(int x, int y, SettlementLocalization settlement)
    {
        int nearTileY = y / 2;
        int oddShift = 0;

        if (!Helper.IsOdd(y))
        {          
            if (y % 4 == 0)
                oddShift = -1;

            AddSettlementToTile(x, nearTileY - 1, settlement.transform);
            AddSettlementToTile(x - 1, nearTileY - 1, settlement.transform);
            AddSettlementToTile(x + oddShift, nearTileY, settlement.transform);
        }
        else
        {           
            if (y % 4 == 1)
                oddShift = 1;

            AddSettlementToTile(x - 1 + oddShift, nearTileY - 1, settlement.transform);
            AddSettlementToTile(x - 1, nearTileY, settlement.transform);
            AddSettlementToTile(x, nearTileY, settlement.transform);
        }
    }

    private void AddSettlementToTile(int x, int y, Transform settlerTransfom)
    {
        var key = Tuple.Create(x, y);

        if (tileMap.ContainsKey(key))
        {
            tileMap[key].GetComponent<Tile>().nearTownPosition.Add(settlerTransfom);
        }
    }

    private Vector3 CoordOfSettler(int x, int y, float yShift)
    {
        float xLocal = x - 1.0f;
        float yLocal = yShift;


        if (y % 4 == 1 || y % 4 == 2)
        {
            xLocal = x - 0.5f;
        }

        return new Vector3(xLocal, 0.232f, yLocal);
    }
}
