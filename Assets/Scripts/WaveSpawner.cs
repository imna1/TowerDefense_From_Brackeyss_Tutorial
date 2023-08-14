using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive;

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform startNode;
    [SerializeField] private Text textWaveCountDown;
    [SerializeField] private MoneyUI moneyUI;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float incrementMultiplayer;
    [SerializeField] private int wayCounts = 1;

    private GameManager gameManager;
    private int waveIndex = 0;
    private int enemiesCount;
    private float countDown = 4;
    private float multiplayer = 1;

    private void Start()
    {
        gameManager = GameManager.instance;
        enemiesAlive = 0;
    }

    private void Update()
    {
        if (enemiesAlive > 0 || gameManager.gameIsOver)
            return;

        if (waveIndex == waves.Length)
        {
            gameManager.WonGame();
            enabled = false;
            return;
        }

        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        textWaveCountDown.text = string.Format("{0:00.00}", countDown);
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        enemiesCount = 0;
        foreach (var count in wave.counts)
        {
            enemiesCount += count;
        }
        enemiesAlive = enemiesCount;
        int remainingEnemies = enemiesCount;
        for (int i = 0; i < enemiesCount; i++)
        {
            bool nextSpawn = true;
            int checkedEnemies = 0;
            int enemyIndex = Random.Range(0, remainingEnemies);
            for (int j = 0; nextSpawn; j++)
            {
                bool isTheLastOne = false;
                if (j + 1 == wave.enemies.Length)
                    isTheLastOne = true;
                else
                    checkedEnemies += wave.counts[j];

                if (checkedEnemies > enemyIndex || isTheLastOne)
                {
                    GameObject go = Instantiate(wave.enemies[j], startNode.position, startNode.rotation);
                    Enemy goComponent = go.GetComponent<Enemy>();
                    goComponent.multiplayer = multiplayer;
                    goComponent.wayIndex = i % wayCounts;
                    wave.counts[j]--;
                    remainingEnemies--;
                    nextSpawn = false;
                    yield return new WaitForSeconds(wave.timeBetweenEnemies);
                }

            }
        }
        PlayerStats.waves++;
        waveIndex++;
        multiplayer += incrementMultiplayer;
    }
}
