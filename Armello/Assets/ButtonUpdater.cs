using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonUpdater : MonoBehaviour
{
    [ComponentInject] private RocketDisplayScript RocketDisplayScript;
    [ComponentInject] private MovementDisplayScript MovementDisplayScript;

    private void Awake()
    {
        this.ComponentInject();
    }

    private void Start()
    {
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.PlayerHasMoved += OnPlayerHasMoved;
        ActionEvents.PlayerHasMoved += OnPlayerHasMoved;
    }

    private void OnPlayerHasMoved(PlayerScript currentPlayer, Hex hex)
    {
        if (Netw.MyPlayer() == currentPlayer)
        {
            MovementDisplayScript.button.interactable = false;
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
        if (Netw.MyPlayer() == currentPlayer)
        {
            MovementDisplayScript.button.interactable = true;
            RocketDisplayScript.button.interactable = true;
        }

    }

    private void OnDestroy()
    {
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
    }
}
