using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAbilityHandler : MonoBehaviour
{
    [ComponentInject] private PlayerMovement PlayerMovement;

    private Hex newHexTile;
    private PlayerScript movingPlayer;

    private void OnMovementAbility(PlayerScript playerDoingAbility, Hex target)
    {
        movingPlayer = playerDoingAbility;
        newHexTile = target;
        StartCoroutine(PlayerMovement.RotateTowardsDestination(newHexTile.transform.position, OnRotationFinished));
    }

    private void OnRotationFinished()
    {
        StartCoroutine(PlayerMovement.MoveToDestination(newHexTile.transform.position, duration: 1, callbackOnFinished: OnMovingFinished));
    }    

    private void OnMovingFinished()
    {
        movingPlayer.transform.position = newHexTile.transform.position;
        movingPlayer.CurrentHexTile = newHexTile;
    }
}