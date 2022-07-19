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


    private bool isLookingForRadarTarget;


    public void OnRadarButtonClick()
    {
        isLookingForRadarTarget = true;
        Textt.GameLocal("Select a tile for a radar target");
    }

    private void Update()
    {
        if (isLookingForRadarTarget)
        {
            DetectMouseClick();
        }
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            TryHandleRadarTileSelection(mousePos);
        }
    }

    private Hex HighlightedHex;

    private void TryHandleRadarTileSelection(Vector3 mousePos)
    {
        List<Hex> selectedTiles;
        if (MonoHelper.instance.FindTile(mousePos, out selectedTiles))
        {
            if (HighlightedHex != null && HighlightedHex == selectedTiles.First())
            {
                StartRadar();
            }
            else
            {
                HighlightTile(selectedTiles);
            }
        }
    }

    private void HighlightTile(List<Hex> selectedTiles)
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlightMove();
        }
        HighlightedHex = selectedTiles.First();
        HighlightedHex.EnableHighlightMove();

        Textt.GameLocal("Reselect tile to do radar");
    }

    private void StartRadar()
    {
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