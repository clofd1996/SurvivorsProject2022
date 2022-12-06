using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject green;
    [SerializeField] GameObject red;
    [SerializeField] GameObject giant;
    [SerializeField] GameObject player;

    public SimpleObjectPool exppool;
    public SimpleObjectPool coinpool;

    [SerializeField] TMP_Text BlueNumber;
    [SerializeField] TMP_Text GreenNumber;
    [SerializeField] TMP_Text RedNumber;
    [SerializeField] TMP_Text GiantNumber;

    private int bluenumber = 0;
    private int greennumber = 0;
    private int rednumber = 0;
    private int giantnumber = 0;

    // Singleton»¯
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private void Update()
    {
        // ÓÎÏ·Ê±ÖÓ
        int seconds = 60+(int)Time.time;
        int minutes = -1;

        do
        {
            minutes += 1;
            seconds -= 60;
        }
        while (seconds >= 60);

        string secondstext = seconds.ToString();
        string minutestext = minutes.ToString();
        if (secondstext.Length == 1)
        {
            secondstext = "0" + secondstext;
        }
        if (minutestext.Length == 1)
        {
            minutestext = "0" + minutestext;
        }

        timerText.text = minutestext + ":" + secondstext;
    }

    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isWaveTracking = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = Random.insideUnitCircle.normalized * 8;
            spawnPosition += player.transform.position;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void CourtEnemies(GameObject enemyPrefab)
    {
        if (enemyPrefab.name == "blue(Clone)")
        {
            bluenumber++;
            BlueNumber.text = bluenumber.ToString();
        }

        else if (enemyPrefab.name == "green(Clone)")
        {
            greennumber++;
            GreenNumber.text = greennumber.ToString();
        }

        else if (enemyPrefab.name == "red(Clone)")
        {
            rednumber++;
            RedNumber.text = rednumber.ToString();
        }

        else if (enemyPrefab.name == "giant(Clone)")
        {
            giantnumber++;
            GiantNumber.text = giantnumber.ToString();
        }
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
          
            yield return new WaitForSeconds(2f);
            SpawnEnemies(blue, 5);
            yield return new WaitForSeconds(3f);
            SpawnEnemies(giant, 3);
            //yield return new WaitForSeconds(7f);
            //SpawnEnemies(red, 1);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 5, isWaveTracking: false);
            SpawnEnemies(green, 5);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 5);
            SpawnEnemies(green, 7);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 10);
            //yield return new WaitForSeconds(5f);
            //SpawnEnemies(blue, 10, isWaveTracking: false);
            //SpawnEnemies(green, 15);
            //yield return new WaitForSeconds(10f);
            //SpawnEnemies(blue, 15);
            //SpawnEnemies(green, 20 , isWaveTracking: false);
            //yield return new WaitForSeconds(10f);
            //SpawnEnemies(blue, 20, isWaveTracking: false);
            //SpawnEnemies(green, 10);
            //yield return new WaitForSeconds(15f);
            //SpawnEnemies(blue, 30);
            //SpawnEnemies(green, 20);
            //yield return new WaitForSeconds(15f);
            //SpawnEnemies(blue, 30);
            //SpawnEnemies(blue, 20, isWaveTracking: false);
        }
    }

}
