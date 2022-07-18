using System.Collections.Generic;
using UnityEngine;

public class HighlightHexMove : AbstractHighlightMonoBehaviour
{
    public Material HighlightMaterial;
    public override Material GetHighlightMaterial() => HighlightMaterial;
}