using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{

    public enum healType
    {
        Player,
        Pain
    }

    public healType type;

    public float healAmount;
    public override void Use()
    {
        switch (type)
        {
            case healType.Player:
                GameSceneManager.Instance.player.Hp += healAmount;
                break;
            case healType.Pain:
                GameSceneManager.Instance.StageHp += healAmount;
                break;
            default:
                break;
        }

        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0, 0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        base.Use();
    }
}
