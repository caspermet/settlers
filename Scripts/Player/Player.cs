using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color color;
    public int order;

    public Transform[] notSetSettlement;
    public Transform[] notSetCity;
    public Transform[] notSetRoute;

    public List<SettlementLocalization> mySettlements;

    public Player(Color color, int order)
    {
        this.color = color;
        this.order = order;

        mySettlements = new List<SettlementLocalization>();
    }
}
