using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject SettingPanel;
    [SerializeField] GameObject PlayerPanel;
    public bool OnSelected; // øÿ÷∆post-processing on/offµƒbool
    [SerializeField] TMP_Text Button;



    public static SaveData saveData;
    string SavePath => Path.Combine(Application.persistentDataPath, "save.data");

    private void Start()
    {
        if (Bool.isButtomOn == true)
        {
            Button.text = "ON";
        }
        else
        {
            Button.text = "OFF";
        }
    }

    private void Awake()
    {
        if (saveData == null)  // ¥Êµµ
            Load();
        else
            Save();

        Debug.Log($"goldCoins:{saveData.coinsGetten}, deathCounter:{saveData.deathCount}");
    }

    private void Update()
    {
        if (Bool.isButtomOn == true)
        {
            Button.text = "ON";
        }
        else
        {
            Button.text = "OFF";
        }
    }

    void Load()
    {
        FileStream file = null;
        try
        {
            file = File.Open(SavePath, FileMode.Open);
            var bf = new BinaryFormatter();
            saveData = bf.Deserialize(file) as SaveData;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            saveData = new SaveData();
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
                
        }

    }

    void Save()
    {
        FileStream file = null;
        try
        {
            if (!Directory.Exists(Application.persistentDataPath))
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }
            file = File.Create(SavePath);
            var bf = new BinaryFormatter();
            bf.Serialize(file, saveData);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
    //3÷÷methods

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
    }

    public void OnUpgradeButtonClick()
    {
        PlayerPanel.SetActive(true);
    }

    public void OnSettingButtonClick()
    {
        SettingPanel.SetActive(true);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void Switch()
    {
        Bool.isButtomOn = !Bool.isButtomOn;
    }

    public void OnExitButtonClick()
    {
        SettingPanel.SetActive(false);
    }

    public void OnExitPlayerButtonClick()
    {
        PlayerPanel.SetActive(false);
    }




    public void SelectPlayer1()
    {
        Bool.index = 0;
        Debug.Log("show" + Bool.index);
    }

    public void SelectPlayer2()
    {
        Bool.index = 1;
        Debug.Log("show" + Bool.index);
    }


}
