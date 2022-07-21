using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameTextManager : MonoBehaviour
{
    private void Start()
    {
        ActionEvents.PlayerRocketHitTile += OnPlayerRocketHitTile;
        ActionEvents.EndRound += OnEndRound;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.PlayerAbility += OnPlayerAbility;
    }    

    private void OnPlayerRocketHitTile(PlayerScript playerOfRocket, Hex hex, PlayerScript playerHit, bool playerKilled)
    {
        if (playerHit != null)
        {
            // wordt geregeld via OnRoundEnded event; misschien later van toepassing bij hits en niet direct door + reset round
            return;
        }

        if (playerOfRocket.IsOnMyNetwork())
        {
            Textt.GameLocal("Missed!");
        }
        else
        {
            Textt.GameLocal(playerOfRocket.PlayerName + " Missed!");
        }
    }

    private void OnEndRound(bool reachedMiddle)
    {
        if (!reachedMiddle)
        {
            Textt.GameSync("Player killed, reset game in 5 sec");
        }
    }

    private void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        if(Netw.MyPlayer() == currentPlayer)
        {
            Textt.GameLocal("New round started. Your Turn");
        }
        else
        {
            Textt.GameLocal("New round started. Turn: " + currentPlayer.PlayerName);
        }
    }

    private void OnNewPlayerTurn(PlayerScript player)
    {
        if(Netw.MyPlayer() == player)
        {
            Textt.GameLocal("New turn, your move!");
        }
        else
        {
            Textt.GameLocal("Turn: " + player.PlayerName);
        }
    }

    private void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if (GameHandler.instance.GameStatus != GameStatus.ActiveRound) { return; }

        if(type == AbilityType.Radar)
        {
            Textt.GameSync("Radar shows options for player location (not the middle)");
        }
        else if (type == AbilityType.Vision)
        {
            Textt.GameSync(GameHandler.instance.CurrentPlayer.PlayerName + " is watching tile...");
        }

        // iets? nodig?
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;
        ActionEvents.EndRound -= OnEndRound;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }    
}
