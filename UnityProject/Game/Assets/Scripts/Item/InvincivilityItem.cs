using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincivilityItem : Item
{
    public override void Use()
    {
        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0, 0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        StartCoroutine(useCo());
    }


    IEnumerator useCo()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(transform.GetChild(0).gameObject);
        GameSceneManager.Instance.player.itemInvincivility = true;
        GameSceneManager.Instance.player.tempinvincivility = true;

        yield return new WaitForSeconds(2.5f);
        GameSceneManager.Instance.player.tempinvincivility = false;
        yield return new WaitForSeconds(0.5f);
        GameSceneManager.Instance.player.itemInvincivility = false;

        base.Use();
    }
}
