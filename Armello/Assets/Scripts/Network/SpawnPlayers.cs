using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private void Start()
    {

        SpawnPlayer("P" + GetPlayerCounter(), false);
    }

    private int GetPlayerCounter()
    {
        var currentPlayers = NetworkHelper.instance.GetPlayerList();
        var currentGoObjects = NetworkHelper.instance.GetPlayers();

        var playerCount = Mathf.Max(currentPlayers.Count, currentGoObjects.Count);
        return playerCount;
    }

    public void SpawnDummyPlayer()
    {
        var playerCounter = GetPlayerCounter();
        if (playerCounter == 1)
        {
            SpawnPlayer("P2", true);
        }


        //else if (playerCounter == 2)
        //{
        //    SpawnPlayer("P3", true);
        //}
        //else if (playerCounter == 3)
        //{
        //    SpawnPlayer("P4", true);
        //}
    }

    public void SpawnPlayer(string name, bool isDummy)
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        var isAi = isDummy && !PhotonNetwork.OfflineMode; // offline = altijd dummy zonder AI

        if (!PhotonNetwork.OfflineMode && !isAi)
        {
            name = PhotonNetwork.NickName;
        }

        object[] myCustomInitData = new List<object> { name, isAi, GetPlayerCounter() }.ToArray();
        var player = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(0, 0), Quaternion.identity, 0, myCustomInitData);
    }
}