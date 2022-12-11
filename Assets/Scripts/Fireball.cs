using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    protected GameObject player; //定义一下GameObject具体是什么
    Vector3 direction;

    public int giantKnifeDamage = 4;

    internal void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //定义下面update里player具体指代什么

        Vector3 destination = player.transform.position; //player的位置
        Vector3 source = transform.position; //火球的位置(fireball本身)
        direction = destination - source; //player和enemy之间的向量
        direction.Normalize(); //单位向量化，原本不到1的，长度可以变成1      

        StartCoroutine(FireballCoroutine());
    }

    IEnumerator FireballCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f); // 火球经过3秒才会消失
            gameObject.SetActive(false);
        }
    }

    internal void Update()
    {
        transform.position += direction * Time.deltaTime * 6; //飞行速度为6（匕首是4）
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.Damage(giantKnifeDamage); // 碰撞给予 Giant Knife Damage = 4点伤害
            gameObject.SetActive(false); //匕首碰撞后消失
        }
    }

}
