using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
    [SerializeField] private AnimationCurve curve;
    private Hex NewHexTile;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void DoMove(Hex selectedHex)
    {        
        NewHexTile = selectedHex;
        StartCoroutine(RotateTowardsDestination(NewHexTile.transform.position, OnRotationFinished));
    }

    private void OnRotationFinished()
    {
        StartCoroutine(MoveToDestination(NewHexTile.transform.position, 1, OnMovingFinished));
    }

    private void OnMovingFinished()
    {
        playerScript.CurrentHexTile = NewHexTile;
        NetworkActionEvents.instance.PlayerHasMoved(playerScript, NewHexTile);
    }

    private IEnumerator MoveToDestination(Vector3 endPosition, float duration, Action callbackOnFinished)
    {
        float elapsedTime = 0f;
        var startPosition = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percComplete = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percComplete));
            yield return null;
        }

        callbackOnFinished();
    }

    private float previousAngleDiff;

    private IEnumerator RotateTowardsDestination(Vector3 endPosition, Action callbackOnFinished)
    {
        float elapsedTime = 0f;
        var targetDirection = endPosition - transform.position;

        while (elapsedTime < 3)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 4 * Time.deltaTime, 0.0f);

            var currentAngleDiff = Vector3.Angle(newDirection, targetDirection);
            if (Math.Abs(currentAngleDiff - previousAngleDiff) < 0.01) { 
                break; 
            }

            transform.rotation = Quaternion.LookRotation(newDirection);

            previousAngleDiff = currentAngleDiff;
            yield return null;
        }

        callbackOnFinished();
    }
}