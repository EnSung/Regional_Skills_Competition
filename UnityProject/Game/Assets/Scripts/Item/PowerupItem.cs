using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupItem : Item
{

    public override void Use()
    {
        if (!GameSceneManager.Instance.player.isSpecialAttack)
            GameSceneManager.Instance.player.AttackLevel++;

        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0, 0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        base.Use();
    }
}
