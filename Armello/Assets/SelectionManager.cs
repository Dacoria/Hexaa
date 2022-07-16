using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public LayerMask selectionMask;
    public HexGrid HexGrid;

    private List<Vector3Int> neighboursHightlighted = new List<Vector3Int>();

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if(FindTarget(mousePosition, out result))
        {
            var selectedHex = result.GetComponent<Hex>();
            selectedHex.DisableHighlight();

            foreach(var neightbour in neighboursHightlighted)
            {
                HexGrid.GetTileAt(neightbour).DisableHighlight();
            }

            neighboursHightlighted = HexGrid.GetNeighboursFor(selectedHex.HexCoordinates);

            foreach (var neightbour in neighboursHightlighted)
            {
                HexGrid.GetTileAt(neightbour).EnableHighlight();
            }
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray, out hit, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}
