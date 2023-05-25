using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isActivePlayer = false;

    public Rigidbody2D rb;
    public int playerNum;

    public float moveSpeed = 5;

    public bool isAlive;

    PlayerController pc;
    Vector2 moveDir;


    void Start(){
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        isAlive = true;
        //Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<FireWeapon>().bullet.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            if(isActivePlayer){

                //updates the move vector
                moveDir.x = Input.GetAxisRaw("Horizontal");
                moveDir.y = Input.GetAxisRaw("Vertical");

                //Looks at the mouse
                Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else{
                //get direction to closest cop
                Vector3 targetDir = GetClosestEnemy("Police") - transform.position;
                float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        
    }

    void FixedUpdate(){
        rb.inertia = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        if(isAlive){
            if(isActivePlayer){
                if(Input.GetKey(KeyCode.LeftShift)){
                    rb.MovePosition(rb.position + moveDir * moveSpeed * 2 * Time.fixedDeltaTime);
                }
                else{
                    rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
                }
                
            }
            else{
                Vector2 dir = CalculateTargetPosition(); //+ GetRandomOffset();
                rb.MovePosition(Vector2.MoveTowards(rb.position, dir, moveSpeed * Time.fixedDeltaTime));
            }
        }
    }

    Vector2 CalculateTargetPosition(){
        Vector2 target = pc.activePlayerObj.transform.position;
        for (int i = 0; i < 3; i++)
        {
            if(pc.assignedOffsets[i] == playerNum){
                target += pc.offsets[i];
            }
        }
        return target;
    }

    public Vector3 GetClosestEnemy(string tag){
        GameObject[] police = GameObject.FindGameObjectsWithTag(tag);
        float distance = 1000f;
        Vector3 closest = Vector3.zero;
        foreach (GameObject obj in police)
        {
            if(Vector3.Distance(transform.position, obj.transform.position) < distance){
                distance = Vector3.Distance(transform.position, obj.transform.position);
                closest = obj.transform.position;
            }
        }
        return closest;
    }
}
