using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayerScript : MonoBehaviour
{
    [ComponentInject] private Button button;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void OnButtonClick()
    {
        SpawnPlayers.instance.SpawnDummyPlayer();
    }

    private bool hasTwoPlayers;

    private void Update()
    {
        if (GameHandler.instance.GameStatus == GameStatus.GameEnded)
        {
            button.interactable = false;
        }
        else if (!hasTwoPlayers)
        {
            hasTwoPlayers = NetworkHelper.instance.AllPlayers.Count() >= 2;
        }
        else
        {
            button.interactable = false;
        }
    }
}
