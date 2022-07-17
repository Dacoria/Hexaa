using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public Vector3Int offSetCooridnates;

    private void Awake()
    {
        offSetCooridnates = transform.position.ConvertPositionToOffset();
    }    
}