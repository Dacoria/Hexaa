using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FogGrid : MonoBehaviour
{
    [ComponentInject] private HexGrid hexGrid;

    private void Awake()
    {
        this.ComponentInject();       
    }

    private void Start()
    {
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.PlayerHasMoved += OnPlayerHasMoved;
    }

    private void Destroy()
    {
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerHasMoved -= OnPlayerHasMoved;
    }    

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript playersTurn)
    {
        UpdateMyFog();
    }    

    private void OnPlayerHasMoved(PlayerScript player, Hex hexDestination)
    {
        if(player.PlayerId == NetworkHelper.instance.GetMyPlayer().PlayerId)
        {
            UpdateMyFog();
        }
    }

    private void UpdateMyFog()
    {
        var myPlayer = NetworkHelper.instance.GetMyPlayer();
        var myTile = myPlayer.CurrentHexTile;

        var neighbourTiles = hexGrid.GetNeighboursFor(myTile.HexCoordinates, 1);

        var allTiles = hexGrid.GetAllTiles();
        foreach(var tile in allTiles)
        {
            var isVisibleTile = tile.HexCoordinates == myTile.HexCoordinates || neighbourTiles.Any(x => x == tile.HexCoordinates);
            tile.SetFogHighlight(!isVisibleTile);
        }
    }
}