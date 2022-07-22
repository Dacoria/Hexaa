using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    public PlayerScript currentPlayer;
    public PlayerScript CurrentPlayer() => currentPlayer;

    public void PlayerEndsTurn(PlayerScript myPlayer)
    {
        NextTurn();
    }

    private void NextTurn()
    {
        currentPlayer = NextPlayer();
        NetworkActionEvents.instance.NewPlayerTurn(CurrentPlayer());
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

            if (player.PlayerId == currentPlayer.PlayerId)
            {
                foundCurrPlayer = true;
            }
        }

        return AllPlayers[0];
    }
}
