using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RadarDisplayScript : MonoBehaviour, IAbilityAction
{
    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }

    private HighlightOneTileDisplayScript highlightOneTileDisplayScript;

    public void InitAbilityAction()
    {
        Textt.GameLocal("Select a tile to start radar");
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        var highlightOneTileSelection = gameObject.AddComponent<HighlightOneTileDisplayScript>();
        highlightOneTileSelection.CallbackOnTileSelection = OnTileSelection;
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;
    }

    private void OnTileSelection(Hex hex)
    {
        Textt.GameLocal("Reselect tile to confirm radar move");
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {
        // doet nog niks met selected tile

        var otherPlayer = NetworkHelper.instance.OtherPlayerClosest(Netw.CurrPlayer());
        if (otherPlayer == null) { return; }
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
            target.EnableHighlight(HighlightColorType.Blue);

            foreach (var grid in grids)
            {
                grid.GetHex().EnableHighlight(HighlightColorType.Blue);
            }
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

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }
}