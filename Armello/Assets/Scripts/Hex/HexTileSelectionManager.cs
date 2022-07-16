using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HexTileSelectionManager : MonoBehaviour
{
    public HexGrid HexGrid;
    private List<Vector3Int> validNeighboursHightlighted = new List<Vector3Int>();

    private PlayerScript SelectedPlayer;

    public void HandleClick(Vector3 mousePosition)
    {
        // highlighted betekent: Valid moves voor player om heen te gaan

        List<Hex> selectedHexes;
        if (FindTile(mousePosition, out selectedHexes))
        {
            HandleClickedOnTile(selectedHexes);
        }
        else
        {
            DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
            SelectedPlayer = null;
        }
    }

    private void HandleClickedOnTile(List<Hex> selectedHexTiles)
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

    private bool FindTile(Vector3 mousePosition, out List<Hex> result)
    {
        var layermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var hits = Physics.RaycastAll(ray, layermask);
        if (hits.Length > 0)
        {
            result = hits
                .Where(x => x.collider.gameObject.GetComponent<Hex>() != null)
                .Select(x => x.collider.gameObject.GetComponent<Hex>())
                .ToList();

            return true;                      
        }

        result = null;
        return false;
    }
}
