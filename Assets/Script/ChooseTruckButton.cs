using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTruckButton : MonoBehaviour
{
    [SerializeField] private int index;

    public void GetIndex()
    {
        InputManager.instance.SetTruckIndex(index);
    }

    public void SetButtonColor(Color color)
    {
        GetComponent<Image>().color = color;
    }

}
