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
        hex.SetFogHighlight(false); // local!
        Netw.CurrPlayer().GetComponent<PlayerRocketHandler>().FireRocket(hex);
        Textt.GameSync("Firing rocket!");
    }


    public void DeselectAbility()
    {
        if (highlightOneTileDisplayScript != null)
        {
            highlightOneTileDisplayScript.DisableHighlight();
        }
        Destroy(highlightOneTileDisplayScript);
    }
}
