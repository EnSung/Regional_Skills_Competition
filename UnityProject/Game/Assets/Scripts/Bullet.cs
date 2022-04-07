using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public enum teamType
    {
        Player,
        Enemy,
    }

    public teamType type;

    public Vector2 dir;
    public float speed;
    public float power;

    float t;

    public virtual void Start()
    {
        t = Time.time + 6;
    }
    public virtual void Update()
    {

        if (t <= Time.time)
            Destroy(gameObject);
        if(type == teamType.Player)
        {
            if (transform.position.y >= 5f)
                Destroy(gameObject);
        }

        transform.Translate(dir * speed * Time.deltaTime);
    }



    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (type)
        {
            case teamType.Player:
                if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
                {
                    collision.GetComponent<Unit>().damaged(power);
                    Destroy(Instantiate(GameSceneManager.Instance.bulletMudPrefab, transform.position, transform.rotation),0.1f);
                    Destroy(gameObject);
                }
                   
                break;
            case teamType.Enemy:
                if (!collision.CompareTag("Enemy") && !collision.CompareTag("Bullet") && !collision.CompareTag("WhiteBloodCell"))
                {
                    collision.GetComponent<Unit>().damaged(power);
                    Destroy(gameObject);
                }
                    
                break;
            default:
                break;
        }
    }
}
