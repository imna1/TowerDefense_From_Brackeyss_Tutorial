using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int startMoney;
    public static int money;
    
    [SerializeField] private int startLives;
    public static int lives;

    public static int waves;
    
    public static int killingEnemies;

    private void Awake()
    {
        money = startMoney;
        lives = startLives;
        waves = 0;
        killingEnemies = 0;
    }
}
