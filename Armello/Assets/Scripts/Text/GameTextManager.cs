using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameTextManager : MonoBehaviour
{
    private void Start()
    {
        ActionEvents.PlayerRocketHitTile += OnPlayerRocketHitTile;
        ActionEvents.RoundEnded += OnRoundEnded;
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
            var otherPlayer = NetworkHelper.instance.OtherPlayerClosest(playerOfRocket, hex.transform.position);
            var distance = Vector3.Distance(otherPlayer.transform.position, hex.transform.position);

            var min = Mathf.Max(1, distance - 2);
            var max = distance + 2;

            var randomDist = (int)UnityEngine.Random.Range(min, max);
            Textt.GameLocal("Missed! Distance offset indication (+-2): " + randomDist);



        }
        else
        {
            Textt.GameLocal(playerOfRocket.PlayerName + " Missed!");
        }
    }

    private void OnRoundEnded(bool reachedMiddle)
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
        if (GameHandler.instance.GameEnded) { return; }
        if (type == AbilityType.Movement)
        {
            if(!player.IsOnMyNetwork())
            {
                Textt.GameLocal(player.PlayerName + " has moved...");
            }
        }

        // meer nodig?
    }

    private void OnDestroy()
    {
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;
        ActionEvents.RoundEnded -= OnRoundEnded;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
    }    
}
