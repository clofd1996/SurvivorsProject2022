using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("Title"); // 点击Exit按钮可以回到主菜单
    }
}
