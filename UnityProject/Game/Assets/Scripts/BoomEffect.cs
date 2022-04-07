using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{



    public AudioClip boomClip;
    void Start()
    {

    }


    public void use()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(10, 10), 0);

        foreach (Collider2D col in colliders)
        {
            if(col.gameObject.CompareTag("Enemy")){
                col.GetComponent<Unit>().damaged(50);
            }
            else if (col.gameObject.CompareTag("Bullet"))
            {
                if(col.gameObject.GetComponent<Bullet>().type == Bullet.teamType.Enemy)
                    Destroy(col.gameObject);
            }
        }

        SoundManager.Instance.PlaySFX("boom", boomClip);
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector2.zero, new Vector2(10, 10));
    }

}
