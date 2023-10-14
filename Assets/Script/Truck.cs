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
    private List<House> targetHouses;
    int nowIndex = 0;


    public void InitTruck(List<MapVertex> paths, Color color,List<House> houses)
    {
        truck.position = Vector3.zero;
        nowIndex = 0;
        truckPath = new(paths);
        Debug.Log(houses.ToString());
        targetHouses = new(houses);
        path.startColor = color;
        path.endColor = color;
        RenderPath();
        color.a = 1f;
        truck.GetComponent<SpriteRenderer>().color = color;
    }

    public void DisableTruck()
    {
        foreach(House h in targetHouses)
        {
            h.OffSelectedEffect();
        }
        MapManager.instance.map.DeletePath(
            truckPath);
        
    }
    public bool CheckTargetHouse(House h)
    {
        foreach(House th in targetHouses)
        {
            if (ReferenceEquals(th, h))
            {
                return true;
            }
        }

        return false;
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
        if (nowIndex == 0)
        {
            transform.position = Vector3.zero;
        }

        int nextIndex = GetNextIndex(nowIndex);
        float distance = Vector2.Distance(truck.position, truckPath[nextIndex].vertexPosition);
        if (distance<=truckArea)
        {
            nowIndex = nextIndex;
        }
        float angle = GetAngle(truckPath[nowIndex].vertexPosition, 
            truckPath[GetNextIndex(nowIndex)].vertexPosition);
        truck.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float pathOverrapCount = truckPath[nowIndex]
            .GetOverrapPathCount(truckPath[GetNextIndex(nowIndex)]);
       
        truck.Translate(Vector3.up * truckSpeed/pathOverrapCount * Time.deltaTime);
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
