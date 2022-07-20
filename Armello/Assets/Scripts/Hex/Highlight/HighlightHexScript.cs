using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HighlightHexScript : MonoBehaviour
{
    private List<HighlightRendererStorage> highlightMap = new List<HighlightRendererStorage>();
    Dictionary<Renderer, Material[]> originalMaterialDict = new Dictionary<Renderer, Material[]>();

    public bool isHighlighted() => CurrentColorHighlight.HasValue;
    public HighlightColorType? CurrentColorHighlight;

    private void Start()
    {
        PrepareMaterialDicts();
    }

    private void PrepareMaterialDicts()
    {
        foreach (var colorHighlightMaterial in MonoHelper.instance.ColorHighlightMaterials)
        {
            var highlightMaterialDict = new Dictionary<Renderer, Material[]>();
            var cachedHighlightMaterialDict = new Dictionary<Color, Material>();

            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                if (renderer.name.StartsWith("Particle"))
                {
                    continue;
                }

                var originalMaterials = renderer.materials;
                if (!originalMaterialDict.ContainsKey(renderer))
                {
                    originalMaterialDict.Add(renderer, originalMaterials);
                }

                var newMaterials = new Material[renderer.materials.Length];

                for (int i = 0; i < originalMaterials.Length; i++)
                {
                    Material mat = null;
                    if (!cachedHighlightMaterialDict.TryGetValue(originalMaterials[i].color, out mat))
                    {
                        mat = new Material(colorHighlightMaterial.Material);
                        mat.color = originalMaterials[i].color;
                        cachedHighlightMaterialDict[mat.color] = mat;
                    }
                    newMaterials[i] = mat;
                }

                highlightMaterialDict.Add(renderer, newMaterials);
            }

            highlightMap.Add(new HighlightRendererStorage(colorHighlightMaterial.ColorType, highlightMaterialDict));
        }
    }

    private void UpdateHighlight(HighlightColorType? color)
    {
        if (color.HasValue)
        {
            foreach (var renderer in originalMaterialDict.Keys)
            {
                renderer.materials = highlightMap.Single(x => x.ColorType == color.Value).MaterialDict[renderer];
            }
        }
        else
        {
            foreach (var renderer in originalMaterialDict.Keys)
            {
                renderer.materials = originalMaterialDict[renderer];
            }
        }
    }

    public void SetHighlight(bool isHighlighted, HighlightColorType? colorType)
    {
        this.CurrentColorHighlight = colorType;
    }


    private HighlightColorType? previousHighlightedColor;
    private void Update()
    {
        if (previousHighlightedColor != this.CurrentColorHighlight)
        {
            UpdateHighlight(this.CurrentColorHighlight);
        }

        previousHighlightedColor = this.CurrentColorHighlight;
    }
}