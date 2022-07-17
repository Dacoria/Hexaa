using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MovementDisplayScript : MonoBehaviour
{
    [ComponentInject] public Button Button;

    private void Awake()
    {
        this.ComponentInject();
    }    

    public void OnMovementButtonClick()
    {
        HexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(GameHandler.instance.CurrentPlayer);
    }
}