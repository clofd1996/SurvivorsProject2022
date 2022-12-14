using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject green;
    [SerializeField] GameObject red;
    [SerializeField] GameObject giant;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject background;
    [SerializeField] GameObject background2;

    public Player[] player;

    [SerializeField] TMP_Text BlueNumber;
    [SerializeField] TMP_Text GreenNumber;
    [SerializeField] TMP_Text RedNumber;
    [SerializeField] TMP_Text GiantNumber;
    [SerializeField] TMP_Text BossNumber;

    [SerializeField] TMP_Text ResumeNumber;

    public Player currentPlayer;

    private int bluenumber = 0;
    private int greennumber = 0;
    private int rednumber = 0;
    private int giantnumber = 0;
    private int bossnumber = 0;

    public GameObject boss1;

    // use for stop coroutine
    public bool flag;

    // Singleton»¯
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        ChangeCharacter();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        flag = true;
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

        if (minutes == 1 && flag == true) // Time limit, go to next stage
        {
            // Stop Coroutine
            StopCoroutine(SpawnEnemyCoroutine());
            // Destroy All Enemy
            PoolManager.GetInstance().bluepool.Destroy();
            PoolManager.GetInstance().greenpool.Destroy();
            PoolManager.GetInstance().redpool.Destroy();
            PoolManager.GetInstance().giantpool.Destroy();
            Destroy(boss1);
            // Destroy All Drops
            PoolManager.GetInstance().coinpool.Destroy();
            PoolManager.GetInstance().exppool.Destroy();
            // Change Map
            background.SetActive(false); 
            background2.SetActive(true);
            // Start New Coroutine
            StartCoroutine(SpawnEnemyCoroutine2());
            flag = false;
        }
    }

    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isWaveTracking = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = Random.insideUnitCircle.normalized * 8;
            spawnPosition += currentPlayer.transform.position;

            //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (enemyPrefab == blue)
            {
                var enemyprefab = PoolManager.GetInstance().bluepool.Get();
                enemyprefab.transform.position = spawnPosition;
                enemyprefab.transform.rotation = Quaternion.identity;
                enemyprefab.SetActive(true);
            }

            else if (enemyPrefab == green)
            {
                var enemyprefab = PoolManager.GetInstance().greenpool.Get();
                enemyprefab.transform.position = spawnPosition;
                enemyprefab.transform.rotation = Quaternion.identity;
                enemyprefab.SetActive(true);
            }

            else if (enemyPrefab == red)
            {
                var enemyprefab = PoolManager.GetInstance().redpool.Get();
                enemyprefab.transform.position = spawnPosition;
                enemyprefab.transform.rotation = Quaternion.identity;
                enemyprefab.SetActive(true);
            }

            else if (enemyPrefab == giant)
            {
                var enemyprefab = PoolManager.GetInstance().giantpool.Get();
                enemyprefab.transform.position = spawnPosition;
                enemyprefab.transform.rotation = Quaternion.identity;
                enemyprefab.SetActive(true);
            }

            else if (enemyPrefab == boss)
            {
                boss1 =Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void CourtEnemies(GameObject enemyPrefab)
    {
        if (enemyPrefab.tag == "blue")
        {
            bluenumber++;
            BlueNumber.text = bluenumber.ToString();
        }

        else if (enemyPrefab.tag == "green")
        {
            greennumber++;
            GreenNumber.text = greennumber.ToString();
        }

        else if (enemyPrefab.tag == "red")
        {
            rednumber++;
            RedNumber.text = rednumber.ToString();
        }

        else if (enemyPrefab.tag == "giant")
        {
            giantnumber++;
            GiantNumber.text = giantnumber.ToString();
        }

        else if (enemyPrefab.tag == "boss")
        {
            bossnumber++;
            BossNumber.text = bossnumber.ToString();
        }
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
          
            yield return new WaitForSeconds(2f);
            SpawnEnemies(blue, 5);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 5, isWaveTracking: false);
            SpawnEnemies(green, 5);
            yield return new WaitForSeconds(2f);
            SpawnEnemies(red, 1);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 5);
            SpawnEnemies(green, 7);
            yield return new WaitForSeconds(3f);
            SpawnEnemies(giant, 2);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 10);

            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 10, isWaveTracking: false);
            SpawnEnemies(giant, 3);
            yield return new WaitForSeconds(10f);
            SpawnEnemies(blue, 15);
            SpawnEnemies(green, 20 , isWaveTracking: false);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(boss, 1);
            yield return new WaitForSeconds(10f);
            SpawnEnemies(giant, 5);
            SpawnEnemies(green, 10);
            yield return new WaitForSeconds(15f);
            SpawnEnemies(blue, 30);
            SpawnEnemies(green, 20);
            yield return new WaitForSeconds(15f);
            SpawnEnemies(blue, 30);
            SpawnEnemies(blue, 20, isWaveTracking: false);
        }
    }

    IEnumerator SpawnEnemyCoroutine2()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            SpawnEnemies(green, 5);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(giant, 5);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 5, isWaveTracking: false);
            SpawnEnemies(red, 5);
            yield return new WaitForSeconds(2f);
            SpawnEnemies(red, 1);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(red, 5);
            SpawnEnemies(giant, 7);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(giant, 2);
            SpawnEnemies(red, 10);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 10);
            SpawnEnemies(giant, 5);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(blue, 10, isWaveTracking: false);
            SpawnEnemies(red, 15);
            yield return new WaitForSeconds(10f);
            SpawnEnemies(blue, 15);
            SpawnEnemies(green, 20, isWaveTracking: false);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(boss, 1);
            yield return new WaitForSeconds(10f);
            SpawnEnemies(red, 20, isWaveTracking: false);
            SpawnEnemies(green, 10);
            yield return new WaitForSeconds(15f);
            SpawnEnemies(blue, 30);
            SpawnEnemies(green, 20);
            yield return new WaitForSeconds(15f);
            SpawnEnemies(blue, 30);
            SpawnEnemies(blue, 20, isWaveTracking: false);

        }
    }

    public void ActiveCounter()
    {
        ResumeNumber.gameObject.SetActive(true);
    }

    public void DetiveCounter()
    {
        ResumeNumber.gameObject.SetActive(false);
    }

    public void ChangeCounter(string number)
    {
        ResumeNumber.text = number;
    }

    public void ChangeCharacter()
    {
        currentPlayer.gameObject.SetActive(false);
        currentPlayer = player[Bool.index];
        
        Debug.Log("show" + Bool.index);
        currentPlayer.gameObject.SetActive(true);
    }
}
