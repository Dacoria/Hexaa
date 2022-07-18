using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VisionDisplayScript : MonoBehaviour
{
    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }    

    private bool isLookingForVisionTarget;

    public void OnVisionButtonClick()
    {
        isLookingForVisionTarget = true;
    }

    private void Update()
    {
        if (isLookingForVisionTarget)
        {
            DetectMouseClick();
        }
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            TryHandleVisionTileSelection(mousePos);
        }
    }

    private Hex HighlightedHex;

    private void TryHandleVisionTileSelection(Vector3 mousePos)
    {
        List<Hex> selectedTiles;
        if (MonoHelper.instance.FindTile(mousePos, out selectedTiles))
        {
            if (HighlightedHex != null && HighlightedHex == selectedTiles.First())
            {
                ShowVisionOfTile();
            }
            else
            {
                HighlightTile(selectedTiles);
            }
        }
    }

    private void HighlightTile(List<Hex> selectedTiles)
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlightMove();
        }
        HighlightedHex = selectedTiles.First();
        HighlightedHex.EnableHighlightMove();
    }

    private void ShowVisionOfTile()
    {
        HighlightedHex.SetFogHighlight(false); // local!
        NetworkActionEvents.instance.PlayerAbility(GameHandler.instance.CurrentPlayer, HighlightedHex, AbilityType.Vision);
    }

    private void OnPlayerAbility(PlayerScript playerDoingAbility, Hex target, AbilityType type)
    {
        isLookingForVisionTarget = false;
        if (type == AbilityType.Vision)
        {
            target.EnableHighlightVision();

            foreach (var player in GameHandler.instance.AllPlayers)
            {
                if(target.HexCoordinates == player.CurrentHexTile.HexCoordinates)
                {
                    player.GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(true);
                }              
            }            
        }
    }
}