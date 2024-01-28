using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public static TruckManager instance;
    [SerializeField] private List<Truck> trucks = new();
    [SerializeField] private List<bool> isTruckDrive = new();
    [SerializeField] private int milkMaxCount;

    public int GetMilkCount() => milkMaxCount;
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
    public void DriveTruck(int index, List<MapVertex> paths,Color color,List<House> houses)
    {
        trucks[index].InitTruck(paths,color,houses);
        isTruckDrive[index] = true;
        trucks[index].gameObject.SetActive(true);
    }

    public void DeleteTruck(int index)
    {
        if (!CheckTruckDrive(index))
            return;

        isTruckDrive[index] = false;
        trucks[index].DisableTruck();
        trucks[index].gameObject.SetActive(false);
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
