using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class House : MonoBehaviour
{
    [SerializeField] private MilkTag milkTag;
    private bool isDelivery { get; set; }
    [SerializeField] private float deliveryTime;
    [SerializeField] private Transform selectedEffect;
    [SerializeField] private Transform maskBase;
    [SerializeField] private float maskStartPosY;
    [SerializeField] private float maskEndPosY;
    private float currentDeliveryTime;

    public MilkTag GetTag() => milkTag;


    private void Start()
    {
        currentDeliveryTime = deliveryTime;
    }
    private void CountdownDeliveryTime()
    {
        currentDeliveryTime -= Time.deltaTime;
        float y = Mathf.Lerp(maskStartPosY, maskEndPosY, currentDeliveryTime / deliveryTime);
        maskBase.localPosition = new Vector3(0, y, 0);
    }

    public void Delivery()
    {
        currentDeliveryTime = deliveryTime;

    }
    public void OnSelectedEffect()
    {
        selectedEffect.DOScale(8f, 0.15f);
    }

    public void OffSelectedEffect()
    {
        selectedEffect.DOScale(0f, 0.15f);
    }

    private void Update()
    {
        CountdownDeliveryTime();
    }
}
