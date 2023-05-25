using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] players;
    public int activePlayer = 0;

    public GameObject activePlayerObj;

    public Vector2[] offsets;

    [HideInInspector]
    public Vector3 assignedOffsets;

    public Manager manager;

    void Start(){
        players[activePlayer].GetComponent<PlayerMovement>().isActivePlayer = true;
        activePlayerObj = players[activePlayer];
        ReassignOffsets(0);
    }

    void Update(){
        if(!activePlayerObj.GetComponent<PlayerMovement>().isAlive){
            FindNewActivePlayer();
        }

        #region[Switch Characters]
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SetActivePlayer(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            SetActivePlayer(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            SetActivePlayer(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4)){
            SetActivePlayer(3);
        }
        #endregion
    }

    void FindNewActivePlayer(){
        bool foundNewActive = false;
        for(int i = 0; i < players.Length; i++){
            if(players[i].GetComponent<PlayerMovement>().isAlive){
                foundNewActive = true;
                SetActivePlayer(i);
            }
        }

        if(!foundNewActive){
            //GAME OVER
            manager.GameOver();
        }
            
    }

    void SetActivePlayer(int playerIndex){
        if(players[playerIndex].GetComponent<PlayerMovement>().isAlive){
            players[activePlayer].GetComponent<PlayerMovement>().isActivePlayer = false;
            activePlayer = playerIndex;
            activePlayerObj = players[activePlayer];
            players[playerIndex].GetComponent<PlayerMovement>().isActivePlayer = true;
            ReassignOffsets(playerIndex);
        }
    }

    public GameObject GetClosestPlayer(Vector3 pos){
        float distance = 1000f;
        int closest = 0;
        for (int i = 0; i < players.Length; i++) {
            if(Vector3.Distance(players[i].transform.position, pos) < distance && 
            Vector3.Distance(players[i].transform.position, pos) > 0.1f &&
            players[i].GetComponent<PlayerMovement>().isAlive){
                distance = Vector3.Distance(players[i].transform.position, pos);
                closest = i;
            }
        }
        return players[closest];
    }

    void ReassignOffsets(int index){
        switch(index){
            case 0:
                assignedOffsets = new Vector3(1,2,3);
                break;
            case 1:
                assignedOffsets = new Vector3(0,2,3);
                break;
            case 2:
                assignedOffsets = new Vector3(0,1,3);
                break;
            case 3:
                assignedOffsets = new Vector3(0,1,2);
                break;
        }
    }
}
