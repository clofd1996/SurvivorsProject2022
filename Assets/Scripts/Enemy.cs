using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject expPrefab; //����prefab
    [SerializeField] float speed = 1f; //�����ٶ�
    [SerializeField] protected float HP = 2; //����HP
    [SerializeField] protected float MaxHP = 2; //�������HP

    [SerializeField] GameObject damageNumber; // �˺�����
    public int enemyDamage = 1;
    public bool isTrackingPlayer = true;
    protected GameObject player; //����һ��GameObject������ʲô
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //��������update��player����ָ��ʲô
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.Damage(enemyDamage); //��ײ��� Enemy Damage = 1���˺�
            gameObject.SetActive(false); //������ײ����ʧ
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 destination = player.transform.position; //player��λ��
        Vector3 source = transform.position; //�����λ��(enemy����)

        Vector3 direction = destination - source; //player��enemy֮�������

        if (isTrackingPlayer == false) //�������û��׷�����
        {
            direction = new Vector3(1, 0, 0); //����ֱ��
        }

        direction.Normalize(); //��λ��������ԭ������1�ģ����ȿ��Ա��1

        transform.position += direction * Time.deltaTime * speed;
        transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1); //�Զ�ת��

        

    }

    public virtual void Damage(int damage)
    {
        GameObject gb = Instantiate(damageNumber, transform.position, Quaternion.identity) as GameObject; // �����˺�ֵ����
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        HP -= damage;     
        
        if (HP <= 0)
        {
            Instantiate(expPrefab, transform.position, Quaternion.identity); //����exp����
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
