using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCell : Unit
{

    public AudioClip damageClip;
    public AudioClip dieClip;
    public override void move()
    {
        base.move();
        transform.Translate(applySpeed * Vector2.down * Time.deltaTime);
    }
    public override void damaged(float power)
    {
        base.damaged(1);
        SoundManager.Instance.PlaySFX("redBloodCell damage", damageClip);
        anim.SetTrigger("Damaged");
        GameSceneManager.Instance.damaged(1);
    }


    public override void die()
    {
        SoundManager.Instance.PlaySFX("RedBloodCell Die", dieClip);
        GameSceneManager.Instance.damaged(10);
        base.die();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageLine"))
            Destroy(gameObject);
    }
}
