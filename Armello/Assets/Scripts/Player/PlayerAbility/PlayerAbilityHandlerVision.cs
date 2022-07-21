using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAbilityHandler : MonoBehaviour
{
    private void OnVisionAbility(PlayerScript playerDoingAbility, Hex target)
    {        
        if (playerDoingAbility.IsMyTurn())
        {
            target.SetFogOnHex(false); // local!
        }

        target.EnableHighlight(HighlightColorType.Yellow);

               
    }
}