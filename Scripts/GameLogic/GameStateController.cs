using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameStateController
{
    public static Player activePlayer;
    public static bool isStart;

    public static event EventHandler<GameState> OnGameStateChange;

    public static GameState State
    {
        get
        {
            return State;
        }
        set
        {
            State = value;
            OnGameStateChange?.Invoke(null, value);
        }
    }
}
