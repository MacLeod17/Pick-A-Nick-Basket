using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int HighScore { get; set; } = 0;

    public Text scoreUI;
    public Text highScoreUI;

    public GameObject menuScreen;
    public GameObject instructionsScreen;
    public GameObject creditsScreen;
    public GameObject gameOverScreen;

    public GameObject ball;

    static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }

    public enum eState
    {
        Menu,
        StartSession,
        Session,
        EndSession,
        GameOver
    }

    public eState State { get; set; } = eState.Menu;
    //public eState State { get; set; } = eState.StartSession;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreUI != null) highScoreUI.text = $"Hi Score: {string.Format("{0:D5}", HighScore)}";
    }

    private void Update()
    {
        switch (State)
        {
            case eState.Menu:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case eState.StartSession:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Score = 0;
                State = eState.Session;
                break;
            case eState.Session:
                CheckLose();
                break;
            case eState.EndSession:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                State = eState.GameOver;
                break;
            case eState.GameOver:
                if (gameOverScreen != null) gameOverScreen.SetActive(true);
                break;
            default:
                break;
        }        
    }

    public void OnStart()
    {
        if (menuScreen != null) menuScreen.SetActive(false);
        State = eState.StartSession;
    }

    public void OnInstructions()
    {
        if (instructionsScreen != null) instructionsScreen.SetActive(true);
        if (menuScreen != null) menuScreen.SetActive(false);
    }

    public void OnCredits()
    {
        if (creditsScreen != null) creditsScreen.SetActive(true);
        if (menuScreen != null) menuScreen.SetActive(false);
    }

    public void OnMenu()
    {
        if (instructionsScreen != null) instructionsScreen.SetActive(false);
        if (creditsScreen != null) creditsScreen.SetActive(false);

        if (menuScreen != null) menuScreen.SetActive(true);
    }

    public void OnRestart()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExit()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        Application.Quit();
    }

    public void AddPoints(int points)
    {
        Score += points;
        if (scoreUI != null) scoreUI.text = $"Score: {string.Format("{0:D5}", Score)}";

        SetHighScore();
    }

    private void SetHighScore()
    {
        if (Score > HighScore) HighScore = Score;
        if (highScoreUI != null) highScoreUI.text = $"Hi Score: {string.Format("{0:D5}", HighScore)}";
    }

    private void CheckLose()
    {
        if (Mathf.Abs(ball.transform.position.x) > 9.5f || Mathf.Abs(ball.transform.position.y) > 5.5f)
        {
            State = eState.EndSession;
        }
    }
}
