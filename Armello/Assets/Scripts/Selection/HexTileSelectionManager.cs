using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HexTileSelectionManager : MonoBehaviour
{
    public HexGrid HexGrid;
    private List<Vector3Int> validNeighboursHightlighted = new List<Vector3Int>();

    public PlayerScript SelectedPlayer;
    public static HexTileSelectionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void HighlightMovementOptionsAroundPlayer(PlayerScript player)
    {
        SelectedPlayer = player;
        HightlightValidNeighbourTiles(player.CurrentHexTile);
    }

    public void HandleMouseClickForMove(Vector3 mousePosition)
    {
        List<Hex> selectedHexes;
        if (MonoHelper.instance.FindTile(mousePosition, out selectedHexes))
        {
            if (SelectedPlayer != null && Netw.CurrPlayer() == SelectedPlayer)
            {
                TryPlayerMoveAction(selectedHexes);
                return;
            }
        }
       
        DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
    }   

    private void TryPlayerMoveAction(List<Hex> selectedHexTiles)
    {
        var validNeighboursClicked = selectedHexTiles.Where(x => validNeighboursHightlighted.Any(y => y == x.HexCoordinates)).ToList();

        if (validNeighboursClicked.Count == 1)
        {
            NetworkActionEvents.instance.PlayerAbility(SelectedPlayer, validNeighboursClicked[0], AbilityType.Movement);
        }
        else if(validNeighboursClicked.Count > 1)
        {
            //Debug.Log("MULTIPLE RESULTS; DO NOTHING");
        }
        else
        {
            //Debug.Log("CANNOT DO MOVE");
        }

        DeselectHighlightedNeighbours(); // niks meer highlighten bij een klik
    }

    public void DeselectHighlightedNeighbours()
    {
        foreach (var neightbour in validNeighboursHightlighted)
        {
            HexGrid.GetTileAt(neightbour).DisableHighlight();
        }
        SelectedPlayer = null;
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
                HexGrid.GetTileAt(neightbour).EnableHighlight(HighlightColorType.Purple);
            }
        }
    }    
}
