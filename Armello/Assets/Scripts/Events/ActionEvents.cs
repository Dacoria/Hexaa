using System;
using System.Collections.Generic;

public static class ActionEvents
{
    public static Action<PlayerScript, Hex> PlayerRocketHit;
    public static Action<PlayerScript> PlayerKilled;

    public static Action<PlayerScript, Hex> PlayerHasMoved;

    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;

}