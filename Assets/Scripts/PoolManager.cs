using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public SimpleObjectPool exppool;
    public SimpleObjectPool coinpool;
    public SimpleObjectPool giantknifepool;
    public SimpleObjectPool fireballpool;

    public SimpleObjectPool bluepool;
    public SimpleObjectPool greenpool;
    public SimpleObjectPool redpool;
    public SimpleObjectPool giantpool;

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
