using Photon.Pun;
using System.Linq;
public static class Netw
{
    public static PlayerScript MyPlayer() => NetworkHelper.instance.GetMyPlayer();
    public static PlayerScript CurrPlayer() => GameHandler.instance.CurrentPlayer;
    public static bool IsMyTurn() => MyPlayer() == CurrPlayer();
    public static bool IsMyTurn(this PlayerScript player) => player == CurrPlayer();
    public static bool IsOnMyNetwork(this PlayerScript player) => NetworkHelper.instance.GetMyPlayers(includeAi: true).Any(x => x == player);
}