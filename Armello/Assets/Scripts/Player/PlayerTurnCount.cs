using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurnCount : MonoBehaviour
{   
    [ComponentInject] private PlayerScript playerScript;

    private int turnCount;
    public int GetTurnCount() => turnCount;

    private void Awake()
    {
        this.ComponentInject();
    }

    private void Start()
    {
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
    }

    private void OnDestroy()
    {
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        turnCount = 0;
        if(playerScript == currentPlayer)
        {
            turnCount++;
        }
    }

    private void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        if (playerScript == currentPlayer)
        {
            turnCount++;
        }
    }
}
