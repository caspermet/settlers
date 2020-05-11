using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceSettlement
{
    static Transform modifeSettlement;

    public static bool SetSettlement(Transform settlement)
    {
        modifeSettlement = settlement;

        if (!IsSomeSettlementNearly())
        {
            settlement.GetComponent<SettlementLocalization>().CreateSettlement();

        }

        return false;
    }

    private static bool IsSomeSettlementNearly()
    {
        var routes = modifeSettlement.GetComponent<SettlementLocalization>().routes;

        foreach (var route in routes)
        {
            if (IsSettlementOnRouteCreate(route))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsSettlementOnRouteCreate(RouteLocalization route)
    {
        if( route == null)
        {
            return false;
        }

        if (route.settlement1 != modifeSettlement)
        {
            return route.settlement1.GetComponent<SettlementLocalization>().isSettlementPlace;
        }
        else
        {
            return route.settlement2.GetComponent<SettlementLocalization>().isSettlementPlace;
        }
    }
}
