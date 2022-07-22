using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementDisplayScript : MonoBehaviour, IAbilityAction
{
    [ComponentInject] private Button button;
    private bool movementAbilIsActive;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void DeselectAbility()
    {
        HexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        movementAbilIsActive = false;
    }

    public void InitAbilityAction()
    {
        HexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(GameHandler.instance.CurrentPlayer());
        Textt.GameLocal("Select the tile to move to");
        movementAbilIsActive = true;
    }

    private void Update()
    {
        if(!movementAbilIsActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (HexTileSelectionManager.instance.SelectedPlayer == null)
            {
                // movement aanzetten gaat eerst via knoppen
                return;
            }

            HexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition);
        }
    }
}