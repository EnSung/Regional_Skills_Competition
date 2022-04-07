using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{

    public GameObject sAttackBullet;

    Vector2 dir;


    int attackCnt;
    bool isSattack;
    public override void Start()
    {
        base.Start();
        dir = Vector2.down;
        StartCoroutine(dashCoroutine());
    }

    public override void move()
    {
        transform.Translate(dir * applySpeed * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -4.19f, 4.19f), transform.position.y);
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

        if (attackCnt >= 2 && !isSattack)
        {
            isSattack = true;
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
            float ran = Random.Range(-40f, 40f);
            for (int j = 0; j < 360; j+= 60)
            {
                Bullet bul = Instantiate(sAttackBullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

                bul.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, j + ran));
                bul.dir = Vector2.right;
                bul.speed = bulletSpeed;
                bul.power = power;


            }
            yield return new WaitForSeconds(0.8f);
        }

        
        isSattack = false;
        currentAttackDelay = Time.time + applyAttackDelay;
        attackCnt = 0;
    }

    IEnumerator dashCoroutine()
    {
        yield return new WaitForSeconds(1);
        while (Hp > 0)
        {
            yield return null;
            int ran = Random.Range(0, 4);

            switch (ran)
            {
                case 0:
                    dir = Vector2.up;
                    break;
                case 1:
                    dir = Vector2.down;
                    break;
                case 2:
                    dir = Vector2.left;
                    break;
                case 3:
                    dir = Vector2.right;
                    break;
                default:
                    break;
            }


            float t = Time.time + 1;
            applySpeed = 7;

            while (t > Time.time)
            {
                yield return null;
                applySpeed = Mathf.Lerp(applySpeed, speed, 9 * Time.deltaTime);
            }

            dir = Vector2.down;

            yield return new WaitForSeconds(3);
    }
    }
}

