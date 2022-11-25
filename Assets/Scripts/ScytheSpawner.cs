using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSpawner : BaseWeapon
{
    [SerializeField] GameObject scythe;
    [SerializeField] SimpleObjectPool pool;

    private void Start()
    {
        StartCoroutine(SpawnScytheCoroutine());
    }

    IEnumerator SpawnScytheCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i< level; i++) // 每升一级，多一把飞刀
            {
                float angle = Random.Range(0, 360);
                //Instantiate(scythe, transform.position, Quaternion.Euler(0, 0, angle));
                var scythe = pool.Get();
                scythe.transform.position = transform.position;
                scythe.transform.rotation = Quaternion.Euler(0, 0, angle);
                scythe.SetActive(true);
            }
            
        }
    }
}
