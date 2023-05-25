using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceHealth : MonoBehaviour
{
    public float health = 10;

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "RobberBullet"){
            health -= 10;

            if(health == 0){
                Score.AddScore(10);
                Destroy(gameObject);
            }
        }
    }
}
