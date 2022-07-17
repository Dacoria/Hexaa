using System.Collections.Generic;
using UnityEngine;

public class GameTextManager : MonoBehaviour
{
    private void Start()
    {
        ActionEvents.PlayerRocketHitTile += OnPlayerRocketHitTile;
        ActionEvents.RoundEnded += OnRoundEnded;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.PlayerHasMoved += OnPlayerHasMoved;
    }    

    private void OnPlayerRocketHitTile(PlayerScript playerOfRocket, Hex hex, PlayerScript playerHit, bool playerKilled)
    {
        if (playerHit != null)
        {
            // wordt geregeld via OnRoundEnded event; misschien later van toepassing bij hits en niet direct door + reset round
            return;
        }

        if (Netw.MyPlayer() == playerOfRocket)
        {
            Textt.GameSync("Missed!");
        }
        else
        {
            Textt.GameSync(playerOfRocket.PlayerName + " Missed!");
        }
    }

    private void OnRoundEnded()
    {
        Textt.GameSync("Player killed, reset game in 5 sec");
    }

    private void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        if(Netw.MyPlayer() == currentPlayer)
        {
            Textt.GameSync("New round started. Your Turn");
        }
        else
        {
            Textt.GameSync("New round started. Turn: " + currentPlayer.PlayerName);
        }
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        if(Netw.MyPlayer() == player)
        {
            Textt.GameSync("New turn, your move!");
        }
        else
        {
            Textt.GameSync("Turn: " + player.PlayerName);
        }
    }

    private void OnPlayerHasMoved(PlayerScript player, Hex hex)
    {
        if (Netw.MyPlayer() == player)
        {
            // update niet nodig lijkt me
        }
        else
        {
            Textt.GameSync(player.PlayerName + " has moved...");
        }
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;
        ActionEvents.RoundEnded -= OnRoundEnded;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.PlayerHasMoved -= OnPlayerHasMoved;
    }
}
