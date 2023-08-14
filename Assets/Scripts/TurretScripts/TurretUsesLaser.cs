using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUsesLaser : Turret
{
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float slowMultiplayer;
    [SerializeField] private float slowDuration;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem laserImpactEffect;
    [SerializeField] private Light laserImpactLight;

    protected Enemy enemyComponent;

    void Start()
    {
        lineRenderer.positionCount = 2;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    void Update()
    {
        if (target == null)
        {
            if (lineRenderer.enabled)
            {
                laserImpactEffect.Stop();
                laserImpactLight.enabled = false;
                lineRenderer.enabled = false;
            }
            return;
        }

        LookAtTarget();
        if (isLookAtEnemy == true)
        {
            enemyComponent = target.GetComponent<Enemy>();
            enemyComponent.TakeDamage(damagePerSecond * Time.deltaTime);
            enemyComponent.UpdateSlow(slowMultiplayer, slowDuration);
            if (!lineRenderer.enabled)
            {
                laserImpactEffect.Play();
                laserImpactLight.enabled = true;
                lineRenderer.enabled = true;
            }
            lineRenderer.SetPosition(0, shootPosition.position);
            lineRenderer.SetPosition(1, target.position);
            direction = shootPosition.position - target.position;
            laserImpactEffect.transform.rotation = Quaternion.LookRotation(direction);
            laserImpactEffect.transform.position = target.position + direction.normalized;
        }
    }
}
