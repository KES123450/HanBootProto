using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMilkTagButton : MonoBehaviour
{
    [SerializeField] private MilkTag milkTag;

    public void OnClick()
    {
        InputManager.instance.SetPathByTag(milkTag);
    }
}
