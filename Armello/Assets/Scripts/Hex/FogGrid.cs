using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
        StartCoroutine(UpdateVisibility(Netw.MyPlayer()));
    }

    private void OnNewPlayerTurn(PlayerScript playersTurn)
    {
        if (playersTurn.IsOnMyNetwork())
        {
            StartCoroutine(UpdateVisibility(playersTurn));
        }
    }       

    private void OnPlayerAbility(PlayerScript player, Hex hexDestination, AbilityType abilityType)
    {
        if(abilityType == AbilityType.Movement && player.IsOnMyNetwork())
        {
            StartCoroutine(UpdateVisibility(player));
        }
    }

    private IEnumerator UpdateVisibility(PlayerScript player)
    {
        yield return new WaitForSeconds(0.1f); // wacht tijd wijzingen zijn verwerkt
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
            tile.SetFogOnHex(!isVisibleTile);
        }
    }

    private void UpdatePlayersVisibleInMyFog(PlayerScript currentPlayer)
    {
        foreach (var player in GameHandler.instance.AllPlayers)
        {
            var playerIsInFog = player.CurrentHexTile.GetComponent<FogOnHex>().isFogActive;
            player.GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(!playerIsInFog);
        }
    }
}