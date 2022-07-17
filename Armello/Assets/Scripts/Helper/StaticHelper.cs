
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public static class StaticHelper
{
    private static System.Random rng = new System.Random();

    public static bool IsWideScreen => Screen.height * 1.2f < Screen.width;
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static float xOffset = 2, yOffset = 1, zOffset = 1.73f;
    public static Vector3Int ConvertPositionToOffset(this Vector3 position)
    {
        var x = Mathf.CeilToInt(position.x / xOffset);
        var y = Mathf.RoundToInt(position.y / yOffset);
        var z = Mathf.RoundToInt(position.z / zOffset);

        return new Vector3Int(x, y, z);
    }

    public static bool In<T>(this T val, params T[] values) where T : struct
    {
        return values.Contains(val);
    }

    public static PlayerScript GetPlayer(this int id)
    {
        return NetworkHelper.instance.GetPlayers().FirstOrDefault(p => p.PlayerId == id);
    }

    public static Hex GetHex(this Vector3Int coordinates)
    {
        return HexGrid.instance.GetTileAt(coordinates);
    }

    public static Hex GetHex(this Vector3 coordinates)
    {
        return HexGrid.instance.GetTileAt(new Vector3Int((int)coordinates.x, (int)coordinates.y, (int)coordinates.z));
    }

    public static int GetPunOwnerActorNr(this GameObject go)
    {
        var photonView = go.GetComponent<PhotonView>();
        if (photonView != null)
        {
            return photonView.OwnerActorNr;
        }
        return -1;
    }
}