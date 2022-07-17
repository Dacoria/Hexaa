using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [ComponentInject] private HexCoordinates hexCoordinates;
    [ComponentInject] private GlowHighlight glowHighlight;
    [ComponentInject] private FogHighlight fogHighlight;
    public Vector3Int HexCoordinates => hexCoordinates.offSetCooridnates;

    public HexType HexType;

    void Awake()
    {
        this.ComponentInject();
    }

    public void EnableHighlight()
    {
        glowHighlight.SetGlow(true);
    }

    public void DisableHighlight()
    {
        glowHighlight.SetGlow(false);
    }

    public void SetFogHighlight(bool fogEnabled)
    {
        fogHighlight.SetFog(fogEnabled);
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