using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantKnife : MonoBehaviour
{
    protected GameObject player; //定义一下GameObject具体是什么
    Vector3 direction;

    internal void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //定义下面update里player具体指代什么

        Vector3 destination = player.transform.position; //player的位置
        Vector3 source = transform.position; //匕首的位置(knife本身)
        direction = destination - source; //player和enemy之间的向量
        direction.Normalize(); //单位向量化，原本不到1的，长度可以变成1      

        StartCoroutine(GiantKnifeCoroutine());
    }

    IEnumerator GiantKnifeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }

    internal void Update()
    {
        transform.position += direction * Time.deltaTime * 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            if (player.OnDamage())
            {
                Destroy(gameObject);
            }
        }
    }

}
