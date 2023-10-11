using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVertex : MonoBehaviour
{
    public Vector3 vertexPosition;
    public LinkedList<MapVertex> neighbors;
    public LinkedList<RoadTag> roadTags;
    public int overlappingPaths;

    public MapVertex(Vector3 pos)
    {
        vertexPosition = pos;
        neighbors = new();
        roadTags = new();
        overlappingPaths = 0;
    }
}
