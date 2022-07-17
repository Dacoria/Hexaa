
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GraphSearch
{
    public static BFSResult BFSGetRange(HexGrid hexGrid, Vector3Int startPoint, int movementPoints)
    {
        Dictionary<Vector3Int, Vector3Int?> visitedNotes = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costsSoFar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();

        nodesToVisitQueue.Enqueue(startPoint);
        costsSoFar.Add(startPoint, 0);
        visitedNotes.Add(startPoint, null);

        while(nodesToVisitQueue.Count > 0)
        {
            var currentNode = nodesToVisitQueue.Dequeue();
            foreach (var neighborPositon in hexGrid.GetNeighboursFor(currentNode))
            {
                var tile = hexGrid.GetTileAt(neighborPositon);
                if (tile.IsObstacle())
                {
                    continue;
                }

                var nodeCost = tile.GetCost();
                var currentCost = costsSoFar[currentNode];
                var newCost = currentCost = nodeCost;

                if(newCost <= movementPoints)
                {
                    if(!visitedNotes.ContainsKey(neighborPositon))
                    {
                        visitedNotes[neighborPositon] = currentNode;
                        costsSoFar[neighborPositon] = newCost;
                        nodesToVisitQueue.Enqueue(neighborPositon);
                    }
                    else if (costsSoFar[neighborPositon] > newCost)
                    {
                        costsSoFar[neighborPositon] = newCost;
                        visitedNotes[neighborPositon] = currentNode;
                    }
                }
            }
        }

        return new BFSResult { visitedNotesDict = visitedNotes };
    }

    public static List<Vector3Int> GeneratePathBFS(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> visitedNotesDict)
    {
        var path = new List<Vector3Int>
        {
            current
        };
        while (visitedNotesDict[current] != null)
        {
            path.Add(visitedNotesDict[current].Value);
            current = visitedNotesDict[current].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList();
    }
}


public struct BFSResult
{
    public Dictionary<Vector3Int, Vector3Int?> visitedNotesDict;

    public List<Vector3Int> GetPathTo(Vector3Int destination)
    {
        if (visitedNotesDict.ContainsKey(destination))
        {
            return GraphSearch.GeneratePathBFS(destination, visitedNotesDict);
        }
        else
        {
            return new List<Vector3Int>();
        }
    }

    public bool IsHexPositionInRange(Vector3Int position) => visitedNotesDict.ContainsKey(position);

    public List<Vector3Int> GetRangePositions() => visitedNotesDict.Keys.ToList();
}