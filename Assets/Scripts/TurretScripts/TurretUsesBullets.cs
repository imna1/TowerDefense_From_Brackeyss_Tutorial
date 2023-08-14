using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUsesBullets : Turret
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotSpeed;

    protected float fireCountdown;
    protected GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }
    
    void Update()
    {
        fireCountdown -= Time.deltaTime;
        if (target == null || gameManager.gameIsOver == true)
        {
            return;
        }

        LookAtTarget();

        if (fireCountdown <= 0f && isLookAtEnemy == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation);
            bullet.GetComponent<Bullet>().Target(target);
            fireCountdown = shotSpeed;
        }
    }
}
