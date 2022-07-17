using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();

    // cache
    private Dictionary<Vector3Int, List<Vector3Int>> hexTileNeightboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach(var hex in FindObjectsOfType<Hex>())
        {
            hexTileDict[hex.HexCoordinates] = hex;
        }
    }

    public List<Hex> GetAllTiles()
    {
        return hexTileDict.Values.ToList();
    }

    public Hex GetTileAt(Vector3Int hexCoordinates)
    {
        Hex result = null;
        hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }


    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates, int range)
    {
        var range1 = GetNeighboursFor(hexCoordinates);
        if(range <= 1)
        {
            return range1;
        }

        var secondUniqueList = new HashSet<Vector3Int>();
        foreach (var neightbourRange in range1)
        {
            var neighboursOfNeighbor = GetNeighboursFor(neightbourRange, 1);
            foreach(var neighbourOfNeighbor in neighboursOfNeighbor)
            {
                if (!range1.Any(x => x == neighbourOfNeighbor) && neighbourOfNeighbor != hexCoordinates)
                {
                    secondUniqueList.Add(neighbourOfNeighbor);
                }
            }
        }

        if (range <= 2)
        {
            return range1.Concat(secondUniqueList).ToList();
        }

        var thirdUniqueList = new HashSet<Vector3Int>();
        foreach (var neightbourRange in secondUniqueList)
        {
            var neighboursOfNeighbor2 = GetNeighboursFor(neightbourRange, 1);
            foreach (var neighbourOfNeighbor in neighboursOfNeighbor2)
            {
                if (!secondUniqueList.Any(x => x == neighbourOfNeighbor) && !range1.Any(x => x == neighbourOfNeighbor) && neighbourOfNeighbor != hexCoordinates)
                {
                    thirdUniqueList.Add(neighbourOfNeighbor);
                }
            }
        }

        return range1.Concat(secondUniqueList).Concat(thirdUniqueList).ToList();
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if(!hexTileDict.ContainsKey(hexCoordinates))
        {
            return new List<Vector3Int>();
        }
        if(hexTileNeightboursDict.ContainsKey(hexCoordinates))
        {
            return hexTileNeightboursDict[hexCoordinates];
        }

        hexTileNeightboursDict.Add(hexCoordinates, new List<Vector3Int>());
        foreach(var direction in Direction.GetDirectionsList(hexCoordinates.z))
        {
            if(hexTileDict.ContainsKey(hexCoordinates + direction))
            {
                hexTileNeightboursDict[hexCoordinates].Add(hexCoordinates + direction);
            }
        }

        return hexTileNeightboursDict[hexCoordinates];
    }
}