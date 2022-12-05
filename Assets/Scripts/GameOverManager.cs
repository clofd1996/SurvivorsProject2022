using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    // Singleton化
    private static GameOverManager instance;

    public static GameOverManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public GameObject gameOverPanel;
    public GameObject ResumeButtom;
    public TMP_Text gameOverPanelText;
    public bool gameIsPaused = false;

    public void ShowDeathScreen()
    {
        //Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        ResumeButtom.SetActive(false);
        gameOverPanelText.text = "Game Over";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }


    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("Title"); // 点击Exit按钮可以回到主菜单
    }

    public void OnResumeButtonClick()
    {
        Resume();
    }
}
