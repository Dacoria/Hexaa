using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    public PlayerScript CurrentPlayer;

    public void PlayerEndsTurn(PlayerScript myPlayer)
    {
        NextTurn();
        ActionEvents.NewPlayerTurn(CurrentPlayer);
    }

    private void NextTurn()
    {
        CurrentPlayer = NextPlayer();
        NetworkActionEvents.instance.NewPlayerTurn(CurrentPlayer);
    }

    private PlayerScript NextPlayer()
    {
        var foundCurrPlayer = false;        

        for (int i = 0; i < AllPlayers.Count(); i++)
        {
            var player = AllPlayers[i];
            if (foundCurrPlayer)
            {
                return player;
            }

            if (player.PlayerId == CurrentPlayer.PlayerId)
            {
                foundCurrPlayer = true;
            }
        }

        return AllPlayers[0];
    }
}
