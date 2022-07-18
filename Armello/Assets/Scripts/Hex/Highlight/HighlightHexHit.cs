using System.Collections.Generic;
using UnityEngine;

public class HighlightHexHit : AbstractHighlightMonoBehaviour
{
    public Material HighlightMaterial;
    public override Material GetHighlightMaterial() => HighlightMaterial;
}