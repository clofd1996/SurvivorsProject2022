using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerIndex
{
    // Singleton»¯
    private static PlayerIndex instance;

    public static PlayerIndex GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public int index = 0;
}
