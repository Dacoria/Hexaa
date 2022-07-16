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

        GameObject hexTileGo;
        if (FindTile(mousePosition, out hexTileGo))
        {
            var selectedHexTile = hexTileGo.GetComponent<Hex>();
            HandleClickedOnTile(selectedHexTile);
        }
        else
        {
            DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
            SelectedPlayer = null;
        }
    }

    private void HandleClickedOnTile(Hex selectedHexTile)
    {
        var currentPlayer = NetworkHelper.instance.GetMyPlayer();
        if(SelectedPlayer != null && currentPlayer == SelectedPlayer)
        {
            TryPlayerMoveAction(selectedHexTile);
        }
        else if(currentPlayer.CurrentHexTile == selectedHexTile)
        {
            ShowMovesForPlayer(selectedHexTile, currentPlayer);
        }
        else
        {
            DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
            SelectedPlayer = null;
        }
    }

    private void ShowMovesForPlayer(Hex selectedHexTile, PlayerScript player)
    {
        SelectedPlayer = player;
        HightlightValidNeighbourTiles(selectedHexTile);
    }

    private void TryPlayerMoveAction(Hex selectedHexTile)
    {
        if (validNeighboursHightlighted.Any(x => x == selectedHexTile.HexCoordinates))
        {
            SelectedPlayer.GetComponent<PlayerMovement>().DoMove(selectedHexTile);
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

    private bool FindTile(Vector3 mousePosition, out GameObject result)
    {
        var layermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var hits = Physics.RaycastAll(ray, layermask);
        if (hits.Length > 0)
        {
            if(hits.Length == 1)
            {
                result = hits[0].collider.gameObject;
                return true;
            }
            else
            {
                // soms ook player object erbij
                result = hits.First(x => x.collider.gameObject.GetComponent<Hex>() != null).collider.gameObject;
                return true;
            }            
        }

        result = null;
        return false;
    }
}
