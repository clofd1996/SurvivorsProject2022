using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.up * 10 * Time.deltaTime; //10代表移动速度
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(2);
            gameObject.SetActive(false);
        }
    }
}
