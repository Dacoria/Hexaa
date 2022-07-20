using System.Collections.Generic;
using UnityEngine;

public class HighlightRendererStorage
{
    public HighlightColorType ColorType;
    public Dictionary<Renderer, Material[]> MaterialDict = new Dictionary<Renderer, Material[]>();

    public HighlightRendererStorage(HighlightColorType colorType, Dictionary<Renderer, Material[]> materialDict)
    {
        ColorType = colorType;
        MaterialDict = materialDict;
    }
}