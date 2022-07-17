using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    private List<Vector3Int> StartPosTiles = new List<Vector3Int>
    {
        new Vector3Int(2, 0, 1),
        new Vector3Int(11, 0, 9)
    };

    public List<PlayerScript> AllPlayers;


    private void SetupNewGame()
    {
        var players = NetworkHelper.instance.GetPlayers().Take(StartPosTiles.Count).ToList();
        CurrentPlayer = players[0];
        NetworkActionEvents.instance.NewRoundStarted(players, CurrentPlayer);
    }

    public void ResetGame()
    {
        if (HexGrid.IsLoaded() && NetworkHelper.instance.GetPlayers().Count > 0)
        {
            SetupNewGame();
        }
        else
        {
            StartCoroutine(MonoHelper.instance.CallbackInXSeconds(0.1f, ResetGame));
        }
    }

    private IEnumerator ResetInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetGame();
    }

    private void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        ResetGameLocal();

        var playersMatch = players.Select(x => x.PlayerId).All(AllPlayers.Select(x => x.PlayerId).Contains);
        var sameSize = players.Count == AllPlayers.Count;

        // fix order....
        var playersRes = new List<PlayerScript>();
        for (int i = 0; i < players.Count; i++)
        {
            var pId = players[i].PlayerId;
            playersRes.Add(AllPlayers.Single(x => x.PlayerId == pId));
        }

        AllPlayers = playersRes;


        if (playersMatch && sameSize)
        {
            CurrentPlayer = currentPlayer;
            //ActionEvents.NewPlayerTurn?.Invoke(currentPlayer); // local; want OnNewRoundStarted is al via sync gegaan; status bekend
            return;
        }
        else
        {
            throw new Exception();
        }
    }

    private void ResetGameLocal()
    {
        var players = NetworkHelper.instance.GetPlayers().Take(StartPosTiles.Count).ToList();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(true);
            var startHexTile = HexGrid.GetTileAt(StartPosTiles[i]);
            players[i].transform.position = startHexTile.transform.position;
            players[i].CurrentHexTile = startHexTile;
        }

        AllPlayers = players;
    }
}
