using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapGraph map;


    private void Awake()
    {
        MapVertex v0 = new MapVertex(new Vector3(0f, 0f, 0));
        MapVertex v1 = new MapVertex(new Vector3(-2.5f, -2.3f, 0));
        MapVertex v2 = new MapVertex(new Vector3(-5.5f, 1.45f, 0));
        MapVertex v3 = new MapVertex(new Vector3(1.25f, 2.45f, 0));
        MapVertex v4 = new MapVertex(new Vector3(2.25f, -1.3f, 0));
        MapVertex v5 = new MapVertex(new Vector3(-3.5f, 4.2f, 0));
        MapVertex v6 = new MapVertex(new Vector3(-7.25f, -0.8f, 0));
        MapVertex v7 = new MapVertex(new Vector3(5.25f, 3.45f, 0));
        MapVertex v8 = new MapVertex(new Vector3(4f, -3.8f, 0));
        List<MapVertex> vertexList = new List<MapVertex> {v0, v1, v2, v3, v4, v5, v6, v7, v8 };
        map = new MapGraph(vertexList);
    }
}
