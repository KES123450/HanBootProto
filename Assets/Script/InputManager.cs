using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Transform corporation;
    [SerializeField] private LineRenderer path;
    [SerializeField] private float area;
    private List<MapVertex> pathVertexes = new();
    [SerializeField] private GameObject truckPrefab;

    private void DrawRoad()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float corporationSpace = Vector3.Distance(mousePosition, corporation.position);
            if (corporationSpace-10 >= area)
            {
                Debug.Log(corporationSpace);
                return;
            }
                

            pathVertexes.Add(new MapVertex(corporation.position));
        }

        if (Input.GetMouseButton(0))
        {
            if (pathVertexes.Count == 0)
                return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            RenderPathByMouse(mousePosition, pathVertexes.Count + 1);

            foreach(MapVertex v in mapManager.map.vertexes)
            {
                /*if (CheckDuplicateVertex(v))
                    break;*/

                float distance = Vector3.Distance(mousePosition, v.vertexPosition);
                if (distance<=area)
                {
                    
                    pathVertexes.Add(v);
                    RenderPath();
                }
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            if(pathVertexes[pathVertexes.Count-1].vertexPosition != corporation.position
                &&pathVertexes.Count<=1)
            {
                path.positionCount = 0;
                pathVertexes.Clear();
                return;
            }

            Instantiate(truckPrefab, corporation.position, Quaternion.identity);
            pathVertexes.Clear();

        }

    }

    private bool CheckDuplicateVertex(MapVertex v)
    {
        foreach(MapVertex mv in pathVertexes)
        {
            if (mv == v)
            {
                Debug.Log("true");
                return true;
            }
        }
        return false;
    }
    private void RenderPathByMouse(Vector3 mousePos, int pathCount)
    {
        path.positionCount = pathCount;
        path.SetPosition(pathCount-1, mousePos);

    }
    private void RenderPath()
    {
        path.positionCount = pathVertexes.Count;
        for (int i = 0; i < pathVertexes.Count; i++)
        {
            path.SetPosition(i, pathVertexes[i].vertexPosition);
        }
    }

    private void Update()
    {
        DrawRoad();
    }
}
