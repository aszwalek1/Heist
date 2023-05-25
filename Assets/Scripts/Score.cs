using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    static float score = 0;

    public static void AddScore(int add){
        score += add;
    }

    public int GetScore(){
        return Mathf.RoundToInt(score);
    }


}
