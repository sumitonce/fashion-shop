using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothDeliveryPoint : MonoBehaviour, IItemInteraction
{
    public ClothIcon clothIcon;
    
    public bool IsOccupied { get; set; }
    private CustomerNPC currentNPC;

    public static event Action<ClothDeliveryPoint> OnDeliveryPointFreed;
    
    [SerializeField]
    private Vector3 clothIconOffset;

    private void Update()
    {
        if (currentNPC && IsOccupied && clothIcon.gameObject.activeInHierarchy)
        {
            clothIcon.transform.position = Camera.main.WorldToScreenPoint(currentNPC.transform.position + clothIconOffset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<CustomerNPC>();
            currentNPC.SetState(CustomerState.Waiting);
            SetClothIcon();
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        if (currentNPC.GetCurrentState() == CustomerState.Waiting)
        {
            currentNPC.SetState(CustomerState.Leaving);
            ClothType requiredCloth = currentNPC.GetRequiredClothType();
            Cloth cloth = inventory.GetSpecificCloth(requiredCloth);
            // Check if required cloth is in inventory
            if (cloth != null)
            {
                currentNPC.ChangeCloth(cloth.clothTexture);
                inventory.RemoveCloth(requiredCloth);
            
                FreeUp();
            }
            else
            {
                Debug.Log("Low inventory!");
            } 
        }
    }

    public void EndInteract()
    {
    }

    private void SetClothIcon()
    {
        clothIcon.gameObject.SetActive(true);
        clothIcon.SetClothIcon(currentNPC.GetRequiredClothType());
    }

    public void Occupy()
    {
        IsOccupied = true;
    }
    
    public void FreeUp()
    {
        IsOccupied = false;
        OnDeliveryPointFreed?.Invoke(this);
        clothIcon.gameObject.SetActive(false);
    }
}
