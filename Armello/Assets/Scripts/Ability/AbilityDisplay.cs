using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AbilityDisplay : MonoBehaviour
{

    public AbilityType type;
    [ComponentInject] private TMP_Text CostText;
    [ComponentInject] public Button Button;
    [ComponentInject] public Image ImageButton;    
    [ComponentInject(SearchDirection = SearchDirection.PARENT)] private ButtonUpdater buttonUpdater;

    public Image ImageAbility;

    private void Awake()
    {
        this.ComponentInject();
        ImageAbility = this.GetComponentOnlyInDirectChildren<Image>();
        this.origPosAbilImage = ImageAbility.transform.position;
    }

    private Vector3 origPosAbilImage;

    private void Start()
    {
        CostText.text = type.Cost().ToString();        
    }



    private Color colorDisabled = new Color(140 / 255f, 140 / 255f, 140 / 255f, 120 / 255f);
    private Color colorSelectable = new Color(220 / 255f, 180 / 255f, 70 / 255f, 120 / 255f);
    private Color colorSelected = new Color(255 / 255f, 190 / 255f, 0 / 255f, 255 / 255f);

    private bool abilityIsActive;

    private void Update()
    {
        if (ImageButton.overrideSprite.name.Contains("pressed"))
        {
            ImageAbility.transform.position = origPosAbilImage + new Vector3(0, -15, 0);
        }
        else
        {
            ImageAbility.transform.position = origPosAbilImage;
        }
    }

    public void OnButtonClick()
    {
        var newStatus = !abilityIsActive;
        buttonUpdater.OnAbilityButtonClick(); // disabled alle buttons
        abilityIsActive = newStatus;
    }

    public void UnselectAbility()
    {
        abilityIsActive = false;
    }
}
