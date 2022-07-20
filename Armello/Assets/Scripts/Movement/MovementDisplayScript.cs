using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementDisplayScript : MonoBehaviour, IAbilityAction
{
    [ComponentInject] private Button button;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void DeselectAbility()
    {
        HexTileSelectionManager.instance.DeselectHighlightedNeighbours();
    }

    public void InitAbilityAction()
    {
        HexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(GameHandler.instance.CurrentPlayer);
        Textt.GameLocal("Select the tile to move to");
    }
}