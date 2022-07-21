using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour, IPunInstantiateMagicCallback
{
    public Hex CurrentHexTile;
    public bool IsAi;
    public int PlayerId;
    public string PlayerName;

    [ComponentInject] private PhotonView photonView;

    private void Awake()
    {
        this.ComponentInject();
    }
    
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        var name = instantiationData[0].ToString();
        IsAi = bool.Parse(instantiationData[1].ToString());
        var hosterCounterId = int.Parse(instantiationData[2].ToString());

        if (PhotonNetwork.OfflineMode || IsAi)
        {
            PlayerId = hosterCounterId + 1000; // forceert dat het anders is dat het photonId
        }
        else
        {
            PlayerId = info.photonView.OwnerActorNr;
        }

        NetworkHelper.instance.RefreshPlayerGos();
        PlayerName = name;
        GetComponentInChildren<PlayerModel>(true).gameObject.SetActive(false); // begin met onzichtbaar model
    }
}
