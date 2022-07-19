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
        var players = NetworkHelper.instance.GetPlayers().OrderBy(x => x.PlayerId).Take(StartPosTiles.Count).ToList();
        CurrentPlayer = players[0];
        NetworkActionEvents.instance.NewRoundStarted(players, CurrentPlayer);
    }

    public void ResetGame()
    {
        StartCoroutine(CR_ResetGame());
    }

    public IEnumerator CR_ResetGame()
    {
        yield return new WaitForSeconds(0.1f);
        if (HexGrid.IsLoaded() && NetworkHelper.instance.GetPlayers().Count > 0)
        {
            SetupNewGame();
        }
        else
        {
            StartCoroutine(CR_ResetGame());
        }
    }

    private IEnumerator ResetInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetGame();
    }

    private void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        GameEnded = false;

        // refresh om te checken
        AllPlayers = NetworkHelper.instance.GetPlayers().Take(StartPosTiles.Count).ToList();

        // check
        var playersMatch = players.Select(x => x.PlayerId).All(AllPlayers.Select(x => x.PlayerId).Contains);
        var sameSize = players.Count == AllPlayers.Count;
        if (!playersMatch || !sameSize) { throw new Exception(); }

        // fix order....
        var playersRes = new List<PlayerScript>();
        for (int i = 0; i < players.Count; i++)
        {
            playersRes.Add(AllPlayers.Single(x => x.PlayerId == players[i].PlayerId));
        }
        AllPlayers = playersRes;

        // reset local
        ResetGameLocal();

        CurrentPlayer = currentPlayer;        
    }

    private void ResetGameLocal()
    {
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            var startHexTile = HexGrid.GetTileAt(StartPosTiles[i]);
            AllPlayers[i].transform.position = startHexTile.transform.position;
            AllPlayers[i].CurrentHexTile = startHexTile;
        }
    }
}