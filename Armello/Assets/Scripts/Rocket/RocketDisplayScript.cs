using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class RocketDisplayScript : MonoBehaviour
{
    public bool IsLookingForRocketTarget;
    private bool isFiringRocket;
    private void Awake()
    {
        IsLookingForRocketTarget = false;
    }

    public void RocketButtonClicked()
    {
        IsLookingForRocketTarget = !IsLookingForRocketTarget;

        Textt.GameSync(IsLookingForRocketTarget ? "Select tile" : "");
    }

    private void Update()
    {       
        if (IsLookingForRocketTarget && !isFiringRocket)
        {
            DetectMouseClick();
        }
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            TryHandleRocketTileSelection(mousePos);
        }
    }

    private Hex HighlightedHex;

    private void TryHandleRocketTileSelection(Vector3 mousePos)
    {
        List<Hex> selectedTiles;
        if (MonoHelper.instance.FindTile(mousePos, out selectedTiles))
        {
            if(HighlightedHex != null && HighlightedHex == selectedTiles.First())
            {
                FireRocketOnHighlightedTile();
            }
            else
            {
                HighlightTile(selectedTiles);
            }
        }
    }

    private void FireRocketOnHighlightedTile()
    {
        isFiringRocket = true;
        Netw.CurrPlayer().GetComponent<PlayerRocketHandler>().FireRocket(HighlightedHex);
        Textt.GameSync("Firing!");
        StartCoroutine(DisableHighlightInXSeconds(1.15f));        
    }

    private IEnumerator DisableHighlightInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HighlightedHex.DisableHighlightMove();
        IsLookingForRocketTarget = false;
        isFiringRocket = false;
    }

    private void HighlightTile(List<Hex> result)
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlightMove();
        }

        HighlightedHex = result.First();
        HighlightedHex.EnableHighlightMove();
    }
}
