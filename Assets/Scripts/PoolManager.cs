using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public SimpleObjectPool exppool;
    public SimpleObjectPool coinpool;
    //public SimpleObjectPool enemypool;
    public SimpleObjectPool giantknifepool;

    // Singleton»¯
    private static PoolManager instance;

    public static PoolManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
}
