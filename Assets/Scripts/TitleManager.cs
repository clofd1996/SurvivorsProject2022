using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //3÷÷methods

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnUpgradeButtonClick()
    {
        Debug.Log("have no time to do yet :<");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
