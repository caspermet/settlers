using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public event EventHandler<Transform> OnMouseClick;
    public event EventHandler OnWaitClick;

    private GameState state;

    private void Start()
    {
        GameStateController.OnGameStateChange += OnChangeEvenet;
    }

    private void OnChangeEvenet(object sender, GameState state)
    {
        SetState(state);
    }

    public void NextState()
    {
        Debug.Log("next state");

        switch (state)
        {
            case GameState.BEFOREROLLDICE:
                break;
            case GameState.AFTERDICE:
                break;
            case GameState.ENDOFGAME:
                break;
            case GameState.WAIT:
                OnWaitClick?.Invoke(this, EventArgs.Empty);
                break;
            case GameState.NORMAL:
                OnMouseClick?.Invoke(this, transform);
                break;
        }
    }

    public void SetState(GameState state)
    {
        this.state = state;

        switch (state)
        {
            case GameState.BEFOREROLLDICE:
                break;
            case GameState.AFTERDICE:              
                break;
            case GameState.ENDOFGAME:
                break;
            case GameState.WAIT:
                ChangeName("X");
                break;
            case GameState.NORMAL:
                ChangeName("N");
                break;
        }
    }

    private void ChangeName(string name)
    {
        var nextStateButton = transform.Find("NextState");
        nextStateButton.GetComponentInChildren<Text>().text = name;
    }

    public void ChangeCollor()
    {
        transform.Find("NextState").GetComponentInChildren<Text>().color = GameStateController.activePlayer.color;
    }
}
