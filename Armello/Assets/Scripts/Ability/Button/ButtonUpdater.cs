using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpdater : MonoBehaviour
{
    public Sprite ButtonPressedSprite;

    [ComponentInject] private EndTurnButtonScript EndTurnButtonScript;
    private List<ButtonAbilityDisplay> abilityScripts;    

    private void Awake()
    {
        this.ComponentInject();
        abilityScripts = GetComponentsInChildren<ButtonAbilityDisplay>().ToList();
    }
    
    public void SetAbilityInteractable(AbilityType type, bool value)
    {
        abilityScripts.Single(x => x.type == type).Button.interactable = value;
    }

    public void SetToUnselected(AbilityType type)
    {
        var abilityScript = abilityScripts.Single(x => x.type == type);
        abilityScript.AbilityIsActive = false;
        abilityScript.GetComponent<IAbilityAction>().DeselectAbility();
    }

    public void OnAbilityButtonClick(ButtonAbilityDisplay caller)
    {
        abilityScripts.Where(x => x != caller).ToList().ForEach(x => SetToUnselected(x.type));
        if (!caller.AbilityIsActive)
        {
            caller.AbilityIsActive = true;
            caller.GetComponent<IAbilityAction>().InitAbilityAction();
        }
    }
}