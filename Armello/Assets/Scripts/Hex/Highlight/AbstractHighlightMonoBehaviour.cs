using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHighlightMonoBehaviour : MonoBehaviour
{
    Dictionary<Renderer, Material[]> highlightMaterialDict = new Dictionary<Renderer, Material[]>();
    Dictionary<Renderer, Material[]> originalMaterialDict = new Dictionary<Renderer, Material[]>();

    Dictionary<Color, Material> cachedMaterialDict = new Dictionary<Color, Material>();
    public abstract Material GetHighlightMaterial();


    public bool isHighlighted = false;

    private void Awake()
    {
        PrepareMaterialDicts();
    }

    private void PrepareMaterialDicts()
    {
        foreach(var renderer in GetComponentsInChildren<Renderer>())
        {
            if(renderer.name.StartsWith("Particle"))
            {
                continue;
            }

            var originalMaterials = renderer.materials;
            originalMaterialDict.Add(renderer, originalMaterials);

            var newMaterials = new Material[renderer.materials.Length];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                if(!cachedMaterialDict.TryGetValue(originalMaterials[i].color, out mat))
                {
                    mat = new Material(GetHighlightMaterial());

                    mat.color = originalMaterials[i].color;
                    cachedMaterialDict[mat.color] = mat;
                }
                newMaterials[i] = mat;
            }

            highlightMaterialDict.Add(renderer, newMaterials);
        }
    }

    private void UpdateHighlight()
    {
        if (isHighlighted)
        {
            foreach (var renderer in originalMaterialDict.Keys)
            {
                renderer.materials = highlightMaterialDict[renderer];
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

    public void SetHighlight(bool isHighlighted)
    {
        this.isHighlighted = isHighlighted;
    }


    private bool previousIsHighlighted;
    private void Update()
    {
        if (previousIsHighlighted != this.isHighlighted)
        {
            UpdateHighlight();
        }

        previousIsHighlighted = this.isHighlighted;
    }
}