using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : Bullet
{


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
            {
                collision.GetComponent<Unit>().damaged(power);
            }
            if (collision.CompareTag("Bullet"))
            {
                try
                {
                    if (collision.GetComponent<Bullet>().type == teamType.Enemy)
                        Destroy(collision.gameObject);

                }
                catch
                {

                }
            }
        }
        catch
        {

        }
        
    }
}
