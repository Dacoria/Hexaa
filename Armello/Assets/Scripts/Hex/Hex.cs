using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [ComponentInject] private HexCoordinates hexCoordinates;
    [ComponentInject] private HighlightHexScript highlightMove;

    [ComponentInject] private FogOnHex fogHighlight;

    public AbilityType AbilityHighlight;

    public Vector3Int HexCoordinates => hexCoordinates.offSetCooridnates;

    public HexType HexType;

    void Awake()
    {
        this.ComponentInject();
    }

    //public void EnableHighlight(AbilityType type) => highlightMove.SetHighlight(true, MonoHelper.instance.ColorAbilityDict.Single(x => x.Value == type).Key);
    public void EnableHighlight(HighlightColorType type) => highlightMove.SetHighlight(true, type);
    public void DisableHighlight() => highlightMove.SetHighlight(false, null);
    public void DisableHighlight(HighlightColorType type)
    {
        if(highlightMove.CurrentColorHighlight.HasValue && highlightMove.CurrentColorHighlight.Value == type)
        {
            highlightMove.SetHighlight(false, null);
        }
    }

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