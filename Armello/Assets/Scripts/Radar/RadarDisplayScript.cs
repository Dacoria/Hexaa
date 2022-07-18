using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RadarDisplayScript : MonoBehaviour
{
    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }    

    public void OnRadarButtonClick()
    {
        var otherPlayer = NetworkHelper.instance.OtherPlayerClosest(Netw.CurrPlayer());
        var grids = HexGrid.instance.GetNeighboursFor(otherPlayer.CurrentHexTile.HexCoordinates);
        grids.Shuffle();
        var gridSelected = grids[0].GetHex();

        NetworkActionEvents.instance.PlayerAbility(GameHandler.instance.CurrentPlayer, gridSelected, AbilityType.Radar);
    }

    private void OnPlayerAbility(PlayerScript player, Hex target, AbilityType type)
    {
        if(type == AbilityType.Radar)
        {
            ClearAllRadars();

            var grids = HexGrid.instance.GetNeighboursFor(target.HexCoordinates);
            target.EnableHighlightRadar();

            foreach (var grid in grids)
            {
                grid.GetHex().EnableHighlightRadar();
            }
        }
    }

    private void ClearAllRadars()
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlightRadar();
        }
    }
}