using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomItem : Item
{


    public override void Use()
    {
        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0, 0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        GameSceneManager.Instance.player.setBoom(false);
        base.Use();
    }
}
