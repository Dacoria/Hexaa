using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ActionPointsDisplayScript : MonoBehaviour
{
    [ComponentInject] private TMP_Text actionPointsText;

    private void Awake()
    {
        this.ComponentInject();
    }

    private void Start()
    {
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }

    private void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        TryUpdateActionPoints(currentPlayer);
    }

    private void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        TryUpdateActionPoints(currentPlayer);
    }

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        TryUpdateActionPoints(player);
    }

    private void TryUpdateActionPoints(PlayerScript player)
    {
        if(player.IsOnMyNetwork())
        {
            StartCoroutine(UpdateActionPoints(player));            
        }
        else
        {
            actionPointsText.text = "";
        }
    }

    private IEnumerator UpdateActionPoints(PlayerScript player)
    {
        // zodat de waardes verwerkt kunnen worden
        yield return new WaitForSeconds(0.1f);
        var a = player.PlayerName;
        var b = player.GetComponent<PlayerAction>();
        var c = player.GetComponent<PlayerAction>().GetPointsLeft();

        actionPointsText.text = player.GetComponent<PlayerAction>().GetPointsLeft().ToString();
    }
    
}
