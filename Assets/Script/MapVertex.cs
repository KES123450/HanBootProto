using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVertex : MonoBehaviour
{
    public Vector3 vertexPosition;
    public List<MapVertex> neighbors;
    public List<RoadTag> roadTags;
    public List<int> overlappingPaths;

    public override string ToString()
    {
        return vertexPosition.ToString();
    }
    public MapVertex(Vector3 pos)
    {
        vertexPosition = pos;
        neighbors = new();
        roadTags = new();
        overlappingPaths = new();
    }

    public int GetOverrapPathCount(MapVertex to)
    {
        int index = neighbors.FindIndex(x => ReferenceEquals(x, to));
        return overlappingPaths[index];
    }
}
