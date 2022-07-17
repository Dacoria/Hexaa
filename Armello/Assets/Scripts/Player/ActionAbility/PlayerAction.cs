using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
    private int PlayerActionsPerTurn = 5;

    public List<PlayerAbility> PlayerAbilitiesInTurn;
        
    private void Awake()
    {
        this.ComponentInject();

        PlayerAbilitiesInTurn = new List<PlayerAbility>();
        foreach (AbilityType abilityType in Enum.GetValues(typeof(AbilityType)))
        {
            PlayerAbilitiesInTurn.Add(new PlayerAbility { AbilityType = abilityType });
        }        
    }

    public int GetPointsLeft() => PlayerActionsPerTurn - PlayerAbilitiesInTurn.Sum(x => x.GetCost());     

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
            PlayerAbilitiesInTurn.Single(x => x.AbilityType == type).CountFiredThisTurn++;
        }        
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript)
        {
            ResetAbilitiesCurrentPlayer(player);
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        if (currentPlayer == playerScript)
        {
            ResetAbilitiesCurrentPlayer(currentPlayer);
        }
    }

    private void ResetAbilitiesCurrentPlayer(PlayerScript player)
    {
        // maakt niet uit welke netwerk of speler of ai dit is; mag altijd wel
        PlayerAbilitiesInTurn.ForEach(x => x.CountFiredThisTurn = 0);        
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }
}

public class PlayerAbility
{
    public AbilityType AbilityType;
    public int CountFiredThisTurn;

    public int GetCost() => AbilityType.Cost() * CountFiredThisTurn;
}