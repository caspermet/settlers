using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings : MonoBehaviour
{
    public Transform settlementPrefab;
    public Transform cityPrefab;
    public Transform routePrefab;

    public int numberOfPlayer;
    public int maxNumberOfRoute;
    public int maxNumberOfSettlements;
    public int maxNumberOfCity;

    public Color[] PlayersColors;
}

