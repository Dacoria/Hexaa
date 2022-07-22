using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAbilityHandler : MonoBehaviour
{
    private void OnRadarAbility(PlayerScript player, Hex randomHex)
    {
        ClearAllRadars();

        var otherPlayer = NetworkHelper.instance.OtherPlayerClosest(Netw.CurrPlayer());
        if (otherPlayer == null) { return; }
        var gridsAroundOtherPlayer = HexGrid.instance.GetNeighboursFor(otherPlayer.CurrentHexTile.HexCoordinates);
        gridsAroundOtherPlayer.Shuffle();
        var target = gridsAroundOtherPlayer[0].GetHex();


        var gridsAroundRadarSpot = HexGrid.instance.GetNeighboursFor(target.HexCoordinates);
        target.EnableHighlight(HighlightColorType.Blue);

        foreach (var grid in gridsAroundRadarSpot)
        {
            grid.GetHex().EnableHighlight(HighlightColorType.Blue);
        }        
    }

    private void ClearAllRadars()
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlight(HighlightColorType.Blue);
        }
    }
}