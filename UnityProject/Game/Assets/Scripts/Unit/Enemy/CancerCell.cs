using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerCell : Enemy
{
    Vector2 dir;

    int attackCnt;

    bool isSkilling;
    public override void Start()
    {
        base.Start();
        dir = (GameSceneManager.Instance.player.transform.position - transform.position).normalized;
    }
    public override void attack()
    {
        if (bulletPrefab == null)
            return;
        if (currentAttackDelay > Time.time)
            return;
        else if (transform.position.y > 5)
            return;
        else if (transform.position.y <= GameSceneManager.Instance.player.transform.position.y)
            return;

        if(attackCnt > 1 && !isSkilling)
        {
            isSkilling = true;
            StartCoroutine(sAttackCoroutine());
        }
        else
        {
            Bullet[] buls = Instantiate(bulletPrefab, attackPos.position, Quaternion.identity).GetComponentsInChildren<Bullet>();


            foreach (Bullet b in buls)
            {
                b.power = power;
                b.speed = bulletSpeed;
                b.dir = (GameSceneManager.Instance.player.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f)) - transform.position).normalized;
            }

            attackCnt++;
            currentAttackDelay = Time.time + applyAttackDelay;

        }

    }

    IEnumerator sAttackCoroutine()
    {

        for (int i = 0; i < 3; i++)
        {
            Bullet[] buls = Instantiate(bulletPrefab, attackPos.position, Quaternion.identity).GetComponentsInChildren<Bullet>();


            foreach (Bullet b in buls)
            {
                b.power = power;
                b.speed = bulletSpeed;
                b.dir = (GameSceneManager.Instance.player.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f)) - transform.position).normalized;
            }

            yield return new WaitForSeconds(0.18f);
        }


        currentAttackDelay = Time.time + applyAttackDelay;
        attackCnt = 0;

        isSkilling = false;


    }

    public override void move()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -4.53f, 4.53f), transform.position.y);
    }
}
