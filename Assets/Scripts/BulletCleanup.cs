using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCleanup : MonoBehaviour
{
    public float despawnTimer = 1;

    void Update()
    {
        if(despawnTimer < 0){
            Destroy(gameObject);
        }
        else{
            despawnTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag != "RobberBullet" && col.gameObject.tag != "PoliceBullet"){
            Destroy(gameObject);
        }
    }
}
