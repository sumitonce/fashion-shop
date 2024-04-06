using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : MonoBehaviour, IItemInteraction
{
    public void Interact(PlayerInventory inventory)
    {
        Debug.Log("NPC Got the cloth");
    }

    public void EndInteract()
    {
        
    }
}
