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
        SceneManager.LoadScene("Title"); // ���Exit��ť���Իص����˵�
    }

    public void OnResumeButtonClick()
    {
        gameOverPanel.SetActive(false); // ���Resume��ť���Իص���Ϸ
    }
}
