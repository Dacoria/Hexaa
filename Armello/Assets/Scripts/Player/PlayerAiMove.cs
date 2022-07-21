using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAiMove : MonoBehaviour
{
    [ComponentInject] private PlayerScript player;
    //private List<AbilityPointsDisplayScript>

    private void Awake()
    {
        this.ComponentInject();
    }

    public void DoTurn()
    {
        //var abilities = AbilityType.get
    }
}
