using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [ComponentInject] private PlayerScript PlayerScript;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void DoMove(Hex selectedHex)
    {
        startPosition = transform.position;
        endPosition = selectedHex.transform.position;
        NewHexTile = selectedHex;

        rotatingIsActive = true;
        lerpingIsActive = true;
    }


    private bool rotatingIsActive;
    private bool lerpingIsActive;
    private float elapsedTime;
    private float desiredLerpDuration = 1;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Hex NewHexTile;

    [SerializeField] private AnimationCurve curve;

    void Update()
    {
        UpdateRotation();        
        UpdateMovement();
    }


    private Quaternion previousRotation;
    private void UpdateRotation()
    {
        if(!rotatingIsActive)
        {
            return;
        }
        if (Quaternion.Angle(transform.rotation, previousRotation) < 0.02f)
        {
            rotatingIsActive = false;
            return;
        }

        Vector3 targetDirection = endPosition - transform.position;
        float singleStep = 4 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        
        previousRotation = transform.rotation;
    }

    private void UpdateMovement()
    {
        if(rotatingIsActive)
        {
            return;
        }
        if (!lerpingIsActive)
        {
            return;
        }
        if (elapsedTime > desiredLerpDuration)
        {
            elapsedTime = 0;
            lerpingIsActive = false;
            PlayerScript.CurrentHexTile = NewHexTile;
            return;
        }

        elapsedTime += Time.deltaTime;
        float percComplete = elapsedTime / desiredLerpDuration;

        transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percComplete));
    }
}
