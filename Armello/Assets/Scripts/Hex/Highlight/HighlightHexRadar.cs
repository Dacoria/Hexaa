using System.Collections.Generic;
using UnityEngine;

public class HighlightHexRadar : AbstractHighlightMonoBehaviour
{
    public Material HighlightMaterial;
    public override Material GetHighlightMaterial() => HighlightMaterial;
}