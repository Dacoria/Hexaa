using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosionHandler : MonoBehaviour
{  
    public void Start()
    {
        ActionEvents.PlayerRocketHit += OnPlayerRocketHit;
    }

    public void Destroy()
    {
        ActionEvents.PlayerRocketHit -= OnPlayerRocketHit;
    }

    private void OnPlayerRocketHit(PlayerScript playerWhoShotRocket, Hex hexTileHit)
    {
        var allPlayers = NetworkHelper.instance.GetPlayers();
        foreach (var player in allPlayers)
        {
            if (player.CurrentHexTile == hexTileHit)
            {
                // voor nu -> altijd dood met 1 raket-hit --> KILL
                playerWhoShotRocket.gameObject.SetActive(false);
                ActionEvents.PlayerKilled?.Invoke(playerWhoShotRocket);
            }
        }
    }
}
