using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGraph : MonoBehaviour
{
    public List<MapVertex> vertexes;

    public MapGraph(List<MapVertex> v)
    {
        vertexes = new List<MapVertex>(v);

    }

    public void AddVertex(Vector3 pos)
    {
        MapVertex v = new MapVertex(pos);
        vertexes.Add(v);
    }

    public void AddEdge(MapVertex from, MapVertex to, RoadTag tag)
    {
        from.neighbors.AddLast(to);
        to.neighbors.AddLast(from);
        from.roadTags.AddLast(tag);
        to.roadTags.AddLast(tag);
    }

    public void DeleteEdge()
    {

    }

}
