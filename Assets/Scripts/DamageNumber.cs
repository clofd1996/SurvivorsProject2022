using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(DamageNumberCoroutine());
    //}

    //IEnumerator DamageNumberCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1.5f);
    //        gameObject.SetActive(false);
    //    }
    // }
    public float destroyTime;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
