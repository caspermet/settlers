using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameStateController
{
    private static GameState state;
    private static Player activePlayer;

    public static bool isStart;

    public static event EventHandler<GameState> OnGameStateChange;
    public static event EventHandler OnActivePlayerChange;

    public static GameState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            OnGameStateChange?.Invoke(null, value);
        }
    }

    public static Player ActivePlayer
    {
        get
        {
            return activePlayer;
        }
        set
        {
            Debug.Log("tesda");
            activePlayer = value;
            OnActivePlayerChange?.Invoke(null, EventArgs.Empty);
        }
       
    }
}
