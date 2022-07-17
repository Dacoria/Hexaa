using System;
using System.Collections.Generic;

public static class ActionEvents
{
    public static Action<PlayerScript, Hex> FirePlayerRocket;
    public static Action<PlayerScript, Hex, PlayerScript, bool> PlayerRocketHitTile;
    public static Action<PlayerScript, Hex> PlayerHasMoved;
    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action RoundEnded;
    public static Action<PlayerScript> NewPlayerTurn;

}