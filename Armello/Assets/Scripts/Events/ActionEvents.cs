using System;
using System.Collections.Generic;

public static class ActionEvents
{
    public static Action<PlayerScript, Hex, PlayerScript, bool> PlayerRocketHitTile;
    public static Action<PlayerScript, Hex, AbilityType> PlayerAbility;
    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<bool> EndRound;
    public static Action EndGame;
    public static Action<PlayerScript> NewPlayerTurn;
}