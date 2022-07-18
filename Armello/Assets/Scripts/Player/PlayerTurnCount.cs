using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurnCount : MonoBehaviour
{   
    [ComponentInject] private PlayerScript playerScript;

    public int TurnCount;

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
        TurnCount = 0;
        if(playerScript == currentPlayer)
        {
            TurnCount++;
        }
    }

    private void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        if (playerScript == currentPlayer)
        {
            TurnCount++;
        }
    }
}
