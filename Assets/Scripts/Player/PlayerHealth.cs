using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 50;

    public Image heart;
    public Image deadHeart;

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "PoliceBullet"){
            health -= 10;
        }
        else if (col.gameObject.tag == "Police"){
            health -= 50;
        }

        if(health == 0){
            GetComponent<PlayerMovement>().isAlive = false;
            GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);

            heart.enabled = false;
            deadHeart.enabled = true;
        }
    }
}
