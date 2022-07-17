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
        RefreshPlayerGos();
    }

    private void Start()
    {
        PlayerList = PhotonNetwork.PlayerList;
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public List<PlayerScript> GetPlayerList()
    {
        return AllPlayers;
    }

    public PlayerScript GetMyPlayer()
    {
        var players = AllPlayers;
        return players.FirstOrDefault();
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