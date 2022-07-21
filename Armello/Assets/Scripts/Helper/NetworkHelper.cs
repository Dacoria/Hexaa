using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHelper : MonoBehaviourPunCallbacks
{
    public static NetworkHelper instance;
    public GameTexts GameTexts;

    public List<PlayerScript> AllPlayers;

    [ComponentInject] private PhotonView photonView;

    public Photon.Realtime.Player[] PlayerList;

    private void Awake()
    {
        instance = this;
        this.ComponentInject();
    }

    private void Start()
    {
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public void RefreshPlayerGos()
    {
        // voor nu: alleen toevoegen (want door tags pak je niet inactieve obj)
        var playersEnabledWithTag = GameObject.FindGameObjectsWithTag(Statics.TAG_PLAYER).Select(x => x.GetComponent<PlayerScript>()).ToList();

        foreach(var player in playersEnabledWithTag)
        {
            if(!AllPlayers.Any(x => x.PlayerId == player.PlayerId))
            {
                AllPlayers.Add(player);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        ActionEvents.EndGame?.Invoke();
        Textt.GameLocal("A player has left the game! This is not supported. Reconnect for a new game");

        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public PlayerScript OtherPlayerClosest(PlayerScript me)
    {
        return OtherPlayerClosest(me, me.transform.position);
    }

    public PlayerScript OtherPlayerClosest(PlayerScript me, Vector3 positionToCompareDistance)
    {
        return AllPlayers.Where(x => x != me)
            .OrderBy(x => Vector3.Distance(x.transform.position, positionToCompareDistance))
            .FirstOrDefault();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }  

    public List<PlayerScript> GetPlayerList()
    {
        return AllPlayers;
    }

    public List<PlayerScript> GetMyPlayers(bool includeAi)
    {
        var players = AllPlayers;
        var res = players
            .Where(x => includeAi || !x.IsAi)
            .Where(x => x.GetComponent<PhotonView>().OwnerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            .ToList();

        return res;
    }

    public PlayerScript GetMyPlayer()
    {
        return GetMyPlayers(false).FirstOrDefault();
    }

    public List<PlayerScript> GetPlayers()
    {
        return AllPlayers;
    }

    public void SetGameText(string gameText, bool network)
    {
        if (network)
        {
            photonView.RPC("RPC_SetGameText", RpcTarget.All, gameText);
        }
        else
        {
            GameTexts.GameText.text = gameText;
        }
    }

    [PunRPC]
    public void RPC_SetGameText(string gameText)
    {
        GameTexts.GameText.text = gameText;
    }

    public void SetActionText(string actionText, bool network)
    {
        if (network)
        {
            photonView.RPC("RPC_SetActionText", RpcTarget.All, actionText);
        }
        else
        {
            GameTexts.ActionText.text = actionText;
        }
    }

    [PunRPC]
    public void RPC_SetActionText(string actionText)
    {
        GameTexts.ActionText.text = actionText;
    }    
}