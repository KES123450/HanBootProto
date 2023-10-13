using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public static TruckManager instance;
    [SerializeField] private List<Truck> trucks = new();
    [SerializeField] private List<bool> isTruckDrive = new();
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
    }
    public void DriveTruck(int index, List<MapVertex> paths,Color color)
    {
        trucks[index].InitTruck(paths,color);
        isTruckDrive[index] = true;
        trucks[index].gameObject.SetActive(true);
    }
    public void AddTruck(Truck t)
    {
        trucks.Add(t);
        isTruckDrive.Add(false);
    }

    public bool CheckTruckDrive(int index)
    {
        if (isTruckDrive[index])
            return true;

        return false;
    }
}
