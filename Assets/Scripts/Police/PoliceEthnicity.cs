using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEthnicity : MonoBehaviour
{
    public SpriteRenderer arms;
    public Gradient skinTones;
    // Start is called before the first frame update
    void Start()
    {
        arms.color = skinTones.Evaluate(Random.value);
    }
}
