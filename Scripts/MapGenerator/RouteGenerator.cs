using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteGenerator : MonoBehaviour, ITileGenerator
{
    float scale;
    Dictionary<Tuple<int, int>, Transform> settlementPosition;
    RouteLocalization routePrefab;
    Transform mapGeneratorTransform;
    Transform mapHolder;
    static string routeName = "Route";

    string holderName = "Generate route";
    public RouteGenerator(Dictionary<Tuple<int, int>, Transform> settlementPosition, Transform transform, RouteLocalization route, float scale)
    {
        this.settlementPosition = settlementPosition;
        this.routePrefab = route;
        mapGeneratorTransform = transform;
        this.scale = scale;
    }

    public Dictionary<Tuple<int, int>, Transform> Generate()
    {
        Debug.Log("Creating settlers places");

        if (mapGeneratorTransform.Find(holderName))
        {
            DestroyImmediate(mapGeneratorTransform.Find(holderName).gameObject);
        }

        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = mapGeneratorTransform;       
        mapHolder.position = new Vector3(0, 0, 0);


        foreach (var settlement in settlementPosition)
        {
            int x = settlement.Key.Item1;
            int y = settlement.Key.Item2;
            int offset = 0;

            if (Helper.IsOdd(y))
            {
                if (y % 4 == 3)
                    offset = -1;
                AddRouteToSettlement(x + offset,        y - 1, settlement.Value);
                AddRouteToSettlement(x + 1 + offset,    y - 1, settlement.Value);
                AddRouteToSettlement(x,                 y + 1, settlement.Value);
            }
            else
            {
                if (y % 4 == 0)
                    offset = -1;
                
                AddRouteToSettlement(x,                 y - 1, settlement.Value);
                AddRouteToSettlement(x + 1 + offset,    y + 1, settlement.Value);
                AddRouteToSettlement(x + offset,        y + 1, settlement.Value);
            }
        }
        return null;
    }

    private bool IsSettlementExist(int x, int y)
    {
        var key = Tuple.Create(x, y);

        if (settlementPosition.ContainsKey(key))      
           return true;
        return false;
        
    }

    private void AddRouteToSettlement(int x, int y, Transform settlement)
    {
        var key = Tuple.Create(x, y);
        
        if (settlementPosition.ContainsKey(key))
        {

            foreach (var item in settlementPosition[key].gameObject.GetComponent<SettlementLocalization>().routes)
            {
                if( item != null && (item.settlement1 == settlement || item.settlement2 == settlement))
                {
                    return;
                }
            }

            Vector3 position = (settlement.position - settlementPosition[key].position) / 2 + settlementPosition[key].position;
            position.y = 0.232f * scale;
            RouteLocalization route = Instantiate(routePrefab, position, Quaternion.Euler(Vector3.right)) as RouteLocalization;

            settlement.gameObject.GetComponent<SettlementLocalization>().routes.Add(route);
            settlementPosition[key].gameObject.GetComponent<SettlementLocalization>().routes.Add(route);           

            route.transform.parent = mapHolder;
            route.settlement1 = settlement;
            route.settlement2 = settlementPosition[key];
            route.name = routeName;
            route.transform.localScale *= scale;
            route.scale = scale;
        }
        else
        {
            settlement.gameObject.GetComponent<SettlementLocalization>().routes.Add(null);
        }        
    }
}
