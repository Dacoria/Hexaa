using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();

    // cache
    private Dictionary<Vector3Int, List<Vector3Int>> hexTileNeightboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    public static HexGrid instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        var hexes = FindObjectsOfType<Hex>();
        var hexesSorted = hexes.OrderBy(x => Vector3.Distance(x.transform.position, new Vector3(0,0,0))).ToList();

        foreach (var hex in hexesSorted)
        {
            hexTileDict[hex.HexCoordinates] = hex;
            var lerp = hex.gameObject.AddComponent<LerpMovement>();
            StartCoroutine(lerp.MoveToDestination(hex.transform.position + new Vector3(0, -100, 0), hex.transform.position, duration: 1.5f, delayedStart: hex.transform.position.x * 0.15f));
        }        
    }

    private bool HexGridLoaded;

    private void Update()
    {
        if(!HexGridLoaded)
        {
            HexGridLoaded = hexTileDict.Values.All(x => Vector3.Distance(x.OrigPosition, x.transform.position) < 0.01f);
            if(HexGridLoaded)
            {
                ActionEvents.GridLoaded?.Invoke();
            }
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
        var neighboursRange1 = GetNeighboursFor(hexCoordinates);     

        var totalProcessedUniqueList = neighboursRange1;
        var previousRankUniqueList = neighboursRange1;

        for (int currentRank = 2; currentRank <= range && currentRank <= 10; currentRank++)
        {
            var newUniqueList = GetUniqueNeighboursNotVisited(hexCoordinates, totalProcessedUniqueList, previousRankUniqueList);
            totalProcessedUniqueList = totalProcessedUniqueList.Concat(newUniqueList).ToList();
            previousRankUniqueList = newUniqueList.ToList();
        }

        return totalProcessedUniqueList;
    }

    private HashSet<Vector3Int> GetUniqueNeighboursNotVisited(Vector3Int startHexToExclude, List<Vector3Int> previouslyVisited, List<Vector3Int> previousRankUniqueList)
    {
        var newUniqueList = new HashSet<Vector3Int>();

        foreach (var neightbourRange in previousRankUniqueList)
        {
            var neighboursOfPreviousRank = GetNeighboursFor(neightbourRange);
            foreach (var neighbourOfNeighbor in neighboursOfPreviousRank)
            {
                if (!previouslyVisited.Any(x => x == neighbourOfNeighbor) && neighbourOfNeighbor != startHexToExclude)
                {
                    newUniqueList.Add(neighbourOfNeighbor);
                }
            }
        }

        return newUniqueList;
    }
    
    public bool IsLoaded()
    {
        return GetAllTiles().Count > 50;
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