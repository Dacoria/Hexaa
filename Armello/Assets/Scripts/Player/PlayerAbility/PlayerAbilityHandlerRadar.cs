using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAbilityHandler : MonoBehaviour
{
    private void OnRadarAbility(PlayerScript player, Hex target)
    {        
        ClearAllRadars();

        var grids = HexGrid.instance.GetNeighboursFor(target.HexCoordinates);
        target.EnableHighlight(HighlightColorType.Blue);

        foreach (var grid in grids)
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