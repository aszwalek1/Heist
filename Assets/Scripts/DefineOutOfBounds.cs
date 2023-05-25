using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineOutOfBounds : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10,11);
        Physics2D.IgnoreLayerCollision(7,11);
        Physics2D.IgnoreLayerCollision(8,11);
    }
}
