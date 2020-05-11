using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public float tileSize = 1;
    public Vector2 mapSize;
    public SettlementLocalization townLocalization;
    public RouteLocalization routeLocalization;
    public Tile tile;

    public List<Tile> typeOftiles;

    public Tile dessert;


    ITileGenerator tileGenerator;
    ITileGenerator settlementGenerotor;
    ITileGenerator routeGenerator;

    Dictionary<Tuple<int, int>, Transform> tileMap;
    Dictionary<Tuple<int, int>, Transform> settlementMap;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {    
        tileGenerator = new TileGenerate(mapSize, transform, typeOftiles);
        tileMap = tileGenerator.Generate();

        settlementGenerotor = new SettlementGenerator(mapSize, transform, townLocalization, tileMap);
        settlementMap = settlementGenerotor.Generate();

        routeGenerator = new RouteGenerator(settlementMap, transform, routeLocalization);
        routeGenerator.Generate();
    }
}
