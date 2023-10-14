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
        from.neighbors.Add(to);
        to.neighbors.Add(from);
        from.roadTags.Add(tag);
        to.roadTags.Add(tag);
        from.overlappingPaths.Add(0);
        to.overlappingPaths.Add(0);
    }

    public void AddPath(MapVertex from, MapVertex to)
    {
        int index = from.neighbors.FindIndex(x => ReferenceEquals(x, to));
        from.overlappingPaths[index]++;

        index = to.neighbors.FindIndex(x => ReferenceEquals(x, from));
        to.overlappingPaths[index]++;
    }

    public void AddPath(List<MapVertex> vertexes)
    {
        for(int i=0; i < vertexes.Count - 1; i++)
        {
            AddPath(vertexes[i], vertexes[i + 1]);
        }
    }

    public void DeletePath(MapVertex from, MapVertex to)
    {
        int index = from.neighbors.FindIndex(x => ReferenceEquals(x, to));
        from.overlappingPaths[index]--;

        index = to.neighbors.FindIndex(x => ReferenceEquals(x, from));
        to.overlappingPaths[index]--;
    }

    public void DeletePath(List<MapVertex> vertexes)
    {
        for (int i = 0; i < vertexes.Count - 1; i++)
        {
            DeletePath(vertexes[i], vertexes[i + 1]);
        }
    }

}
