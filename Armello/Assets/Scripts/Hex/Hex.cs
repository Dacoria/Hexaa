using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [ComponentInject] private HexCoordinates hexCoordinates;
    [ComponentInject] private HighlightHexMove highlightMove;
    [ComponentInject] private HighlightHexHit highlighHit;
    [ComponentInject] private HighlightHexRadar highlighRadar;
    [ComponentInject] private HighlightHexVision highlighVision;

    [ComponentInject] private FogOnHex fogHighlight;
    public Vector3Int HexCoordinates => hexCoordinates.offSetCooridnates;

    public HexType HexType;

    void Awake()
    {
        this.ComponentInject();
    }

    public void EnableHighlightMove() => highlightMove.SetHighlight(true);
    public void DisableHighlightMove() => highlightMove.SetHighlight(false);
    public void EnableHighlightHit() => highlighHit.SetHighlight(true);
    public void DisableHighlightHit() => highlighHit.SetHighlight(false);
    public void EnableHighlightRadar() => highlighRadar.SetHighlight(true);
    public void DisableHighlightRadar() => highlighRadar.SetHighlight(false);
    public void EnableHighlightVision() => highlighVision.SetHighlight(true);
    public void DisableHighlightVision() => highlighVision.SetHighlight(false);

    public void SetFogHighlight(bool fogEnabled) => fogHighlight.SetFog(fogEnabled);

    public int GetCost() => HexType switch
    {
        HexType.Difficult => 20,
        HexType.Default => 10,
        HexType.None => 10,
        HexType.Road => 5,
        HexType.Water => 1000,
        
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