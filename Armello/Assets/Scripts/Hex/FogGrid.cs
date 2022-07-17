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
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.PlayerHasMoved += OnPlayerHasMoved;
    }    

    private void OnDestroy()
    {
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.PlayerHasMoved -= OnPlayerHasMoved;
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript playersTurn)
    {
        UpdateVisibility(playersTurn);
    }

    private void OnNewPlayerTurn(PlayerScript playersTurn)
    {
        UpdateVisibility(playersTurn);
    }       

    private void OnPlayerHasMoved(PlayerScript player, Hex hexDestination)
    {
        if(player.PlayerId == Netw.MyPlayer().PlayerId)
        {
            UpdateVisibility(player);
        }
    }

    private void UpdateVisibility(PlayerScript currentTurnPlayer)
    {
        // geen ai of andere dingen? dan update je alleen je eigen fog, ook al ben je niet aan de beurt. Anders: Toon van wie er aan de beurt is
        var playerToUpdate = currentTurnPlayer.IsOnMyNetwork() ? currentTurnPlayer : Netw.CurrPlayer();

        UpdateMyFog(playerToUpdate);
        UpdatePlayersVisibleInMyFog(playerToUpdate);
    }   

    private void UpdateMyFog(PlayerScript currentTurnPlayer)
    {
        var currPlayerTile = currentTurnPlayer.CurrentHexTile;
        var neighbourTiles = hexGrid.GetNeighboursFor(currPlayerTile.HexCoordinates, 1);

        var allTiles = hexGrid.GetAllTiles();
        foreach(var tile in allTiles)
        {
            var isVisibleTile = tile.HexCoordinates == currPlayerTile.HexCoordinates || neighbourTiles.Any(x => x == tile.HexCoordinates);
            tile.SetFogHighlight(!isVisibleTile);
        }
    }

    private void UpdatePlayersVisibleInMyFog(PlayerScript currentPlayer)
    {
        foreach (var player in GameHandler.instance.AllPlayers)
        {
            var playerIsInFog = player.CurrentHexTile.GetComponent<FogHighlight>().isFogActive;
            player.GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(!playerIsInFog);
        }
    }
}