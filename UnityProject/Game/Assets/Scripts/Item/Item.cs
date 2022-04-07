using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string getText;
    public virtual void Use()
    {

        GameSceneManager.Instance.score += 60;
        Destroy(gameObject);
    }


    public virtual void Update()
    {
        transform.Translate(Vector2.down * 2 * Time.deltaTime);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageLine"))
            Destroy(gameObject);
    }


}
