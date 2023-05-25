using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float timeBwnTargetChecks = 10;

    public bool rangedUnit = false;
    public float minDist = 1;

    [HideInInspector]
    public GameObject target;

    float targetCheckTime;

    void Start()
    {
        targetCheckTime = timeBwnTargetChecks;
        FindNearestRobber();
    }

    void Update()
    {
        //re-evaluates where the nearest robber is periodically
        if(targetCheckTime < 0){
            FindNearestRobber();
            targetCheckTime = timeBwnTargetChecks;
        } else{
            targetCheckTime -= Time.deltaTime;
        }

        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y,targetDir.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate(){        
        if(rangedUnit){
            if(Vector3.Distance(transform.position, target.transform.position) > minDist){
                
            }
        }
        else{
            rb.MovePosition(Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime));
        }
    }

    void FindNearestRobber(){
        //Gets the list of Players
        GameObject[] players = GameObject.FindWithTag("Player").GetComponent<PlayerController>().players;
        
        if(Random.value < 0.5f) {
            target = players[GameObject.FindWithTag("Player").GetComponent<PlayerController>().activePlayer];  
        }
        else {
            target = GameObject.FindWithTag("Player").GetComponent<PlayerController>().GetClosestPlayer(transform.position);
        }
    }
}
