using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementDisplayScript : MonoBehaviour, IDeselectHandler
{
    [ComponentInject] private Button button;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        HexTileSelectionManager.instance.DeselectHighlightedNeighbours();
    }

    public void OnMovementButtonClick()
    {
        HexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(GameHandler.instance.CurrentPlayer);
        Textt.GameLocal("Select the tile to move to");
    }
}