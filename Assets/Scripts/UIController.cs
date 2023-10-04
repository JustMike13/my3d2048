using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    const int IN_GAME = 0;
    const int IN_MENU = 3;
    const int IN_SETTINGS = 4;
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField]
    Canvas MenuCanvas;
    [SerializeField]
    TextMeshProUGUI StartButtonText;
    [SerializeField]
    Canvas GameCanvas;
    [SerializeField]
    Canvas SettingsCanvas;
    [SerializeField]
    GameObject CameraObject;
    int score;
    int state;
    bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        state = 0;
        GameCanvas.enabled = false;
        gameStarted = false;
        //SettingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == IN_GAME || state == IN_SETTINGS)
            {
                SwitchState(IN_MENU);
            }
            else if (state == IN_MENU && gameStarted)
            {
                SwitchState(IN_GAME);
            }
        }
    }

    public void SwitchState(int s)
    {
        state = s;
        MenuCanvas.enabled = s == IN_MENU;
        GameCanvas.enabled = s == IN_GAME;
        //SettingsCanvas.enabled = s == IN_SETTINGS;

        CameraObject.GetComponent<CameraController>().SetCamera(s);

        if (s == IN_GAME)
        {
            StartButtonText.text = "Resume";
            gameStarted = true;
            GetComponent<GameController>().Resume();
        }
        else
        {
            GetComponent<GameController>().Pause();
        }
    }

    public void IncreaseScore(int s)
    {
        score += s;
        ScoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        score = 0;
        ScoreText.text = score.ToString();
        GetComponent<GameController>().Restart();
        SwitchState(IN_GAME);
    }

    public void StartButton_OnClick()
    {
        SwitchState(IN_GAME);
    }

    public void ExitGame()
    {
        Application.Quit();
        SceneManager.LoadScene("SampleScene");
    }
}
