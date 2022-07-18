using System.Collections.Generic;
using UnityEngine;

public class HighlightHexVision : AbstractHighlightMonoBehaviour
{
    public Material HighlightMaterial;
    public override Material GetHighlightMaterial() => HighlightMaterial;
}