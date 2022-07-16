using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketHandler : MonoBehaviour
{
    public GameObject RocketPrefab;

    public void FireRocket(Hex hexTarget)
    {
        Vector3 destination = hexTarget.transform.position + new Vector3(0,15,0);
        var rocketGo = Instantiate(RocketPrefab, destination, Quaternion.Euler(0,0,180f));
    }
}
