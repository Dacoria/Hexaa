using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisionDisplayScript : MonoBehaviour, IAbilityAction
{
    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }

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
        hex.SetFogHighlight(false); // local!
        NetworkActionEvents.instance.PlayerAbility(GameHandler.instance.CurrentPlayer, hex, AbilityType.Vision);
    }

    private void OnPlayerAbility(PlayerScript playerDoingAbility, Hex target, AbilityType type)
    {
        Destroy(highlightOneTileDisplayScript);
        if (type == AbilityType.Vision)
        {
            target.EnableHighlight(HighlightColorType.Yellow);

            foreach (var player in GameHandler.instance.AllPlayers)
            {
                if(target.HexCoordinates == player.CurrentHexTile.HexCoordinates)
                {
                    player.GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(true);
                }              
            }            
        }
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }
}