using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeletePathButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int index;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("right : " + index);
            TruckManager.instance.DeleteTruck(index);
            GetComponent<Image>().color = Color.white;
        }
    }
}
