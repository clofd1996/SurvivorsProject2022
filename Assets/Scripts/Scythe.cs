using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ScytheCoroutine());
    }

    void Update()
    {
        transform.position += transform.up * 10 * Time.deltaTime; //10代表移动速度
    }

    IEnumerator ScytheCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(1);
            gameObject.SetActive(false);
        }
    }
}
