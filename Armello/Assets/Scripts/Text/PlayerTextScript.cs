using TMPro;
using UnityEngine;

public class PlayerTextScript : MonoBehaviour
{
    [ComponentInject] private PlayerScript playerScript;
    [ComponentInject] private TMP_Text playerName;

    private void Awake()
    {
        this.ComponentInject();
    }   

    void Update()
    {
        playerName.text = playerScript.PlayerName;
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}