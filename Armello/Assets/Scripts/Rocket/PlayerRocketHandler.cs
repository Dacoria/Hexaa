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
        ActionEvents.FirePlayerRocket += OnFirePlayerRocket;
    }

    private void OnDestroy()
    {
        ActionEvents.FirePlayerRocket -= OnFirePlayerRocket;
    }

    public void FireRocket(Hex hexTarget)
    {        

        NetworkActionEvents.instance.PlayerRocketFired(playerScript, hexTarget);
    }

    public void OnFirePlayerRocket(PlayerScript playerThatFiresRocket, Hex hexTarget)
    {
        if (playerScript.IsAi)
        {
            return; // wordt al los afgevuurd door de echte speler
        }

        Vector3 destination = hexTarget.transform.position + new Vector3(0, 15, 0);
        var rocketGo = Instantiate(RocketPrefab, destination, Quaternion.Euler(0, 0, 180f));
        rocketGo.Player = playerScript;
        rocketGo.HexTarget = hexTarget;
    }
}
