using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public List<MapVertex> truckPath;
    [SerializeField] private Transform truck;
    [SerializeField] private float truckSpeed;
    [SerializeField] private LineRenderer path;
    [SerializeField] private float truckArea;
    int nowIndex = 0;


    public void InitTruck(List<MapVertex> paths, Color color)
    {
        transform.position = Vector3.zero;
        truckPath = new(paths);
        path.startColor = color;
        path.endColor = color;
        RenderPath();
    }
    
    private void Start()
    {
    /*    truckPath = new();
        MapVertex v1 = new MapVertex(new Vector3(0, 0, 0));
        MapVertex v2 = new MapVertex(new Vector3(1, 0, 0));
        MapVertex v3 = new MapVertex(new Vector3(6, 2, 0));
        MapVertex v4 = new MapVertex(new Vector3(2, 3, 0));
        MapVertex v5 = new MapVertex(new Vector3(0, 0, 0));
        truckPath.Add(v1);
        truckPath.Add(v2);
        truckPath.Add(v3);
        truckPath.Add(v4);
        truckPath.Add(v5);
        RenderPath();*/
    }
    private float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg-90f;
    }

    private int GetNextIndex(int nowIndex)
    {
        if (nowIndex >= truckPath.Count-1)
        {
            return 1;
        }

        return nowIndex+1;
    }

    private void DriveToPath()
    {
        int nextIndex = GetNextIndex(nowIndex);
        float distance = Vector2.Distance(truck.position, truckPath[nextIndex].vertexPosition);
        if (distance<=truckArea)
        {
            nowIndex = nextIndex;
        }
        float angle = GetAngle(truckPath[nowIndex].vertexPosition, 
            truckPath[GetNextIndex(nowIndex)].vertexPosition);
        truck.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        truck.Translate(Vector3.up * truckSpeed * Time.deltaTime);
    }

    private void RenderPath()
    {
        path.positionCount = truckPath.Count;
        for(int i = 0; i < truckPath.Count; i++)
        {
            path.SetPosition(i, truckPath[i].vertexPosition);
        }
    }

    private void Update()
    {
        DriveToPath();
    }
}
