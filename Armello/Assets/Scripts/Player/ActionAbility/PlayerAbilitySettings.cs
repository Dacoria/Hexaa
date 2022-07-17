using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAbilitySettings
{
    public static List<AbilitySetting> AbilitySettings = new List<AbilitySetting>
    {
        new AbilitySetting{Type = AbilityType.Rocket,      Cost = 3,   MaxPerTurn = 1},
        new AbilitySetting{Type = AbilityType.Movement,    Cost = 2,   MaxPerTurn = 1},
    };

    public static int Cost(this AbilityType abilityType) => AbilitySettings.Single(x => x.Type == abilityType).Cost;
    public static int MaxPerTurn(this AbilityType abilityType) => AbilitySettings.Single(x => x.Type == abilityType).MaxPerTurn;
}

public class AbilitySetting
{
    public int Cost;
    public AbilityType Type;
    public int MaxPerTurn;
}

public enum AbilityType
{
    Rocket,
    Movement
}