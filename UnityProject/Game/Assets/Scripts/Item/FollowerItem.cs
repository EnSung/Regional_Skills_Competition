using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerItem : Item
{
    public override void Use()
    {
        if(GameSceneManager.Instance.player.follwerCnt < 4){
            GameSceneManager.Instance.player.follwerCnt++;
        }

        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0, 0.2f), transform.rotation).GetComponent<GetText>();
        t.text.text = getText;
        GameSceneManager.Instance.player.getFollower();
        base.Use();
    }
}
