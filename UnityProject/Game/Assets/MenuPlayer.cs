using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{


    Animator anim;
    public GameObject bulletPrefab;

    public AudioClip atkclip;
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Cotin());
    }

    void Update()
    {
    }



    IEnumerator Cotin()
    {


        for (int i = 0; i < 100; i++)
        {
            yield return null;

            Vector2 dir = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 2.39f));

            while (Vector2.Distance(transform.position,dir) > 0.1f)
            {
                yield return null;
                transform.position = Vector2.MoveTowards(transform.position, dir, 5 * Time.deltaTime);

                if (dir.x > transform.position.x)
                {
                    anim.SetInteger("h", 1);
                }
                else if (dir.x < transform.position.x)
                {
                    anim.SetInteger("h", -1);
                }
                else
                    anim.SetInteger("h", 0);
            }

            anim.SetInteger("h", 0);

            Bullet bul = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponentInChildren<Bullet>();
            bul.dir = Vector2.up;
            bul.speed = 5;
            SoundManager.Instance.PlaySFX("palyersta", atkclip);

            yield return new WaitForSeconds(1.5f);
        }

    }
}
