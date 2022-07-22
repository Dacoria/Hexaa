using Photon.Pun;
using System.Linq;
public static class Netw
{
    public static PlayerScript MyPlayer() => NetworkHelper.instance.GetMyPlayer();
    public static PlayerScript CurrPlayer() => GameHandler.instance.CurrentPlayer();
    public static bool IsMyNetwTurn() => CurrPlayer().IsOnMyNetwork();
    public static bool IsMyTurn(this PlayerScript player) => CurrPlayer() == player;
    public static bool IsOnMyNetwork(this PlayerScript player) => NetworkHelper.instance.GetMyPlayers(includeAi: true).Any(x => x == player);
}