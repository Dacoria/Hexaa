using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilitySetup
{
    public static List<AbilitySetting> AbilitySettings = new List<AbilitySetting>
    {
        // MaxPerTurn = ongebruikt

        new AbilitySetting{Type = AbilityType.Rocket,   Cost = 3,   MaxPerTurn = 1, AvailableFromTurn = 2},
        new AbilitySetting{Type = AbilityType.Movement, Cost = 3,   MaxPerTurn = 1, AvailableFromTurn = 1},
        new AbilitySetting{Type = AbilityType.Radar,    Cost = 2,   MaxPerTurn = 1, AvailableFromTurn = 1},
        new AbilitySetting{Type = AbilityType.Vision,   Cost = 1,   MaxPerTurn = 1, AvailableFromTurn = 1},
    };
}