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
    [SerializeField] float spawnDistance = 8f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

     private void Update()
    {
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
            //yield return new WaitForSeconds(5f);
            //SpawnEnemies(blue, 5, isWaveTracking: false);
            //SpawnEnemies(green, 5);
            //yield return new WaitForSeconds(5f);
            //SpawnEnemies(blue, 5);
            //SpawnEnemies(green, 7);
            //yield return new WaitForSeconds(5f);
            //SpawnEnemies(blue, 10);
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