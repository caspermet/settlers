using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(0, 4)]
    public int numberOfPlayers;
    public List<Color> playersColors;

    private SettlementsController settlementsController;
    private PlayerController playerController;
    private GUIController gUIController;

    void Start()
    {
        playerController = new PlayerController(FindObjectOfType<MapSettings>());
        settlementsController = new SettlementsController();

        GameStateController.State = GameState.NORMAL;

        gUIController = FindObjectOfType<GUIController>();

        gUIController.OnMouseClick += NexState;
        gUIController.SetState(GameStateController.State);
    }

    private void NexState(object sender, Transform e)
    {
        playerController.NextPlayer();

        switch (GameStateController.State)
        {
            case GameState.BEFOREROLLDICE:

                break;
            case GameState.AFTERDICE:

                break;
            case GameState.ENDOFGAME:

                break;
            case GameState.WAIT:

                break;
        }
    }
}
