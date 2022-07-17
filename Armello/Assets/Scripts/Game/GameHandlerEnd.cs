using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    private void CheckEndRound()
    {
        NetworkActionEvents.instance.RoundEnded();
        StartCoroutine(ResetInXSeconds(5));
    }
}
