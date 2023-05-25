using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject GameOverMenu;
    public GameObject StopMenu;
    public GameObject OptionsMenu;
    public TMP_Text ScoreText;
    public TMP_Text HiScoreText;

    public bool resetHiScore = false;

    void Start(){
        Time.timeScale = 1;
        AudioListener.volume = 1;
        if(!PlayerPrefs.HasKey("HighScore") || resetHiScore){
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }
    
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            StopMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume() {
        Time.timeScale = 1;
        StopMenu.SetActive(false);
    }

    public void Exit() {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver(){
        Time.timeScale = 0;
        AudioListener.volume = 0;
        GameOverMenu.SetActive(true);
        int localScore = GetComponent<Score>().GetScore();

        if(PlayerPrefs.GetInt("HighScore") < localScore){
            PlayerPrefs.SetInt("HighScore", localScore);
        }

        ScoreText.text = "Your Score: " + localScore.ToString();
        HiScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void Restart() {
        SceneManager.LoadScene("Game");
    }
}
