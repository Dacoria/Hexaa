using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class RocketDisplayScript : MonoBehaviour
{
    public bool IsLookingForRocketTarget;

    private void Awake()
    {
        IsLookingForRocketTarget = false;
    }

    public void RocketButtonClicked()
    {
        IsLookingForRocketTarget = !IsLookingForRocketTarget;
        Textt.GameSync("Select tile");
    }

    private void Update()
    {
        if (IsLookingForRocketTarget)
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
        NetworkHelper.instance.GetMyPlayer().GetComponent<PlayerRocketHandler>().FireRocket(HighlightedHex);
        Textt.GameSync("");
        StartCoroutine(DisableHighlightInXSeconds(1f));        
    }

    private IEnumerator DisableHighlightInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HighlightedHex.DisableHighlight();
        IsLookingForRocketTarget = false;
    }

    private void HighlightTile(List<Hex> result)
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlight();
        }
        HighlightedHex = result.First();
        HighlightedHex.EnableHighlight();
    }
}
