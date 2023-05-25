using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FireWeapon : MonoBehaviour
{
    public bool isPlayable = false;

    public GameObject bullet;

    public float reloadTime = 1;

    public string targetTag;

    public AudioSource gunShot;

    float shootCooldown;
    // Start is called before the first frame update
    void Start()
    {
        shootCooldown = reloadTime;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
        Physics2D.IgnoreLayerCollision(6,7);
        Physics2D.IgnoreLayerCollision(6,8);
        Physics2D.IgnoreLayerCollision(7,8);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayable){
            if(GetComponent<PlayerMovement>().isActivePlayer){
                if(Input.GetMouseButton(0) && shootCooldown < 0){
                        Shoot(Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position));
                        shootCooldown = reloadTime;
                }
                else{
                    shootCooldown -= Time.deltaTime;
                }
            }
            else{
                if(shootCooldown < 0 && GameObject.FindGameObjectsWithTag(targetTag).Length > 0){
                    Shoot(Vector3.Normalize(GetComponent<PlayerMovement>().GetClosestEnemy(targetTag) - transform.position));
                    shootCooldown = reloadTime + Random.Range(-0.5f, 0.5f);
                }
                else{
                    shootCooldown -= Time.deltaTime;
                }
                
            }
        }
        else{
            if(shootCooldown < 0 && GameObject.FindGameObjectsWithTag(targetTag).Length > 0){
                Shoot(Vector3.Normalize(GetComponent<EnemyMovement>().target.transform.position - transform.position));
                shootCooldown = reloadTime + Random.Range(-0.5f, 0.5f);
            }
            else{
                shootCooldown -= Time.deltaTime;
            }
        }
    }

    void Shoot(Vector3 shotDir){
        GameObject shot = Instantiate(bullet, transform.position + shotDir, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(shotDir, ForceMode2D.Impulse);
        gunShot.Play();
    }
}
