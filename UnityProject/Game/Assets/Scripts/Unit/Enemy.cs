using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{

    public Transform attackPos;
    public GameObject bulletPrefab;

    public int getScore;

    public bool isBacteria;

    public bool isDie;

    [Header("AudioClip")]
    public AudioClip damageClip;
    public AudioClip dieClip;
    public override void Start()
    {
        GameSceneManager.Instance.monsterList.Add(gameObject);

        if (!isBacteria)
        {
            MaxHp += 2 * GameSceneManager.Instance.player.AttackLevel + GameSceneManager.Instance.currentStage * 5;
            power += 2 * GameSceneManager.Instance.currentStage * 8;
        }
        
        
        StartCoroutine(scaleCoroutine(transform.localScale.x,transform.localScale.y));
        anim = GetComponent<Animator>();
        base.Start();
    }
    public override void move()
    {
        base.move();
        transform.Translate(Vector2.down * applySpeed * Time.deltaTime);
    }

    public override void attack()
    {
        if (bulletPrefab == null)
            return;
        base.attack();
        if (currentAttackDelay > Time.time)
            return;
        else if (transform.position.y > 5)
            return;
        else if (transform.position.y <= GameSceneManager.Instance.player.transform.position.y)
            return;

        Bullet[] buls = Instantiate(bulletPrefab, attackPos.position, Quaternion.identity).GetComponentsInChildren<Bullet>();


        foreach (Bullet b in buls)
        {
            b.power = power;
            b.speed = bulletSpeed;
            b.dir = (GameSceneManager.Instance.player.transform.position + new Vector3(Random.Range(-0.4f,0.4f), Random.Range(-0.4f, 0.4f)) -  transform.position).normalized;
        }

        currentAttackDelay = Time.time + applyAttackDelay;
    }

    public override void damaged(float power)
    {
        SoundManager.Instance.PlaySFX("EnemyDamage", damageClip);
        anim.SetTrigger("Damaged");
        base.damaged(power);
    }

    public override void die()
    {
        if (!isDie)
        {
            isDie = true;
            Instantiate(GameSceneManager.Instance.dieEffectPrefab, transform.position, transform.rotation);
            GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0,0.5f), transform.rotation).GetComponent<GetText>();
            t.text.text = "+" + getScore.ToString();

            int ran = Random.Range(0, 1001);

            if (ran >= 850)
                Instantiate(GameSceneManager.Instance.itemPrefabs[Random.Range(0, GameSceneManager.Instance.itemPrefabs.Length)],transform.position,transform.rotation);
            GameSceneManager.Instance.score += getScore;

            if (dieClip != null)
                SoundManager.Instance.PlaySFX("enemyDie", dieClip);
            base.die();
        }
        
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageLine"))
        {
            GameSceneManager.Instance.damaged(power * 0.5f);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
            collision.GetComponent<Unit>().damaged(power * 0.5f);
    }



    public virtual void OnDestroy()
    {
        GameSceneManager.Instance.monsterList.Remove(gameObject);

    }

    IEnumerator scaleCoroutine(float x, float y)
    {
        transform.localScale = Vector2.zero;

        float t = Time.time + 1.5f;
        while(t > Time.time)
        {
            yield return null;
            transform.localScale = new Vector2(Mathf.Lerp(transform.localScale.x, x, 9 * Time.deltaTime), Mathf.Lerp(transform.localScale.y, y, 9 * Time.deltaTime));
            
        }

        transform.localScale = new Vector2(x, y);
    }

}
