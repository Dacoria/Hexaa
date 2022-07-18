using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
    public int PlayerActionsPerTurn = 5;
    public int ActionPointsLimit = 10;

    public int CurrentPlayerActionPoints;


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
            CurrentPlayerActionPoints -= type.Cost();
        }        
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript)
        {
            CurrentPlayerActionPoints = Mathf.Min(CurrentPlayerActionPoints + PlayerActionsPerTurn, ActionPointsLimit);
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentPlayerActionPoints = 0;
        if (currentPlayer == playerScript)
        {
            CurrentPlayerActionPoints = Mathf.Min(CurrentPlayerActionPoints + PlayerActionsPerTurn, ActionPointsLimit);
        }
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }
}