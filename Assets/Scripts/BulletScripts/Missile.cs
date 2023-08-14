using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    [SerializeField] private float explosionRadius;

    private float appliedDamage;

    void Update()
    {
        if (target == null || gameManager.gameIsOver == true)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = target.position - transform.position;
        float unitsToMove = speed * Time.deltaTime;
        if (unitsToMove >= direction.magnitude)
        {
            HitTarget();
        }
        transform.Translate(direction.normalized * unitsToMove, Space.World);
        transform.LookAt(target);
    }

    protected override void HitTarget()
    {
        GameObject bulletShelters = Instantiate(bulletParticles, transform.position, transform.rotation);
        Destroy(bulletShelters, 1.3f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                appliedDamage = damage - (Vector3.Distance(transform.position, collider.transform.position) / explosionRadius) * damage / 3.5f;
                collider.GetComponent<Enemy>().TakeDamage(appliedDamage);
            }
        }
        Destroy(gameObject);
    }
}
