using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerIdleFunStuffDisplay : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
    [ComponentInject] private PlayerMovement playerMovement;
    [ComponentInject] private Animator animator;

    private float defaultWaitTimeForCheck = 5;

    private void Awake()
    {
        this.ComponentInject();
        StartCoroutine(CheckEveryXSeconds(0));
    }    

    private void Start()
    {
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }

    private void OnPlayerAbility(PlayerScript player, Hex arg2, AbilityType type)
    {
        if(playerScript == player && type == AbilityType.Movement)
        {
            ResetWaitAnimation();
            StopAllCoroutines();
            StartCoroutine(CheckEveryXSeconds(10));
        }
    }

    private IEnumerator CheckEveryXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CheckForIdleFunStuff();
        StartCoroutine(CheckEveryXSeconds(defaultWaitTimeForCheck));
    }

    private Coroutine rotationCoroutine;

    private void CheckForIdleFunStuff()
    {
        var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if(!clipInfo.Any())
        {
            return;
        }

        var currAnimationName = clipInfo[0].clip.name;

        if(currAnimationName.Contains("Idle"))
        {
            StartCoroutine(playerMovement.RotateTowardsDestination(transform.position + new Vector3(0, 0, -10)));
            TryNewWaitAnimation();            
        }
        else
        {
            if(currAnimationName.Contains("Dance") || currAnimationName.Contains("Hop"))
            {
                TryNewWaitAnimation();
            }
            else
            {
                ResetWaitAnimation();
            }            
        }
    }

    private List<string> WaitAnimationBoolParams = new List<string>
    {
        Statics.ANIMATION_BOOL_DANCE,
        Statics.ANIMATION_BOOL_HOP,
    };

    private List<string> WaitAnimationTriggerParams = new List<string>
    {
        Statics.ANIMATION_TRIGGER_WAVE,
        Statics.ANIMATION_TRIGGER_SPINNING,
    };

    private void ResetWaitAnimation()
    {
        foreach(var waitAnimationBoolParam in WaitAnimationBoolParams)
        {
            animator.SetBool(waitAnimationBoolParam, false);
        }    
    }

    private void TryNewWaitAnimation()
    {
        ResetWaitAnimation();

        var range = UnityEngine.Random.Range(0, 4);
        if (range <= 1)
        {
            WaitAnimationBoolParams.Shuffle();
            animator.SetBool(WaitAnimationBoolParams[0], true);
        }
        else if (range == 2)
        {
            WaitAnimationTriggerParams.Shuffle();
            animator.SetTrigger(WaitAnimationTriggerParams[0]);
        }
    }

    


}