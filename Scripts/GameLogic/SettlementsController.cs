using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementsController : MonoBehaviour
{
    Transform activeSettlement;

    public SettlementsController()
    {
        SetSettlementsEvent();
    }

    private void SetSettlementsEvent()
    {
        var settlements = FindObjectsOfType<SettlementLocalization>();

        foreach (var settlement in settlements)
        {
            settlement.OnMouseClick += OnSettlementClick;
        }
    }

    private void OnSettlementClick(object sender, Transform e)
    {
        if ((activeSettlement != null && activeSettlement != e) || activeSettlement == null && GameStateController.State != GameState.NORMAL)
            return;

        if (GameStateController.activePlayer.mySettlements.Count >= FindObjectOfType<MapSettings>().maxNumberOfSettlements)
        {
            return;
        }
        else if (e == activeSettlement)
        {
            e.GetComponent<SettlementLocalization>().CreateSettlement();
            activeSettlement = null;
            GameStateController.State = GameState.NORMAL;
            FindObjectOfType<GUIController>().OnWaitClick -= DeactivePreCreateSettlement;
        }
        else
        {
            e.GetComponent<SettlementLocalization>().PreCreateSettlement();
            activeSettlement = e;
            GameStateController.State = GameState.WAIT;
            FindObjectOfType<GUIController>().OnWaitClick += DeactivePreCreateSettlement;
        }
    }

    private void DeactivePreCreateSettlement(object sender, EventArgs e)
    {
        GameStateController.State = GameState.NORMAL;
        activeSettlement.GetComponent<SettlementLocalization>().DeletePreCreatingSettlement();
        activeSettlement = null;

        FindObjectOfType<GUIController>().OnWaitClick -= DeactivePreCreateSettlement;
    }
}
