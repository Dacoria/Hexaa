using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGameButtonScript : MonoBehaviour
{
    [ComponentInject] private Button button;
    [ComponentInject] private TMP_Text text;

    private void Awake()
    {
        this.ComponentInject();
    }

    private bool hasStartedFirstGame;
    private bool hasAtLeastTwoPlayers;

    public void OnButtonClick()
    {
        GameHandler.instance.ResetGame();
    }

    private void Update()
    {
        if (!hasAtLeastTwoPlayers)
        {
            hasAtLeastTwoPlayers = NetworkHelper.instance.AllPlayers.Count() >= 2;
        }

        text.text = hasStartedFirstGame ? "Reset" : "Start";
        button.interactable = hasAtLeastTwoPlayers;
    }
}
