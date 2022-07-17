using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public Hex VictoryHex;

    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        if(abilityType == AbilityType.Movement)
        {
            if(hex.HexCoordinates == VictoryHex.HexCoordinates)
            {
                Textt.GameSync("Victory! " + player.PlayerName + " has reached the middle!");
                ActionEvents.RoundEnded?.Invoke(true);
            }
        }
    }
}
