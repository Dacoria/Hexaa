﻿using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkActionEvents : MonoBehaviour
{
    [ComponentInject] private PhotonView photonView;
    public static NetworkActionEvents instance;

    private void Awake()
    {
        instance = this;
        this.ComponentInject();
    }

    public void PlayerRocketFired(PlayerScript playerWhoShotRocket, Hex hexTile)
    {
        photonView.RPC("RPC_AE_PlayerRocketFired", RpcTarget.All, playerWhoShotRocket.PlayerId, (Vector3)hexTile.HexCoordinates);
    }

    [PunRPC]
    public void RPC_AE_PlayerRocketFired(int pIdWhoShotRocket, Vector3 hexTile)
    {
        ActionEvents.FirePlayerRocket?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex());
    }

    public void PlayerRocketHitTile(PlayerScript playerWhoShotRocket, Hex hexTile, PlayerScript hitByRocket, bool playerKilled)
    {
        photonView.RPC("RPC_AE_PlayerRocketHitTile", RpcTarget.All, playerWhoShotRocket.PlayerId, (Vector3)hexTile.HexCoordinates, hitByRocket == null ? -1 : hitByRocket.PlayerId, playerKilled);
    }

    [PunRPC]
    public void RPC_AE_PlayerRocketHitTile(int pIdWhoShotRocket, Vector3 hexTile, int pIdhitByRocket, bool playerKilled)
    {
        ActionEvents.PlayerRocketHitTile?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex(), pIdhitByRocket == -1 ? null : pIdhitByRocket.GetPlayer(), playerKilled);
    }

    public void PlayerHasMoved(PlayerScript player, Hex hexTile)
    {
        photonView.RPC("RPC_AE_PlayerHasMoved", RpcTarget.All, player.PlayerId, (Vector3)hexTile.HexCoordinates);
    }

    [PunRPC]
    public void RPC_AE_PlayerHasMoved(int pId, Vector3 hexTile)
    {
        ActionEvents.PlayerHasMoved?.Invoke(pId.GetPlayer(), hexTile.GetHex());
    }

    public void NewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_NewRoundStarted", RpcTarget.All, players.Select(x => x.PlayerId).ToArray(), currentPlayer.PlayerId);
    }

    [PunRPC]
    public void RPC_AE_NewRoundStarted(int[] playerIds, int currentPlayerId)
    {
        ActionEvents.NewRoundStarted?.Invoke(playerIds.Select(x => x.GetPlayer()).ToList(), currentPlayerId.GetPlayer());
    }

    public void RoundEnded()
    {
        photonView.RPC("RPC_AE_RoundEnded", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_AE_RoundEnded()
    {
        ActionEvents.RoundEnded?.Invoke();
    }

    public void NewPlayerTurn(PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, currentPlayer.PlayerId);
    }

    [PunRPC]
    public void RPC_AE_NewPlayerTurn(int currentPlayerId)
    {
        ActionEvents.NewPlayerTurn?.Invoke(currentPlayerId.GetPlayer());
    }
}