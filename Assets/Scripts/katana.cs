using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katana : BaseWeapon
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        StartCoroutine(KatanaCoroutine());
    }

    IEnumerator KatanaCoroutine()
    {
        while (true)
        {
            transform.localScale = Vector3.one * (1 + level * 0.3f); // 每次升级增加0.3倍大小

            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            yield return new WaitForSeconds(0.5f);

            spriteRenderer.enabled = true;
            boxCollider2D.enabled = true;
            yield return new WaitForSeconds(1.5f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Damage(2);
        }

    }
}
