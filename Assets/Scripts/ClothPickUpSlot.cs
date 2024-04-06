using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothPickUpSlot : MonoBehaviour, IItemInteraction
{
    public GameObject cloth; // Cloth stored here
    public float clothPickUpSpeed = 2f; // Cloth taking speed in sec
    
    private bool isTakingCloth; // check if player is in the pick up zone
    private PlayerInventory playerInventory; // Set the curent player inventory

    public void Interact(PlayerInventory inventory)
    {
        isTakingCloth = true;
        playerInventory = inventory;
        StartCoroutine(TakeCloths());
        Debug.Log("Started taking cloths");
    }

    public void EndInteract()
    {
        isTakingCloth = false;
        Debug.Log("Taking cloths ended");
    }

    IEnumerator TakeCloths()
    {
        float timeElapsed = 0f;
        
        while (isTakingCloth)
        {
            timeElapsed += Time.deltaTime;
            
            if (timeElapsed >= clothPickUpSpeed)
            {
                timeElapsed = 0f;

                Cloth newCloth = Instantiate(cloth, transform.position, Quaternion.identity).GetComponent<Cloth>();
                playerInventory.AddItem(newCloth);
                
                Debug.Log("Cloth picked up");
            }

            yield return null;
        }
    }
}
