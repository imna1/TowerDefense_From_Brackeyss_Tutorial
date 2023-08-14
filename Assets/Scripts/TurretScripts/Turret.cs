using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float turnSpeed;
    [SerializeField] protected float shootAngle;
    [SerializeField] protected Transform partToRotation;
    [SerializeField] protected Transform shootPosition;
    
    protected float distanceToEnemy;
    protected float shortestDistance;
    protected bool isLookAtEnemy = false;
    protected GameObject nearestEnemy;
    protected GameObject[] enemies;
    protected Transform target;
    protected Vector3 direction;
    
    protected void UpdateTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        shortestDistance = Mathf.Infinity;
        _ = nearestEnemy == null;
        foreach (var enemy in enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
            target = null;
    }
    protected void LookAtTarget()
    {
        Vector3 direction = target.position - partToRotation.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        partToRotation.rotation = Quaternion.Lerp(partToRotation.rotation, lookRotation, Time.deltaTime * turnSpeed);
        float angle = partToRotation.rotation.eulerAngles.y;
        if (angle < shootAngle)
        {
            if (angle + shootAngle > lookRotation.eulerAngles.y || lookRotation.eulerAngles.y > 360 - angle)
                isLookAtEnemy = true;
            else
                isLookAtEnemy = false;
        }
        else if (angle > 360 - shootAngle)
        {
            if (angle - shootAngle < lookRotation.eulerAngles.y || lookRotation.eulerAngles.y < 0 + angle)
                isLookAtEnemy = true;
            else
                isLookAtEnemy = false;
        }
        else 
        {
            if (angle - shootAngle < lookRotation.eulerAngles.y && angle + shootAngle > lookRotation.eulerAngles.y)
                isLookAtEnemy = true;
            else
                isLookAtEnemy = false;
        } 
    }
    
}
