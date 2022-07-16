using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHandler : MonoBehaviour
{
    public GameObject RocketPrefab;

    public void FireRocket()
    {
        Vector3 destination = transform.position + new Vector3(0,10,0);
        var rocketGo = Instantiate(RocketPrefab, destination, Quaternion.Euler(0,0,180f));
    }
}
