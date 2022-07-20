using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    [ComponentInject] private ButtonUpdater buttonUpdater;
    [ComponentInject] private EndTurnButtonScript endTurnButtonScript;

    private void Awake()
    {
        this.ComponentInject();        
    }

    private void Start()
    {
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.PlayerAbility += OnPlayerAbility;
        ActionEvents.RoundEnded += OnRoundEnded;
        UpdateAllAbilities(setToUnselected: true, interactable: false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateAllAbilities(setToUnselected: true);
        }
    }

    private void OnRoundEnded(bool reachedMiddle)
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
    }    

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if(GameHandler.instance.GameEnded) { return; }
        StartCoroutine(UpdatePlayerAbilityButtons(player, hex, type));
    }

    private IEnumerator UpdatePlayerAbilityButtons(PlayerScript player, Hex hex, AbilityType type)
    {
        yield return new WaitForSeconds(0.1f); // wijziging moet verwerkt worden....
        UpdateAllAbilities(setToUnselected: true);
    }

    private void UpdateAllAbilities(bool setToUnselected, bool? interactable = null)
    {
        foreach (AbilityType abilityType in Enum.GetValues(typeof(AbilityType)))
        {
            if(abilityType == AbilityType.None) continue;

            if (setToUnselected)
            {
                buttonUpdater.SetToUnselected(abilityType);
            }
            if (interactable.HasValue)
            {
                buttonUpdater.SetAbilityInteractable(abilityType, interactable.Value);
            }
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript currentPlayer)
    {
        CheckEnableButtonsNewTurn(currentPlayer);
        DisableRocketOnFirstRound(currentPlayer);
    }

    private void DisableRocketOnFirstRound(PlayerScript currentPlayer)
    {
        if (currentPlayer.GetComponent<PlayerTurnCount>().TurnCount <= 1)
        {
            buttonUpdater.SetAbilityInteractable(AbilityType.Rocket, false);
        }
    }

    private void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        CheckEnableButtonsNewTurn(currentPlayer);
        DisableRocketOnFirstRound(currentPlayer);
    }

    private void CheckEnableButtonsNewTurn(PlayerScript currentPlayer)
    {
        if (GameHandler.instance.GameEnded) { return; }
        UpdateAllAbilities(interactable: currentPlayer.IsOnMyNetwork(), setToUnselected: true);
        endTurnButtonScript.Button.interactable = currentPlayer.IsOnMyNetwork();
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.RoundEnded -= OnRoundEnded;
    }
}