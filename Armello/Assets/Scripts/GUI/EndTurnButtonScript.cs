using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    [ComponentInject] private Button button;
    private void Awake()
    {
        this.ComponentInject();
    }
    
    public void OnEndTurnButtonClick()
    {
        if (GameHandler.instance.CurrentPlayer == Netw.MyPlayer())
        {
            GameHandler.instance.PlayerEndsTurn(Netw.MyPlayer());
        }
    }
}
