using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBullet : Bullet
{
    
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
}
