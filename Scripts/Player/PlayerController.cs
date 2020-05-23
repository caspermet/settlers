using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController
{
    private Player[] players;
    MapSettings mapSetting;

    public PlayerController(MapSettings mapSetting)
    {
        this.mapSetting = mapSetting;
        players = new Player[mapSetting.numberOfPlayer];

        CreatePlayers();
    }

    private void CreatePlayers()
    {
        List<Color> shufflePlayersColors = mapSetting.PlayersColors.ToList();
        shufflePlayersColors.Shuffle();

        for (int i = 0; i < mapSetting.numberOfPlayer; i++)
        {
            Debug.Log("Creating player number of " + (i + 1));
            CreatePlayer(i, shufflePlayersColors);
        }
    }

    private void CreatePlayer(int i, List<Color> shufflePlayersColors)
    {       
        Player player = new Player(shufflePlayersColors[i], i);
        players[i] = player;

        if (i == 0)
        {
            GameStateController.ActivePlayer = player;            
        }
    }

    public void NextPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (GameStateController.ActivePlayer.order == players[i].order)
            {
                if (i + 1 == players.Length)
                {
                    GameStateController.ActivePlayer = players[0];
                }
                else
                {
                    GameStateController.ActivePlayer = players[i + 1];
                }

                return;
            }
        }
    }
}
