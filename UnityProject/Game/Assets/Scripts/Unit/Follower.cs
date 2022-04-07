using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Unit
{

    public GameObject bulletPrefab;

    public override void attack()
    {
        if (currentAttackDelay > Time.time)
            return;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(10, 10), 0);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                Bullet bul = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
                bul.dir = (col.transform.position - transform.position).normalized;
                bul.speed = bulletSpeed;
                bul.power = power;


                break;
            }
        }

        currentAttackDelay = Time.time + applyAttackDelay;
    }
}
