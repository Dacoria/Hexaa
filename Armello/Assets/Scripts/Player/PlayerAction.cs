using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
        
    private void Awake()
    {
        this.ComponentInject();        
    }

    private void Start()
    {
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        ResetAbilitiesCurrentPlayer(player);
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        ResetAbilitiesCurrentPlayer(currentPlayer);
    }

    private void ResetAbilitiesCurrentPlayer(PlayerScript player)
    {
        player.HasDoneMovementThisTurn = false;
        player.HasFiredRocketThisTurn = false;
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
    }

}