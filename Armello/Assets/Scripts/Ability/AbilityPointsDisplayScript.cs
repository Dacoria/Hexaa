using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityPointsDisplayScript : MonoBehaviour
{
    [ComponentInject] private TMP_Text actionPointsText;
    public Image BarFilled;

    private void Awake()
    {
        this.ComponentInject();
        actionPointsText.text = "";
        BarFilled.fillAmount = 0;
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
        var playerAction = player.GetComponent<PlayerActionPoints>();
        actionPointsText.text = player.CurrentActionPoints() + "/" + playerAction.ActionPointsLimit;
        targetBarFilledAmount = player.CurrentActionPoints() / (float)playerAction.ActionPointsLimit;
    }

    private float targetBarFilledAmount;

    private void Update()
    {
        BarFilled.fillAmount = Mathf.Lerp(BarFilled.fillAmount, targetBarFilledAmount, 2 * Time.deltaTime);
    }

}
