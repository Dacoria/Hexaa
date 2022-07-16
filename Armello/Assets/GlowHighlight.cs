using System.Collections.Generic;
using UnityEngine;

public class GlowHighlight : MonoBehaviour
{
    Dictionary<Renderer, Material[]> glowMaterialDict = new Dictionary<Renderer, Material[]>();
    Dictionary<Renderer, Material[]> originalMaterialDict = new Dictionary<Renderer, Material[]>();

    Dictionary<Color, Material> cachedMaterialDict = new Dictionary<Color, Material>();

    public Material glowMaterial;

    public bool isGlowing = false;

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
                    mat = new Material(glowMaterial);

                    mat.color = originalMaterials[i].color;
                    cachedMaterialDict[mat.color] = mat;
                }
                newMaterials[i] = mat;
            }

            glowMaterialDict.Add(renderer, newMaterials);
        }
    }

    private void UpdateGlow()
    {
        if (isGlowing)
        {
            foreach (var renderer in originalMaterialDict.Keys)
            {
                renderer.materials = glowMaterialDict[renderer];
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

    public void SetGlow(bool isGlowing)
    {
        this.isGlowing = isGlowing;
    }


    private bool oldGlow;
    private void Update()
    {
        if (oldGlow != this.isGlowing)
        {
            UpdateGlow();
        }

        oldGlow = this.isGlowing;
    }
}