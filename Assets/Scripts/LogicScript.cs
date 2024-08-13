using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    private float playerOffset;
    private float currentPlayerAltitude;
    private float highestAltitude = 0f;
    public Text highscoreText;

    private const string HighScoreKey = "Highscore";

    [SerializeField] private Text scoreText;



    private void Start() {
        if (Player.Instance) {
            playerOffset = Player.Instance.transform.position.y; //because the player is below 0 when the game starts
            currentPlayerAltitude = Player.Instance.transform.position.y - playerOffset;
            InitializeHighScore();
        } else {
            Debug.Log("Player is not yet instantiated");
        }
        
    }
    private void Update() {

        currentPlayerAltitude = Player.Instance.transform.position.y - playerOffset;
        
        if (currentPlayerAltitude > highestAltitude) {
            highestAltitude = Mathf.Floor(currentPlayerAltitude);
            scoreText.text = highestAltitude.ToString();
        }

    }

    private void InitializeHighScore() {
        if (!PlayerPrefs.HasKey(HighScoreKey)) {
            PlayerPrefs.SetInt(HighScoreKey, 0);
        }
    }
    private int GetHighScore() {
        return PlayerPrefs.GetInt(HighScoreKey);
    }

    private void SetHighScore(int score) {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.Save();  
    }

    //trigger this on player death!!!!
    public void UpdateHighScore() {
        if (highestAltitude > GetHighScore()) {
            SetHighScore(Mathf.FloorToInt(highestAltitude));
        }
            highscoreText.text = GetHighScore().ToString();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        Debug.Log("Gameover from logic script");
    }



}
