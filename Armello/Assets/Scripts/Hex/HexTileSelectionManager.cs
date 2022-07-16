using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HexTileSelectionManager : MonoBehaviour
{
    public HexGrid HexGrid;
    private List<Vector3Int> validNeighboursHightlighted = new List<Vector3Int>();

    private PlayerScript SelectedPlayer;
    
    public void HandleMouseClick(Vector3 mousePosition)
    {
        // highlighted betekent: Valid moves voor player om heen te gaan

        List<Hex> selectedHexes;
        if (MonoHelper.instance.FindTile(mousePosition, out selectedHexes))
        {
            HandleClickOnTile(selectedHexes);
        }
        else
        {
            DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
            SelectedPlayer = null;
        }
    }

    private void HandleClickOnTile(List<Hex> selectedHexTiles)
    {
        var currentPlayer = NetworkHelper.instance.GetMyPlayer();
        if(SelectedPlayer != null && currentPlayer == SelectedPlayer)
        {
            TryPlayerMoveAction(selectedHexTiles);
        }
        else if(selectedHexTiles.Any(x => x == currentPlayer.CurrentHexTile))
        {
            ShowMovesForPlayer(currentPlayer);
        }
        else
        {
            DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
            SelectedPlayer = null;
        }
    }

    private void ShowMovesForPlayer(PlayerScript player)
    {
        SelectedPlayer = player;
        HightlightValidNeighbourTiles(player.CurrentHexTile);
    }

    private void TryPlayerMoveAction(List<Hex> selectedHexTiles)
    {
        var validNeighboursClicked = selectedHexTiles.Where(x => validNeighboursHightlighted.Any(y => y == x.HexCoordinates)).ToList();

        if (validNeighboursClicked.Count == 1)
        {
            SelectedPlayer.GetComponent<PlayerMovement>().DoMove(validNeighboursClicked[0]);
        }
        else if(validNeighboursClicked.Count > 1)
        {
            Debug.Log("MULTIPLE RESULTS; DO NOTHING");
        }
        else
        {
            Debug.Log("CANNOT DO MOVE");
        }

        DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
        SelectedPlayer = null;
    }

    private void DeselectHighlightedNeighbours()
    {
        foreach (var neightbour in validNeighboursHightlighted)
        {
            HexGrid.GetTileAt(neightbour).DisableHighlight();
        }
    }

    private void HightlightValidNeighbourTiles(Hex selectedHex)
    {
        var neighboursToTryToHightlight = HexGrid.GetNeighboursFor(selectedHex.HexCoordinates);
        validNeighboursHightlighted = new List<Vector3Int>();

        foreach (var neightbour in neighboursToTryToHightlight)
        {
            var tile = HexGrid.GetTileAt(neightbour);
            if(!tile.HexType.In(HexType.Water, HexType.Obstacle))
            {
                validNeighboursHightlighted.Add(neightbour);
                HexGrid.GetTileAt(neightbour).EnableHighlight();
            }
        }
    }    
}
