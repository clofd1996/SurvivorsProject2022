using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    protected GameObject player; //����һ��GameObject������ʲô
    Vector3 direction;

    public int giantKnifeDamage = 4;

    internal void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //��������update��player����ָ��ʲô

        Vector3 destination = player.transform.position; //player��λ��
        Vector3 source = transform.position; //�����λ��(fireball����)
        direction = destination - source; //player��enemy֮�������
        direction.Normalize(); //��λ��������ԭ������1�ģ����ȿ��Ա��1      

        StartCoroutine(FireballCoroutine());
    }

    IEnumerator FireballCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f); // ���򾭹�3��Ż���ʧ
            gameObject.SetActive(false);
        }
    }

    internal void Update()
    {
        transform.position += direction * Time.deltaTime * 6; //�����ٶ�Ϊ6��ذ����4��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.Damage(giantKnifeDamage); // ��ײ���� Giant Knife Damage = 4���˺�
            gameObject.SetActive(false); //ذ����ײ����ʧ
        }
    }

}
