using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private HexGrid HexGrid;
    private Vector3Int StartTileP1 = new Vector3Int(2, 0, 1);
    private Vector3Int StartTileP2 = new Vector3Int(11, 0, 9);


    private void Start()
    {
        HexGrid = FindObjectOfType<HexGrid>();
        ActionEvents.PlayerKilled += OnPlayerKilled;
    }    
    private void OnDestroy()
    {
        ActionEvents.PlayerKilled -= OnPlayerKilled;
    }

    private void OnPlayerKilled(PlayerScript playerKilled)
    {
        Textt.GameSync("Player killed, reset game in 5 sec");
        StartCoroutine(ResetInXSeconds(5));
    }

    private IEnumerator ResetInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetGame();
    }

    public void ResetGame()
    {
        var players = NetworkHelper.instance.GetPlayers();
        if(players.Count >= 1)
        {
            players[0].gameObject.SetActive(true);
            var startHexTile = HexGrid.GetTileAt(StartTileP1);
            players[0].transform.position = startHexTile.transform.position;
            players[0].CurrentHexTile = startHexTile;
        }
        if (players.Count >= 2)
        {
            players[1].gameObject.SetActive(true);
            var startHexTile = HexGrid.GetTileAt(StartTileP2);
            players[1].transform.position = startHexTile.transform.position;
            players[1].CurrentHexTile = startHexTile;
        }
    }

}
