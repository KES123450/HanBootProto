using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private List<House> houses = new();
    [SerializeField] private int spawnInterval;
    private float timer;
    private int index;
    private void SpawnHouseByTime()
    {
        timer += Time.deltaTime;
        if (index >= houses.Count)
            return;

        if (index == 0)
        {
            if (timer >= 3f)
            {
                houses[index].SpawnHouse();
                timer = 0;
                index++;
            }
        }

        if (timer >= spawnInterval)
        {
            houses[index].SpawnHouse();
            timer = 0;
            index++;
        }
    }

    private void Update()
    {
        SpawnHouseByTime();
    }
}
