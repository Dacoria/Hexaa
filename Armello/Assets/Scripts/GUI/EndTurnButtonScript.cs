using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    [ComponentInject] public Button Button;

    private void Awake()
    {
        this.ComponentInject();    
    }

    public void OnEndTurnButtonClick()
    {
        if (GameHandler.instance.CurrentPlayer.IsOnMyNetwork())
        {
            GameHandler.instance.PlayerEndsTurn(GameHandler.instance.CurrentPlayer);
        }
    }
}
