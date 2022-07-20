using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketHandler : MonoBehaviour
{
    public RocketScript RocketPrefab;
    [ComponentInject] private PlayerScript playerScript;

    private void Awake()
    {
        this.ComponentInject();
    }

    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
        ActionEvents.PlayerRocketHitTile += OnPlayerRocketHitTile;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;
    }

    public void FireRocket(Hex hexTarget)
    {
        NetworkActionEvents.instance.PlayerAbility(playerScript, hexTarget, AbilityType.Rocket);
    }

    public void OnPlayerAbility(PlayerScript playerThatFiresRocket, Hex hexTarget, AbilityType abilityType)
    {
        if(abilityType != AbilityType.Rocket)
        {
            return;
        }
        if(playerScript != playerThatFiresRocket)
        {
            return;
        }        

        Vector3 destination = hexTarget.transform.position + new Vector3(0, 15, 0);
        var rocketGo = Instantiate(RocketPrefab, destination, Quaternion.Euler(0, 0, 180f));
        rocketGo.Player = playerScript;
        rocketGo.HexTarget = hexTarget;
    }

    private void OnPlayerRocketHitTile(PlayerScript playerThatSendRocket, Hex hexHit, PlayerScript playerHit, bool hitPlayer)
    {
        if(playerScript == playerThatSendRocket)
        {
            hexHit.EnableHighlight(HighlightColorType.Pink);
        }
    }
}
