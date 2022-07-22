using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisionDisplayScript : MonoBehaviour, IAbilityAction
{
    private HighlightOneTileDisplayScript highlightOneTileDisplayScript;

    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        Textt.GameLocal("Select a tile for a Vision target");
        var highlightOneTileSelection = gameObject.AddComponent<HighlightOneTileDisplayScript>();
        highlightOneTileSelection.CallbackOnTileSelection = OnTileSelection;
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;
    }

    private void OnTileSelection(Hex hex)
    {
        Textt.GameLocal("Reselect tile to get vision on that tile");
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {
        NetworkActionEvents.instance.PlayerAbility(GameHandler.instance.CurrentPlayer(), hex, AbilityType.Vision);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }
}