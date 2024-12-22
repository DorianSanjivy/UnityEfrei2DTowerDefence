using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    public static int grangeMaxHealth = 100; // Santé maximale de la grange
    public static int grangeCurrentHealth = 100; // Santé actuelle de la grange
    public static int playerMoney = 999;


    public float GetCurrentHealth()
    {
        return grangeCurrentHealth;
    }

    public float GetMaxHealth()
    {
        return grangeMaxHealth;
    }
}

