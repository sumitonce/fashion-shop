using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Cloth> cloths;// { get; set; }
    public Transform itemHoldingSlot;
    
    [SerializeField]
    private int maxItems = 10;
    private float clothZOffset = 0.07f; // cloth z multiplier to stack cloths

    public bool IsInventoryHasSpace
    {
        get
        {
            if (cloths.Count < maxItems)
                return true;
            else
                return false;
        }
    }

    private void Awake()
    {
        cloths = new List<Cloth>();
    }

    public void AddItem(Cloth item)
    {
        cloths.Add(item);
        item.transform.SetParent(itemHoldingSlot);
        Vector3 clothPosition = itemHoldingSlot.position;
        clothPosition.y += cloths.Count * clothZOffset;
        item.transform.position = clothPosition;
    }

    public Cloth GetSpecificCloth(ClothType clothType)
    {
        return cloths.FirstOrDefault(cloth => cloth.clothType == clothType);
    }

    public void RemoveCloth(ClothType clothType)
    {
        Debug.Log("Cloth removed from inventory");
        Cloth clothToRemove = cloths.FirstOrDefault(cloth => cloth.clothType == clothType);
        Destroy(clothToRemove.gameObject);

        int clothIndex = cloths.IndexOf(clothToRemove);
        cloths.Remove(clothToRemove);

        for (int i = clothIndex; i < cloths.Count; i++)
        {
            Debug.Log("Change Y position " + cloths[i].gameObject.name);
            Vector3 clothPosition = cloths[i].transform.position;
            clothPosition.y -= clothZOffset;
            cloths[i].transform.position = clothPosition;
        }
    }
}
