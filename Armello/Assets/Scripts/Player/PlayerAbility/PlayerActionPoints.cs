using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionPoints : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
    public int PlayerActionsPerTurn = 5;
    public int ActionPointsLimit = 10;

    private int currentPlayerActionPoints;
    public int GetCurrentPlayerActionPoints() => currentPlayerActionPoints;

    private void Awake()
    {
        this.ComponentInject();                
    }

    private void Start()
    {
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if(player == playerScript)
        {
            currentPlayerActionPoints -= type.Cost();
        }        
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript)
        {
            currentPlayerActionPoints = Mathf.Min(currentPlayerActionPoints + PlayerActionsPerTurn, ActionPointsLimit);
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        currentPlayerActionPoints = 0;
        if (currentPlayer == playerScript)
        {
            currentPlayerActionPoints = Mathf.Min(currentPlayerActionPoints + PlayerActionsPerTurn, ActionPointsLimit);
        }
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }
}