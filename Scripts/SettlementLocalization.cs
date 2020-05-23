
using System;
using System.Collections.Generic;
using UnityEngine;

public class SettlementLocalization : MonoBehaviour
{
    public List<RouteLocalization> routes { get; set; }   

    public Gradient hoverRingGradient;
    public Gradient defaultRingGradient;

    public bool isSettlementPlace;
    public bool isSettlementInPrePlace;
    public float scale { get; set; }

    public float lerpTime;

    private Transform settlementPrefab;
    private Transform cityPrefab;

    public Player owner;

    Transform settlement;
    Transform city;

    Vector3 startMarker;
    Vector3 endMarker;
    public float speed = 1.0F;

    public event EventHandler<Transform> OnMouseClick;

    private void Start()
    {
        var mapSetting = FindObjectOfType<MapSettings>();
        settlementPrefab = mapSetting.settlementPrefab;
        cityPrefab = mapSetting.cityPrefab;
    }

    private void Update()
    {
        if (isSettlementInPrePlace)
        {
            settlement.transform.localPosition = Vector3.Lerp(endMarker, startMarker, Mathf.PingPong(Time.time, 1.0f));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                Debug.Log("Settlement click");
                OnMouseClick?.Invoke(this, transform);
            }
        }
    }

    public SettlementLocalization()
    {
        routes = new List<RouteLocalization>();
        isSettlementPlace = false;
        isSettlementInPrePlace = false;
    }
    public void CreateSettlement()
    {
        Debug.Log("Create Settlement");

        settlement.transform.localPosition = Vector3.zero;
        isSettlementInPrePlace = false;
        isSettlementPlace = true;

        owner = GameStateController.ActivePlayer;
        GameStateController.ActivePlayer.mySettlements.Add(this);
    }

    public void PreCreateSettlement()
    {
        if (settlement != null)
            return;

        Debug.Log("Create PreSettlement");
        settlement = Instantiate(settlementPrefab, transform.position, new Quaternion());
        settlement.parent = transform;
        settlement.GetComponent<Renderer>().material.color = GameStateController.ActivePlayer.color;
        isSettlementInPrePlace = true;

        startMarker = new Vector3(0, 0.1f, 0);
        endMarker = new Vector3(0, 0.2f, 0);
    }

    public void DeletePreCreatingSettlement()
    {
        isSettlementInPrePlace = false;
        Destroy(settlement.gameObject);
        settlement = null;
    }

    public void SetActiveRing(bool isActive)
    {
        Transform childTrans = transform.Find("Ring");
        childTrans.gameObject.SetActive(isActive);
    }
}
