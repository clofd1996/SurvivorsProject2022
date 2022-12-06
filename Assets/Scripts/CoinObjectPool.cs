using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObjectPool : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;

    List<GameObject> pooledObjects = new List<GameObject>();

    int objectIndex;

    private void Awake()
    {
        for (int i = 0; i < 500; i++)
        {
            pooledObjects.Add(Instantiate(objectPrefab));
        }
    }

    public GameObject Get()
    {
        if (objectIndex >= pooledObjects.Count)
        {
            objectIndex = 0;
        }

        objectIndex %= pooledObjects.Count;

        var result = pooledObjects[objectIndex++];
        return result;
    }
}
