using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothIcon : MonoBehaviour
{
    [Tooltip("Index based on ClothType enum")]
    public Sprite[] icons;
    public Image icon;

    public void SetClothIcon(ClothType clothType)
    {
        icon.sprite = icons[(int)clothType];
    }
}
