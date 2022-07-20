using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SpriteAbilityHeightCorrection : MonoBehaviour
{
    [ComponentInject] private Button Button;    
    [ComponentInject] private Image ImageAbility;

    private Image ImageButton;
    private Vector3 origPosAbilImage;

    private void Awake()
    {
        this.ComponentInject();
        this.ImageButton = Button.GetComponent<Image>();

        this.origPosAbilImage = ImageAbility.transform.position;
    }

    private void Update()
    {
        if (ImageButton.sprite.name.Contains("pressed"))
        {
            ImageAbility.transform.position = origPosAbilImage + new Vector3(0, -15, 0);
        }
        else
        {
            ImageAbility.transform.position = origPosAbilImage;
        }
    }
}
