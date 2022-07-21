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
        ActionEvents.EndRound += OnEndRound;
        ActionEvents.EndGame += OnEndGame;
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(visible: false, interactable: false);
    }

    private void UpdateEndTurnButton(bool? visible = null, bool ? interactable = null)
    {
        if (visible.HasValue)
        {
            endTurnButtonScript.GetComponent<CanvasGroup>().alpha = visible.Value ? 1 : 0;
        }
        if (interactable.HasValue)
        {
            endTurnButtonScript.Button.interactable = interactable.Value;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateAllAbilities(setToUnselected: true);
        }
    }

    private void OnEndRound(bool reachedMiddle)
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(interactable: false);
    }

    private void OnEndGame()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(interactable: false);
    }

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if(GameHandler.instance.GameStatus != GameStatus.ActiveRound) { return; }
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
        if (GameHandler.instance.GameStatus != GameStatus.ActiveRound) { return; }
        UpdateAllAbilities(interactable: currentPlayer.IsOnMyNetwork(), setToUnselected: true);
        UpdateEndTurnButton(visible: true, interactable: currentPlayer.IsOnMyNetwork());
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.EndRound -= OnEndRound;
        ActionEvents.EndGame -= OnEndGame;

    }
}