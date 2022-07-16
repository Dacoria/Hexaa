using System;
using System.Collections.Generic;

public static class ActionEvents
{
    public static Action<PlayerScript, Hex> PlayerRocketHit;
    public static Action<PlayerScript> PlayerKilled;
}