using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected int level;

    internal void LevelUp()
    {
        if (++level == 1) // Éýµ½1¼¶
        {
            gameObject.SetActive(true);
        }

        


    }
}
