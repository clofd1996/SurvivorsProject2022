using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject expPrefab; //经验prefab
    [SerializeField] float speed = 1f; //怪物速度
    [SerializeField] protected float HP = 2; //怪物HP
    [SerializeField] protected float MaxHP = 2; //怪物最大HP

    [SerializeField] GameObject damageNumber; // 伤害数字
    public int enemyDamage = 1;
    public bool isTrackingPlayer = true;
    protected GameObject player; //定义一下GameObject具体是什么
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //定义下面update里player具体指代什么
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.Damage(enemyDamage); //碰撞造成 Enemy Damage = 1点伤害
            gameObject.SetActive(false); //怪物碰撞后消失
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 destination = player.transform.position; //player的位置
        Vector3 source = transform.position; //怪物的位置(enemy本身)

        Vector3 direction = destination - source; //player和enemy之间的向量

        if (isTrackingPlayer == false) //如果怪物没有追踪玩家
        {
            direction = new Vector3(1, 0, 0); //往右直走
        }

        direction.Normalize(); //单位向量化，原本不到1的，长度可以变成1

        transform.position += direction * Time.deltaTime * speed;
        transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1); //自动转向

        

    }

    public virtual void Damage(int damage)
    {
        GameObject gb = Instantiate(damageNumber, transform.position, Quaternion.identity) as GameObject; // 生成伤害值气泡
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        HP -= damage;     
        
        if (HP <= 0)
        {
            Instantiate(expPrefab, transform.position, Quaternion.identity); //生成exp道具
            Destroy(gameObject);
        }
    }

    void SpawnDamageNumber(TMP_Text damageText, int damage)
    {
        Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 0.1f;
        spawnPosition += transform.position;

        //Instantiate(damageText, spawnPosition, Quaternion.identity);
    }
}
