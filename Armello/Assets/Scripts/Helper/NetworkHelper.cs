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

    private void Awake()
    {
        instance = this;
    }

    public PlayerScript GetMyPlayer()
    {
        var playerGos = GameObject.FindGameObjectsWithTag(Statics.TAG_PLAYER).ToList();
        return playerGos[0].GetComponent<PlayerScript>();
    }
}