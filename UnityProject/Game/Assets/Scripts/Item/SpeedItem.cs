using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{

    public override void Use()
    {
        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0,0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        StartCoroutine(useCo());
    }


    IEnumerator useCo()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(transform.GetChild(0).gameObject);
        GetComponent<Collider2D>().enabled = false;

        float t = Time.time + 4;
        while (t > Time.time)
        {
            yield return null;
            GameSceneManager.Instance.player.applySpeed = GameSceneManager.Instance.player.speed + 4;

        }


        GameSceneManager.Instance.player.applySpeed = GameSceneManager.Instance.player.speed;
        base.Use();
    }
}
