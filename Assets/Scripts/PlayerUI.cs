using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerUI : MonoBehaviour
{
    public GameObject pickUpLoaderParent;
    public Image pickUpLoader;
    public Vector3 loaderIconOffset;
    public float iconMoveSpeed = 2f;

    public ClothPickUpSlot pickUpSlot { get; set; }

    private void Update()
    {
        if (pickUpSlot)
        {
            UpdatePickUpLoader(pickUpSlot.PickupElapsedTime);
            Vector3 iconPosition = Vector3.Lerp(pickUpLoaderParent.transform.position,
                Camera.main.WorldToScreenPoint(transform.position + loaderIconOffset), iconMoveSpeed);
            pickUpLoaderParent.transform.position = iconPosition;
        }
    }

    public void UpdatePickUpLoader(float pickUpTime)
    {
        Debug.Log(pickUpTime);
        pickUpLoader.fillAmount = pickUpTime;
    }

    public void SetPickupSlot(ClothPickUpSlot pickUpSlot)
    {
        if(pickUpSlot)
            pickUpLoaderParent.SetActive(true);
        else
            pickUpLoaderParent.SetActive(false);

        this.pickUpSlot = pickUpSlot;
        pickUpLoader.fillAmount = 0f;
    }
}
