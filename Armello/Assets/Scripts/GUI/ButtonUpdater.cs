using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonUpdater : MonoBehaviour
{
    [ComponentInject] private EndTurnButtonScript EndTurnButtonScript;
    private List<AbilityDisplay> abilityScripts;

    private void Awake()
    {
        this.ComponentInject();
        abilityScripts = GetComponentsInChildren<AbilityDisplay>().ToList();
    }

    private void Start()
    {
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.PlayerAbility += OnPlayerAbility;
        ActionEvents.RoundEnded += OnRoundEnded;
        DisableAllButtons();
    }

    private void OnRoundEnded(bool reachedMiddle)
    {
        DisableAllButtons();
    }

    private void DisableAllButtons()
    {
        foreach (var abilityScript in abilityScripts)
        {
            abilityScript.Button.interactable = false;
        }

        EndTurnButtonScript.Button.interactable = false;
    }

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if(GameHandler.instance.GameEnded) { return; }
        StartCoroutine(UpdatePlayerAbilityButtons(player, hex, type));
    }

    private IEnumerator UpdatePlayerAbilityButtons(PlayerScript player, Hex hex, AbilityType type)
    {
        yield return new WaitForSeconds(0.1f); // wijziging moet verwerkt worden....

        if (player.IsOnMyNetwork())
        {
            var playerAction = player.GetComponent<PlayerAbility>();
            var pointsLeft = playerAction.CurrentPlayerActionPoints;

            foreach (var abilityScript in abilityScripts)
            {
                abilityScript.Button.interactable = abilityScript.type.Cost() <= pointsLeft;
                abilityScript.UnselectAbility();
            }
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript currentPlayer)
    {
        CheckEnableButtonsNewTurn(currentPlayer);
        abilityScripts.First(x => x.type == AbilityType.Rocket).Button.interactable = false; // geen rocket in turn 1
    }

    public void OnAbilityButtonClick()
    {
        abilityScripts.ForEach(x => x.UnselectAbility());
    }

    private void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        CheckEnableButtonsNewTurn(currentPlayer);
        if(currentPlayer.GetComponent<PlayerTurnCount>().TurnCount <= 1)
        {
            abilityScripts.First(x => x.type == AbilityType.Rocket).Button.interactable = false; // geen rocket in turn 1
        }
    }

    private void CheckEnableButtonsNewTurn(PlayerScript currentPlayer)
    {
        if (GameHandler.instance.GameEnded) { return; }

        foreach (var abilityScript in abilityScripts)
        {
            abilityScript.Button.interactable = currentPlayer.IsOnMyNetwork();
            abilityScript.UnselectAbility();
        }

        EndTurnButtonScript.Button.interactable = currentPlayer.IsOnMyNetwork();
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.RoundEnded -= OnRoundEnded;
    }
}