using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject bulletParticles;

    protected Transform target;
    protected GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void Target(Transform target)
    {
        this.target = target;
    }

    protected virtual void HitTarget()
    {
        GameObject bulletShelters = Instantiate(bulletParticles, transform.position, transform.rotation);
        target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(bulletShelters, 1.3f);
        Destroy(gameObject);
    }
}
