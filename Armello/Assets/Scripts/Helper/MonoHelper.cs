using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonoHelper : MonoBehaviour
{
    public static MonoHelper instance;

    private void Awake()
    {
        instance = this;
    }

    public bool FindTile(Vector3 mousePosition, out List<Hex> result)
    {
        var layermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var hits = Physics.RaycastAll(ray, layermask);
        if (hits.Length > 0)
        {
            result = hits
                .Where(x => x.collider.gameObject.GetComponent<Hex>() != null)
                .Select(x => x.collider.gameObject.GetComponent<Hex>())
                .ToList();

            return true;
        }

        result = null;
        return false;
    }
}