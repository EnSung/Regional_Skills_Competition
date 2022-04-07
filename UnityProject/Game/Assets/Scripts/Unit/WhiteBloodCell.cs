using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : Unit
{
    Vector2 dir;
    bool isDie;


    public AudioClip damageClip;
    public AudioClip dieClip;

    public override void attack()
    {
        base.attack();
    }

    public override void damaged(float power)
    {
        anim.SetTrigger("Damaged");
        SoundManager.Instance.PlaySFX("WhiteBloodCell damage", damageClip);
        base.damaged(power);
    }

    public override void die()
    {
        if (!isDie)
        {
            isDie = true;
            Instantiate(GameSceneManager.Instance.itemPrefabs[Random.Range(0, GameSceneManager.Instance.itemPrefabs.Length)], transform.position, transform.rotation);
            SoundManager.Instance.PlaySFX("WhiteBloodCell die", dieClip);
            base.die();
        }
            
    }


    public override void move()
    {
        base.move();

        transform.Translate(dir * speed * Time.deltaTime);
    }

    public override void Start()
    {
        base.Start();
        dir = new Vector2(Random.Range(-1, 2) * 4, Random.Range(-4,-1)).normalized;
    }


    public override void Update()
    {
        base.Update();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
        }
        else if (collision.gameObject.CompareTag("DamageLine"))
            Destroy(gameObject);
    }
}
