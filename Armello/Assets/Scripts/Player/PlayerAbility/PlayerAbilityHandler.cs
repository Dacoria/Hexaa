using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAbilityHandler : MonoBehaviour
{
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

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if (player == playerScript)
        {
            if (type == AbilityType.Rocket)
            {
                OnRocketAbility(player, hex);
            }
            else if (type == AbilityType.Vision)
            {
                OnVisionAbility(player, hex);
            }
            else if (type == AbilityType.Radar)
            {
                OnRadarAbility(player, hex);
            }
            else if (type == AbilityType.Movement)
            {
                OnMovementAbility(player, hex);
            }
        }
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;

    }
}