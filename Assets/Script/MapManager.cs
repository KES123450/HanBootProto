using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public MapGraph map;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        MapVertex v0 = new MapVertex(new Vector3(0f, 0f, 0));
        MapVertex v1 = new MapVertex(new Vector3(-2.5f, -2.3f, 0));
        MapVertex v2 = new MapVertex(new Vector3(-5.5f, 2.95f, 0));
        MapVertex v3 = new MapVertex(new Vector3(1.25f, 2.45f, 0));
        MapVertex v4 = new MapVertex(new Vector3(2.25f, -1.3f, 0));
        MapVertex v5 = new MapVertex(new Vector3(-3.5f, 4.2f, 0));
        MapVertex v6 = new MapVertex(new Vector3(-7.25f, -0.8f, 0));
        MapVertex v7 = new MapVertex(new Vector3(5.25f, 3.45f, 0));
        MapVertex v8 = new MapVertex(new Vector3(4f, -3.8f, 0));
        MapVertex v9 = new MapVertex(new Vector3(-5.87f, -3.06f, 0));
        MapVertex v10 = new MapVertex(new Vector3(-3.44f, 1.18f, 0));
        List<MapVertex> vertexList = new List<MapVertex> {v0, v1, v2, v3, v4, v5, v6, v7, v8,v9,v10 };
        map = new MapGraph(vertexList);

        map.AddEdge(map.vertexes[0], map.vertexes[5], RoadTag.Lane);
        map.AddEdge(map.vertexes[5], map.vertexes[2], RoadTag.Lane);
        map.AddEdge(map.vertexes[2], map.vertexes[6], RoadTag.Lane);
        map.AddEdge(map.vertexes[6], map.vertexes[0], RoadTag.Lane);
        map.AddEdge(map.vertexes[6], map.vertexes[1], RoadTag.Lane);

        map.AddEdge(map.vertexes[1], map.vertexes[0], RoadTag.Lane);
        map.AddEdge(map.vertexes[5], map.vertexes[7], RoadTag.Lane);
        map.AddEdge(map.vertexes[1], map.vertexes[4], RoadTag.Lane);
        map.AddEdge(map.vertexes[4], map.vertexes[7], RoadTag.Lane);
        map.AddEdge(map.vertexes[7], map.vertexes[8], RoadTag.Lane);

        map.AddEdge(map.vertexes[1], map.vertexes[8], RoadTag.Lane);
        map.AddEdge(map.vertexes[5], map.vertexes[3], RoadTag.Lane);
        map.AddEdge(map.vertexes[3], map.vertexes[7], RoadTag.Lane);
        map.AddEdge(map.vertexes[3], map.vertexes[4], RoadTag.Lane);
        map.AddEdge(map.vertexes[0], map.vertexes[3], RoadTag.Lane);

        map.AddEdge(map.vertexes[10], map.vertexes[5], RoadTag.Lane);
        map.AddEdge(map.vertexes[10], map.vertexes[6], RoadTag.Lane);
        map.AddEdge(map.vertexes[0], map.vertexes[10], RoadTag.Lane);
        map.AddEdge(map.vertexes[4], map.vertexes[8], RoadTag.Lane);
        map.AddEdge(map.vertexes[6], map.vertexes[9], RoadTag.Lane);

        map.AddEdge(map.vertexes[8], map.vertexes[9], RoadTag.Lane);
        map.AddEdge(map.vertexes[0], map.vertexes[4], RoadTag.Lane);
    }
}
