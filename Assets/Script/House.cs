using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private MilkTag milkTag;
    private bool isDelivery { get; set; }
    [SerializeField] private int deliveryTime;
    private int currentDeliveryTime;

    public MilkTag GetTag() => milkTag;

    

    private void CountdownDeliveryTime()
    {

    }


}
