using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private float hp;
    [SerializeField] private float maxHp;
    public float speed;
    public float applySpeed;

    public float applyPower;
    public float power;

    public float currentAttackDelay;
    public float applyAttackDelay;
    public float attackDelay;

    public float bulletSpeed;

    public Animator anim;
    
    public float Hp
    {
        get { return hp; }
        set { hp = value;
            if (hp > maxHp)
                hp = maxHp;
        }
    }

    public float MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    public virtual void Start()
    {
        hp = MaxHp;
        applyAttackDelay = attackDelay;
        applyPower = power;
        applySpeed = speed;
        try
        {
            anim = GetComponent<Animator>();
        }
        catch
        {

        }
    }

    public virtual void Update()
    {
        move();
        attack();
    }

    public virtual void move()
    {

    }

    public virtual void attack()
    {

    }

    public virtual void damaged(float power)
    {
        Hp -= power;
        if (hp <= 0)
            die();
    }

    public virtual void die()
    {
        Destroy(gameObject);
    }
}
