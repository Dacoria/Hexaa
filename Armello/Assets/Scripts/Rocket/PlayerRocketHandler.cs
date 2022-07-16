using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketHandler : MonoBehaviour
{
    public RocketScript RocketPrefab;
    [ComponentInject] private PlayerScript playerScript;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void FireRocket(Hex hexTarget)
    {
        Vector3 destination = hexTarget.transform.position + new Vector3(0,15,0);
        var rocketGo = Instantiate(RocketPrefab, destination, Quaternion.Euler(0,0,180f));
        rocketGo.Player = playerScript;
        rocketGo.HexTarget = hexTarget;
    }   
}
