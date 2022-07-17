using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class AbilityCostDisplay : MonoBehaviour
{

    public AbilityType type;
    [ComponentInject] private TMP_Text CostText;

    private void Awake()
    {
        this.ComponentInject();
    }

    private void Start()
    {
        CostText.text = type.Cost().ToString();
    }
}
