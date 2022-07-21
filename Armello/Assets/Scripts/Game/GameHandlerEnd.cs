using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    private void EndGameOnRocketHit()
    {
        NetworkActionEvents.instance.RoundEnded(false);
        StartCoroutine(ResetInXSeconds(5));
    }

    private void OnEndRound(bool reachedMiddle)
    {
        GameStatus = GameStatus.RoundEnded;
    }

    private void OnEndGame()
    {
        GameStatus = GameStatus.GameEnded;
    }
}
