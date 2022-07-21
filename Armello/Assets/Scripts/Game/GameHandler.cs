using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    private HexGrid HexGrid;
    public static GameHandler instance;
    public GameStatus GameStatus;

    private void Awake()
    {
        instance = this;
        GameStatus = GameStatus.NotStarted;
    }

    private void Start()
    {
        HexGrid = FindObjectOfType<HexGrid>();
        ActionEvents.PlayerRocketHitTile += OnPlayerRocketHitTile;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.EndRound += OnEndRound;
        ActionEvents.EndGame += OnEndGame;
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        CurrentPlayer = player;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.EndRound -= OnEndRound;
    }

    private void OnPlayerRocketHitTile(PlayerScript ownerRocket, Hex hex, PlayerScript playerHit, bool playerKilled)
    {
        if (playerKilled)
        {
            EndGameOnRocketHit();
        }        
    }    
}