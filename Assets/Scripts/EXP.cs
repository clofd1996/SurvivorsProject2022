using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.AddExp();
            TitleManager.saveData.expGetten++;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
