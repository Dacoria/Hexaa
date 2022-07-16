using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHelper : MonoBehaviour
{
    public static NetworkHelper instance;
    public GameTexts GameTexts;

    public List<PlayerScript> AllPlayers;

    private void Awake()
    {
        instance = this;
        AllPlayers = GameObject.FindGameObjectsWithTag(Statics.TAG_PLAYER).Select(x => x.GetComponent<PlayerScript>()).ToList();
    }

    public PlayerScript GetMyPlayer()
    {
        var players = AllPlayers;
        return players[0];
    }

    public List<PlayerScript> GetPlayers()
    {
        return AllPlayers;
    }

    public void SetGameText(string gameText, bool network)
    {
        //if (network)
        //{
        //    photonView.RPC("RPC_SetGameText", RpcTarget.All, gameText);
        //}
        //else
        //{
            GameTexts.GameText.text = gameText;
        //}
    }

    //[PunRPC]
    //public void RPC_SetGameText(string gameText)
    //{
    //    GameTexts.GameText.text = gameText;
    //}

    public void SetActionText(string actionText, bool network)
    {
        //if (network)
        //{
        //    photonView.RPC("RPC_SetActionText", RpcTarget.All, actionText);
        //}
        //else
        //{
            GameTexts.ActionText.text = actionText;
        //}
    }

    //[PunRPC]
    //public void RPC_SetActionText(string actionText)
    //{
    //    GameTexts.ActionText.text = actionText;
    //}
}