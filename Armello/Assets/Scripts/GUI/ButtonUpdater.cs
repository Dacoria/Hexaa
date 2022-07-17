using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonUpdater : MonoBehaviour
{
    [ComponentInject] private RocketDisplayScript RocketDisplayScript;
    [ComponentInject] private MovementDisplayScript MovementDisplayScript;
    [ComponentInject] private EndTurnButtonScript EndTurnButtonScript;

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
    }

    private void OnRoundEnded(bool reachedMiddle)
    {
        MovementDisplayScript.Button.interactable = false;
        RocketDisplayScript.Button.interactable = false;
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
            var playerAction = player.GetComponent<PlayerAction>();
            var pointsLeft = playerAction.GetPointsLeft();

            MovementDisplayScript.Button.interactable = AbilityType.Movement.Cost() <= pointsLeft;
            RocketDisplayScript.Button.interactable = AbilityType.Rocket.Cost() <= pointsLeft;
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript currentPlayer)
    {
        CheckEnableButtonsNewTurn(currentPlayer);
    }

    private void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        CheckEnableButtonsNewTurn(currentPlayer);
    }

    private void CheckEnableButtonsNewTurn(PlayerScript currentPlayer)
    {
        if (GameHandler.instance.GameEnded) { return; }
        MovementDisplayScript.Button.interactable = currentPlayer.IsOnMyNetwork();
        RocketDisplayScript.Button.interactable = currentPlayer.IsOnMyNetwork();
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