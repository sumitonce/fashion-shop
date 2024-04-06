using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothDeliveryPoint : MonoBehaviour, IItemInteraction
{
    public bool IsOccupied { get; set; }
    
    private CustomerNPC currentNPC;

    public static event Action<ClothDeliveryPoint> OnDeliveryPointFreed; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<CustomerNPC>();
            currentNPC.SetState(CustomerState.Waiting);
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

    public void Occupy()
    {
        IsOccupied = true;
    }
    
    public void FreeUp()
    {
        IsOccupied = false;
        OnDeliveryPointFreed?.Invoke(this);
    }
}
