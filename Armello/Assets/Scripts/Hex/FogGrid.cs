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
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }    

    private void OnDestroy()
    {
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }

    private void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript playersTurn)
    {
        // initiele setup --> daarna OnNewPlayerTurn voor de beurt updaten
        UpdateVisibility(Netw.MyPlayer());
    }

    private void OnNewPlayerTurn(PlayerScript playersTurn)
    {
        if (playersTurn.IsOnMyNetwork())
        {
            UpdateVisibility(playersTurn);
        }
    }       

    private void OnPlayerAbility(PlayerScript player, Hex hexDestination, AbilityType abilityType)
    {
        if(abilityType == AbilityType.Movement && player.IsOnMyNetwork())
        {
            UpdateVisibility(player);
        }
    }

    private void UpdateVisibility(PlayerScript player)
    {
        UpdateMyFog(player);
        UpdatePlayersVisibleInMyFog(player);
    }   

    private void UpdateMyFog(PlayerScript player)
    {
        var currPlayerTile = player.CurrentHexTile;
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