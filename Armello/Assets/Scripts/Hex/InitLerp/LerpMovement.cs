using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public IEnumerator MoveToDestination(Vector3 startPosition, Vector3 endPosition, float duration, float delayedStart = 0, Action callbackOnFinished = null, bool destroyOnFinished = true)
    {
        transform.position = startPosition;
        yield return new WaitForSeconds(delayedStart);

        float elapsedTime = 0f;
        var curve = MonoHelper.instance.CurveGradual;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percComplete = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percComplete));
            yield return null;
        }

        callbackOnFinished?.Invoke();
        if(destroyOnFinished) { Destroy(this); }
    }
}
