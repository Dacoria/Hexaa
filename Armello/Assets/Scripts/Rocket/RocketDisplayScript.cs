using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RocketDisplayScript : MonoBehaviour, IAbilityAction
{
    private HighlightOneTileDisplayScript highlightOneTileDisplayScript;

    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        Textt.GameLocal("Select a tile to fire your rocket");
        var highlightOneTileSelection = gameObject.AddComponent<HighlightOneTileDisplayScript>();
        highlightOneTileSelection.CallbackOnTileSelection = OnTileSelection;
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;
    }

    private void OnTileSelection(Hex hex)
    {
        Textt.GameLocal("Reselect tile to fire the rocket!");
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {        
        NetworkActionEvents.instance.PlayerAbility(Netw.CurrPlayer(), hex, AbilityType.Rocket);
        Textt.GameSync("Firing rocket!");
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }
}
