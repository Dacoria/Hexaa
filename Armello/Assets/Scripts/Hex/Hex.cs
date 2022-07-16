using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [ComponentInject] private HexCoordinates hexCoordinates;
    [ComponentInject] private GlowHighlight glowHighlight;
    public Vector3Int HexCoordinates => hexCoordinates.offSetCooridnates;

    public HexType HexType;

    void Awake()
    {
        this.ComponentInject();
    }

    public void EnableHighlight()
    {
        glowHighlight.ToggleGlow(true);
    }

    public void DisableHighlight()
    {
        glowHighlight.ToggleGlow(false);
    }

    public int GetCost() => HexType switch
    {
        HexType.Difficult => 20,
        HexType.Default => 10,
        HexType.Road => 5,
        _ => throw new System.Exception("Hextype " + HexType + " is not supported")
    };

    public bool IsObstacle() => HexType == HexType.Obstacle;
}


public enum HexType
{
    None,
    Default,
    Difficult,
    Road,
    Water,
    Obstacle
}