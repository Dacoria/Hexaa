using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RadarDisplayScript : MonoBehaviour, IAbilityAction
{
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
        NetworkActionEvents.instance.PlayerAbility(GameHandler.instance.CurrentPlayer(), hex, AbilityType.Radar);
    } 

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }
}