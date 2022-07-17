using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    public bool GameEnded;

    private void EndGameOnRocketHit()
    {
        NetworkActionEvents.instance.RoundEnded(false);
        StartCoroutine(ResetInXSeconds(5));
    }

    private void OnRoundEnded(bool reachedMiddle)
    {
        GameEnded = true;
    }
}
