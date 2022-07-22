using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAi : MonoBehaviour
{
    [ComponentInject] private PlayerScript player;
    private PlayerAiMove playerAiMove;

    private void Awake()
    {
        this.ComponentInject();
        this.playerAiMove = gameObject.AddComponent<PlayerAiMove>();
    }

    private void Start()
    {        
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += NewRoundStarted;
    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= NewRoundStarted;
    }

    private void NewRoundStarted(List<PlayerScript> players, PlayerScript currPlayer)
    {
        StartCoroutine(OnNewTurn(currPlayer));
    }

    private void OnNewPlayerTurn(PlayerScript currPlayer)
    {
        StartCoroutine(OnNewTurn(currPlayer));
    }

    private IEnumerator OnNewTurn(PlayerScript currPlayer)
    {
        yield return new WaitForSeconds(2f); // wacht op wijzigingen verwerken + wachttijd
        if (player.IsMyTurn() && player == currPlayer)
        {
            this.playerAiMove.DoTurn();
        }
    }
}
