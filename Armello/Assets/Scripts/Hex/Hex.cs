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

    public HighlightColorType? GetHighlight() => highlightMove.CurrentColorHighlight;

    public Vector3Int HexCoordinates => hexCoordinates.offSetCooridnates;

    public HexType HexType;

    public Vector3 OrigPosition;

    void Awake()
    {
        this.ComponentInject();
        OrigPosition = this.transform.position;
    }

    public void EnableHighlight(HighlightColorType type) => highlightMove.SetHighlight(true, type);
    public void DisableHighlight() => highlightMove.SetHighlight(false, null);
    public void DisableHighlight(HighlightColorType type)
    {
        if(highlightMove.CurrentColorHighlight.HasValue && highlightMove.CurrentColorHighlight.Value == type)
        {
            highlightMove.SetHighlight(false, null);
        }
    }

    public void SetFogOnHex(bool fogEnabled)
    {
        fogHighlight.SetFog(fogEnabled);
    
        foreach (var player in GameHandler.instance.AllPlayers)
        {
            if (HexCoordinates == player.CurrentHexTile.HexCoordinates)
            {
                player.GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(!fogEnabled);
            }
        }
        
    }

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