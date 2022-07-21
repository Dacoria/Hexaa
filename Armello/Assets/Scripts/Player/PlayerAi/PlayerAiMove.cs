using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAiMove : MonoBehaviour
{
    [ComponentInject] private PlayerScript player;
    [ComponentInject] private PlayerAbilityPoints playerAbilityPoints;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void DoTurn()
    {
        var randomChoice = UnityEngine.Random.Range(0, 3); // excl. max!



    }
}
