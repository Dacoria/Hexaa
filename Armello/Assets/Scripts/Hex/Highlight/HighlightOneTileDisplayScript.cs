using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class HighlightOneTileDisplayScript : MonoBehaviour
{
    public Action<Hex> CallbackOnTileSelection;
    public Action<Hex> CallbackOnTileSelectionConfirmed;

    public bool DestroyOnCallbackSelection = false;
    public bool DestroyOnCallbackSelectionConfirmed = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            TryHandleTileSelection(mousePos);
        }
    }   

    private Hex HighlightedHex;

    private void TryHandleTileSelection(Vector3 mousePos)
    {
        List<Hex> selectedTiles;
        if (MonoHelper.instance.FindTile(mousePos, out selectedTiles))
        {
            if(HighlightedHex != null && selectedTiles.Any(x => x == HighlightedHex))
            {
                if(selectedTiles.Count == 1)
                {
                    TileSelectionConfirmed();
                }                
            }
            else
            {
                TileSelection(selectedTiles);                
            }
        }
    }

    private void TileSelectionConfirmed()
    {
        HighlightedHex.DisableHighlight();
        CallbackOnTileSelectionConfirmed?.Invoke(HighlightedHex);
        if (DestroyOnCallbackSelectionConfirmed) { Destroy(this); }
    }

    private void TileSelection(List<Hex> result)
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlight();
        }

        HighlightedHex = result.First();
        HighlightedHex.EnableHighlight(HighlightColorType.White);

        CallbackOnTileSelection?.Invoke(HighlightedHex);
        if(DestroyOnCallbackSelection) { Destroy(this); }
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy - HighlightOneTileDisplayScript");

        if(HighlightedHex != null)
        {
            HighlightedHex.DisableHighlight(HighlightColorType.White);
        }
    }
}
