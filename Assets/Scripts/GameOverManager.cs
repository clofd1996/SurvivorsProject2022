using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowDeathScreen()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
    }


    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("Title"); // 点击Exit按钮可以回到主菜单
    }

    public void OnResumeButtonClick()
    {
        gameOverPanel.SetActive(false); // 点击Resume按钮可以回到游戏
    }
}
