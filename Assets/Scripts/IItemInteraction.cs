using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInteraction
{
    public void Interact(PlayerInventory inventory);
    public void EndInteract();
}
