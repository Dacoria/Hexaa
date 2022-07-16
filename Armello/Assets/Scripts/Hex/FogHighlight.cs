using System.Collections.Generic;
using UnityEngine;

public class FogHighlight : MonoBehaviour
{
    [ComponentInject] private ParticleSystem particleSystem;

    public bool isFogActive = false;

    private void Awake()
    {
        this.ComponentInject();
        this.UpdateFog();
    }
    private void UpdateFog()
    {
        particleSystem.gameObject.SetActive(isFogActive);
    }

    public void SetFog(bool isFogActive)
    {
        this.isFogActive = isFogActive;
    }


    private bool oldFogIsActive;
    private void Update()
    {
        if (oldFogIsActive != this.isFogActive)
        {
            UpdateFog();
        }

        oldFogIsActive = this.isFogActive;
    }
}