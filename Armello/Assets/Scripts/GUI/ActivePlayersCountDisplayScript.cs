using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ActivePlayersCountDisplayScript : MonoBehaviour
{
    public TMP_Text textNetwCountPlayers;
    public TMP_Text textCountAllPlayers;

    private void Awake()
    {
        this.ComponentInject();
    }


    private void Update()
    {
        textNetwCountPlayers.text = "Active real players: " + NetworkHelper.instance.PlayerList.Count();
        textCountAllPlayers.text = "All active players: " + NetworkHelper.instance.AllPlayers.Count();

    }
}
