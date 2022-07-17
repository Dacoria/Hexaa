using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [ComponentInject] private PlayerScript player;

    private void Awake()
    {
        this.ComponentInject();
    }

    private void Update()
    {
        DetectMouseClick();
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0) &&
            player.PlayerId == NetworkHelper.instance.GetMyPlayer().PlayerId)
        {
            Vector3 mousePos = Input.mousePosition;
            HexTileSelectionManager.instance.HandleMouseClick(mousePos);
        }
    }
}
