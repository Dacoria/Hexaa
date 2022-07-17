using Photon.Pun;

public static class Netw
{
    public static PlayerScript MyPlayer() => NetworkHelper.instance.GetMyPlayer();
    public static PlayerScript CurrPlayer() => GameHandler.instance.CurrentPlayer;
    public static bool IsMyTurn => MyPlayer() == CurrPlayer();
}