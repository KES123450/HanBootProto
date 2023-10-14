using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            Debug.Log("house");

            House h = collision.GetComponent<House>();
            if (!transform.parent.GetComponent<Truck>().CheckTargetHouse(h))
                return;

            h.Delivery();
        }
    }
}
