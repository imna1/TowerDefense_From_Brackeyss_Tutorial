using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public float multiplayer;
    [HideInInspector] public int wayIndex;

    [SerializeField] private int moneyGain;
    [SerializeField] private float startSpeed;
    [SerializeField] private float startHealth;
    [SerializeField] private GameObject enemyDeathEffect;
    [SerializeField] private Image healthBar;

    private GameManager gameManager;
    private Transform targetWaypoint;
    private int waypointIndex;
    private float speed;
    private float health;
    private float calculatedHealth;
    private float slowCountdown;
    private float slowMultiplayer;
    private bool isDead;

    private void Start()
    {
        calculatedHealth = startHealth * multiplayer;
        health = calculatedHealth;
        speed = startSpeed * ((multiplayer - 1) * 0.6f + 1);
        gameManager = GameManager.instance;
        waypointIndex = 0;
        targetWaypoint = Waypoints.points[wayIndex][waypointIndex];
    }
    private void Update()
    {
        if (gameManager.gameIsOver == true)
            return;
        if (slowCountdown > 0f)
        {
            speed = startSpeed * (1 - slowMultiplayer);
            slowCountdown -= Time.deltaTime;

        }
        else
        {
            speed = startSpeed;
            slowMultiplayer = 0;
        }
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, targetWaypoint.position) < 0.3f)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points[wayIndex].Length - 1)
        {
            gameManager.SubtractLive();
            WaveSpawner.enemiesAlive--;
            Destroy(gameObject);
            return;
        }
        targetWaypoint = Waypoints.points[wayIndex][++waypointIndex];
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / calculatedHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    public void UpdateSlow(float slowFactor, float duration)
    {
        slowCountdown = duration;
        if(slowMultiplayer < slowFactor)
            slowMultiplayer = slowFactor;
    }
    void Die()
    {
        isDead = true;
        PlayerStats.money += moneyGain;
        PlayerStats.killingEnemies++;
        WaveSpawner.enemiesAlive--;
        EventBus.UpdateMoneyEvent.Invoke();
        GameObject effect = (GameObject)Instantiate(enemyDeathEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
