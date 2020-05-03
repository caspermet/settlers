using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteLocalization : MonoBehaviour
{
    public Transform settlement1;
    public Transform settlement2;
    public Transform routePrefab;

    Transform route;

    public void CreateRoute()
    {
        if (route != null)
            return;

        Debug.Log("Create Route");

        route = Instantiate(routePrefab, transform.position, new Quaternion());
        route.parent = transform;

        var looAt = settlement1.position;
        transform.LookAt(looAt);
    }
}
