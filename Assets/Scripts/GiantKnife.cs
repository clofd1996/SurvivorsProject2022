using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantKnife : MonoBehaviour
{
    protected GameObject player; //����һ��GameObject������ʲô
    Vector3 direction;

    internal void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //��������update��player����ָ��ʲô

        Vector3 destination = player.transform.position; //player��λ��
        Vector3 source = transform.position; //ذ�׵�λ��(knife����)
        direction = destination - source; //player��enemy֮�������
        direction.Normalize(); //��λ��������ԭ������1�ģ����ȿ��Ա��1      

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
