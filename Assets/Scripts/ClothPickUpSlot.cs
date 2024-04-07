using System.Collections;
using UnityEngine;

public class ClothPickUpSlot : MonoBehaviour, IItemInteraction
{
    public GameObject cloth; // Cloth stored here
    public float clothPickUpSpeed = 1f; // Cloth taking speed in sec
    
    private bool isTakingCloth; // check if player is in the pick up zone
    private PlayerInventory playerInventory; // Set the curent player inventory

    private float pickUpElapsedTime;

    public float PickupElapsedTime
    {
        get { return pickUpElapsedTime; }
    }

    public void Interact(PlayerInventory inventory)
    {
        isTakingCloth = true;
        playerInventory = inventory;
        playerInventory.GetComponent<PlayerUI>().SetPickupSlot(this);
        StartCoroutine(TakeCloths());
        Debug.Log("Started taking cloths");
    }

    public void EndInteract()
    {
        isTakingCloth = false;
        playerInventory.GetComponent<PlayerUI>().SetPickupSlot(null);
        Debug.Log("Taking cloths ended");
    }

    IEnumerator TakeCloths()
    {
        pickUpElapsedTime = 0f;
        
        while (isTakingCloth)
        {
            pickUpElapsedTime += Time.deltaTime;

            if (playerInventory.IsInventoryHasSpace)
            {
                if (pickUpElapsedTime >= clothPickUpSpeed)
                {
                    pickUpElapsedTime = 0f;

                    Cloth newCloth = Instantiate(cloth, transform.position, Quaternion.identity).GetComponent<Cloth>();
                    playerInventory.AddItem(newCloth);
                
                    Debug.Log("Cloth picked up");
                }
            }

            yield return null;
        }
    }
}
