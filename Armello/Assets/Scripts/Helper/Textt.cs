public static class Textt
{
    public static void GameLocal(string text)
    {
        NetworkHelper.instance.SetGameText(text, false);
    }

    public static void ActionLocal(string text)
    {
        NetworkHelper.instance.SetActionText(text, false);
    }

    public static void GameSync(string text)
    {
        NetworkHelper.instance.SetGameText(text, true);
    }

    public static void ActionSync(string text)
    {
        NetworkHelper.instance.SetActionText(text, true);
    }
}