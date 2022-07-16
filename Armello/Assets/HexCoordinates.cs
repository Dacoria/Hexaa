using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public static float xOffset = 2, yOffset = 1, zOffset = 1.73f;

    public Vector3Int offSetCooridnates;

    private void Awake()
    {
        offSetCooridnates = ConvertPositionToOffset(transform.position);
    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        var x = Mathf.CeilToInt(position.x / xOffset);
        var y = Mathf.RoundToInt(position.y / yOffset);
        var z = Mathf.RoundToInt(position.z / zOffset);

        return new Vector3Int(x, y, z);
    }
}
