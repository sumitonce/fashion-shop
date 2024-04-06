using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothType
{
    Red,
    Blue
}

public class Cloth : MonoBehaviour
{
    public ClothType clothType;
    public Texture2D clothTexture;
}
