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

    private void NewRoundStarted(List<PlayerScript> players, PlayerScript player)
    {
        StartCoroutine(OnNewTurn(player));
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        StartCoroutine(OnNewTurn(player));
    }

    private IEnumerator OnNewTurn(PlayerScript player)
    {
        yield return new WaitForSeconds(0.1f); // wacht op wijzigingen verwerken;
        if(player.IsMyTurn())
        {
            this.playerAiMove.DoTurn();
        }
    }
}
