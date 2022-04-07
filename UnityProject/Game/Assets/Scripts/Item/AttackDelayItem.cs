using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelayItem : Item
{

    public override void Use()
    {
        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab,transform.position + new Vector3(0, 0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        StartCoroutine(useCo());
    }


    IEnumerator useCo()
    {
        if (GameSceneManager.Instance.player.isSpecialAttack)
            base.Use();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(1).gameObject);
        float t = Time.time + 4;
        while (t > Time.time)
        {
            yield return null;
            GameSceneManager.Instance.player.applyAttackDelay = GameSceneManager.Instance.player.attackDelay -0.1f;

        }


        GameSceneManager.Instance.player.applyAttackDelay = GameSceneManager.Instance.player.attackDelay;
        base.Use();
    }
}
