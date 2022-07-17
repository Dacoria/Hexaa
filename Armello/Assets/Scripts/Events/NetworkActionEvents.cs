using Photon.Pun;
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
    
    public void PlayerRocketHitTile(PlayerScript playerWhoShotRocket, Hex hexTile, PlayerScript hitByRocket, bool playerKilled)
    {
        photonView.RPC("RPC_AE_PlayerRocketHitTile", RpcTarget.All, playerWhoShotRocket.PlayerId, (Vector3)hexTile.HexCoordinates, hitByRocket == null ? -1 : hitByRocket.PlayerId, playerKilled);
    }

    [PunRPC]
    public void RPC_AE_PlayerRocketHitTile(int pIdWhoShotRocket, Vector3 hexTile, int pIdhitByRocket, bool playerKilled)
    {
        ActionEvents.PlayerRocketHitTile?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex(), pIdhitByRocket == -1 ? null : pIdhitByRocket.GetPlayer(), playerKilled);
    }

    public void PlayerAbility(PlayerScript player, Hex hexTile, AbilityType abilityType)
    {
        photonView.RPC("RPC_AE_PlayerAbility", RpcTarget.All, player.PlayerId, (Vector3)hexTile.HexCoordinates, abilityType);
    }

    [PunRPC]
    public void RPC_AE_PlayerAbility(int pId, Vector3 hexTile, AbilityType abilityType)
    {
        ActionEvents.PlayerAbility?.Invoke(pId.GetPlayer(), hexTile.GetHex(), abilityType);
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

    public void RoundEnded(bool reachedMiddle)
    {
        photonView.RPC("RPC_AE_RoundEnded", RpcTarget.All, reachedMiddle);
    }

    [PunRPC]
    public void RPC_AE_RoundEnded(bool reachedMiddle)
    {
        ActionEvents.RoundEnded?.Invoke(reachedMiddle);
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