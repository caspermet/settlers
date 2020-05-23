using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteLocalization : MonoBehaviour
{
    public Transform settlement1;
    public Transform settlement2;
    public Transform routePrefab;
    public float scale { get; set; }

    Transform route;

    private void Start()
    {
        var mapSetting = FindObjectOfType<MapSettings>();

        routePrefab = mapSetting.routePrefab;
    }

    public RouteLocalization(float scale)
    {
        this.scale = scale;

    }

    public void CreateRoute()
    {
        if (route != null)
            return;

        Debug.Log("Create Route");

        route = Instantiate(routePrefab, transform.position, new Quaternion());
        route.parent = transform;
        route.transform.localScale *= scale;

        var looAt = settlement1.position;
        transform.LookAt(looAt);
    }
}
