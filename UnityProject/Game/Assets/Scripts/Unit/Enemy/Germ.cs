using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germ : Enemy
{
    Vector2 dir;
    public override void Start()
    {
        base.Start();
        StartCoroutine(moveCoroutine());
    }


    public override void move()
    {
        transform.Translate((Vector2.down + dir).normalized * applySpeed * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -4.64f, 4.64f), transform.position.y);

    }
    IEnumerator moveCoroutine()
    {
        while(Hp > 0)
        {
            yield return null;

            int ran = Random.Range(-1, 2);
            dir = new Vector2(ran,0);

            yield return new WaitForSeconds(0.65f);


        }
    }


}
