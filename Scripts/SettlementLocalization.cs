using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementLocalization : MonoBehaviour
{
    public List<RouteLocalization> routes { get; set; }
    public Transform settlementPrefab;
    public Transform cityPrefab;

    public bool isSettlementPlace;

    Transform settlement;
    Transform city;

    public SettlementLocalization()
    {
        routes = new List<RouteLocalization>();
        isSettlementPlace = false;
    }

    public void CreateSettlement()
    {
        if (settlement != null)
            return;

        Debug.Log("Create Settlement");

        settlement = Instantiate(settlementPrefab, transform.position, new Quaternion());
        settlement.parent = transform;

        isSettlementPlace = true;
    }

}
