using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAbilityHandler : MonoBehaviour
{
    public RocketScript RocketPrefab;

    private void OnRocketAbility(PlayerScript playerDoingAbility, Hex target)
    {
        if (playerDoingAbility.IsMyTurn())
        {
            target.SetFogOnHex(false); // local!
        }

        Vector3 destination = target.transform.position + new Vector3(0, 15, 0);
        var rocketGo = Instantiate(RocketPrefab, destination, Quaternion.Euler(0, 0, 180f));
        rocketGo.Player = playerScript;
        rocketGo.HexTarget = target;
    }

    private void OnPlayerRocketHitTile(PlayerScript playerThatSendRocket, Hex hexHit, PlayerScript playerHit, bool hitPlayer)
    {
        if (playerScript == playerThatSendRocket)
        {
            hexHit.EnableHighlight(HighlightColorType.Pink);

            if (hitPlayer)
            {
                // voor nu -> altijd dood met 1 raket-hit --> KILL
                playerHit.gameObject.GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(false); // begin met onzichtbaar model
            }

            GameHandler.instance.PlayerRocketHitTile(playerThatSendRocket, hexHit, playerHit, hitPlayer);
        }
    }
}