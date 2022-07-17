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
    private HexGrid HexGrid;
    public int PlayerId;
    public int SentServerTimestamp;
    public string PlayerName;

    public bool HasDoneMovementThisTurn;
    public bool HasFiredRocketThisTurn;

    private void OnEnable()
    {
        StartCoroutine(InitSetCurrentHexTile());
    }

    private IEnumerator InitSetCurrentHexTile()
    {
        yield return new WaitForSeconds(0.5f);
        HexGrid = FindObjectOfType<HexGrid>();

        var tilePos = transform.position.ConvertPositionToOffset();
        CurrentHexTile = HexGrid.GetTileAt(tilePos);

        transform.position = CurrentHexTile.transform.position;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        var name = instantiationData[0].ToString();
        IsAi = bool.Parse(instantiationData[1].ToString());
        var hosterCounterId = int.Parse(instantiationData[2].ToString());
        SentServerTimestamp = info.SentServerTimestamp;


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
        gameObject.SetActive(false);



        // DEBUG CODE
        GameHandler.instance.ResetGame();
    }
}
