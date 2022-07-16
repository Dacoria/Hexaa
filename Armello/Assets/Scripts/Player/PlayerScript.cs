using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Hex CurrentHexTile;
    private HexGrid HexGrid;

    private void Start()
    {
        StartCoroutine(InitSetCurrentHexTile());        
    }

    private IEnumerator InitSetCurrentHexTile()
    {
        yield return new WaitForSeconds(0.5f);
        HexGrid = FindObjectOfType<HexGrid>();

        var tilePos = transform.position.ConvertPositionToOffset();
        CurrentHexTile = HexGrid.GetTileAt(tilePos);

        transform.position = CurrentHexTile.transform.position;
    }
}
