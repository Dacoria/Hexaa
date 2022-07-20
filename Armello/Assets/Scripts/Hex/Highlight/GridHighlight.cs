using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    private void Awake()
    {
        this.ComponentInject();       
    }

    private void Start()
    {
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
    }    

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
    }

    private void OnNewPlayerTurn(PlayerScript playersTurn)
    {
        if (playersTurn.IsOnMyNetwork())
        {
            ClearAllHighlightsOnGrid();
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript arg2)
    {
        ClearAllHighlightsOnGrid();
    }  

    private void ClearAllHighlightsOnGrid()
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlight();
        }
    }
}