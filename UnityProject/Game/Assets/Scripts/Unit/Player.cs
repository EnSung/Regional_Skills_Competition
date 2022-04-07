using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private int attackLevel;
    public int prevAttackLevel;


    public Transform attackPos;
    public GameObject[] bulletPrefabs;

    public bool damageInvincivility;
    public bool cheatInvincivility;
    public bool itemInvincivility;
    public bool isSpecialAttack;
    public bool tempinvincivility;

    Coroutine temp;

    [Header("BOOM")]
    public int boomCount = 5;
    public GameObject boomPrefab;

    [Header("SpecialAttack")]
    public float currentSpecialAttackGauge;
    public float maxSpecialAttackGauge;

    [Header("timeControl")]
    public float currentTimeControlGauge;
    public float maxTimeControlGauge;
    float t;

    public bool isTimeControl;

    [Header("Follower")]
    public GameObject[] followers;

    public int follwerCnt;


    public bool canMove;

    [Header("AudioClip")]
    public AudioClip getItemClip;
    public AudioClip damageClip;
    public AudioClip playerShotClip;
    public AudioClip specialAttackClip;
    public int AttackLevel
    {
        get { return attackLevel; }
        set { attackLevel = value; 
        if(attackLevel > 4)
                attackLevel = 4;
        }
    }
    public override void Start()
    {
        base.Start();
        Invoke("firstFunc", 0.3f);
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(invincivilityCoroutine());
    }

    public void firstFunc()
    {
        StartCoroutine(firstCoroutine());

    }
    public override void Update()
    {
        base.Update();
        Inputs();
        timeControl();

        
    }

    public override void attack()
    {
        base.attack();

        if (currentAttackDelay > Time.time)
            return;
        else if (!Input.GetMouseButton(0))
        {
            if (!isSpecialAttack)
                return;
        }
        if (!canMove)
            return;
        

        Bullet[] buls = Instantiate(bulletPrefabs[AttackLevel],attackPos.position,Quaternion.identity).GetComponentsInChildren<Bullet>();

        if (!isSpecialAttack)
            SoundManager.Instance.PlaySFX("playerShoot", playerShotClip);

        foreach (Bullet b in buls)
        {
            b.power = power;
            b.speed = bulletSpeed + attackLevel + 1;
            b.dir = Vector2.up;
        }



        currentAttackDelay = Time.time + applyAttackDelay;


    }

    public override void damaged(float power)
    {
        if (cheatInvincivility || damageInvincivility || itemInvincivility)
            return;

        SoundManager.Instance.PlaySFX("PlayerDamage", damageClip);
        currentSpecialAttackGauge += power;
        StartCoroutine(damageCoroutine());
        base.damaged(power);
    }

    public override void die()
    {
        GameSceneManager.Instance.gameEnd(true);
    }


    public override void move()
    {
        if (!canMove)
            return;
        base.move();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        anim.SetInteger("h", (int)h);
        transform.Translate(new Vector2(h, v) * applySpeed * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -4.62f, 4.62f), Mathf.Clamp(transform.position.y, -4.71f, 4.71f));
    }

    public void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            setBoom(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentSpecialAttackGauge >= maxSpecialAttackGauge)
            {
                if(temp != null)
                {
                    StopCoroutine(temp);
                }
                temp = StartCoroutine(specialAttackCoroutine());
                SoundManager.Instance.PlaySFX("specialAttack", specialAttackClip);

            }
        }
    }

    IEnumerator specialAttackCoroutine()
    {
        currentSpecialAttackGauge = 0;
        isSpecialAttack = true;
        if (attackLevel < 5)
            prevAttackLevel = attackLevel;

        attackLevel = 5;

        applyAttackDelay = 0.01f;

        yield return new WaitForSeconds(5);
        applyAttackDelay = attackDelay;
        attackLevel = prevAttackLevel;
        isSpecialAttack = false;
    }

    IEnumerator invincivilityCoroutine()
    {
        SpriteRenderer SR = GetComponentInChildren<SpriteRenderer>();
        Color color = SR.color;
        
        while(Hp > 0)
        {
            yield return null;
            while(tempinvincivility || cheatInvincivility)
            {
                yield return null;

                SR.color = Color.red;

                yield return new WaitForSeconds(0.2f);
            
                SR.color = Color.yellow;

                yield return new WaitForSeconds(0.2f);

                SR.color = Color.green;

                yield return new WaitForSeconds(0.2f);

                SR.color = Color.blue;
            }

            SR.color = new Color(color.r,color.g,color.b,SR.color.a);

        }
    }


    public void setBoom(bool isUse)
    {
        

        if (isUse)
        {
            if (boomCount <= 0)
                return;
            
            Instantiate(boomPrefab, Vector2.zero, Quaternion.identity);
            GameSceneManager.Instance.UIController.boomSetting(isUse);
            boomCount--;
            if (boomCount < 0)
                boomCount = 0;
        }
        else
        {
            if (boomCount < 5)
            {
                boomCount++;
                GameSceneManager.Instance.UIController.boomSetting(isUse);
            }
        }

    }
    IEnumerator damageCoroutine()
    {
        SpriteRenderer SR = GetComponentInChildren<SpriteRenderer>();
        damageInvincivility = true;
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.3f);

        yield return new WaitForSeconds(1.5f);

        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b,1);
        damageInvincivility = false;

    }


    public void timeControl()
    {
        if(GameManager.Instance.isGameControl)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            isTimeControl = !isTimeControl;
        }

        

        if (isTimeControl)
        {
            if (GameSceneManager.Instance.isGameover)
                return;

            t = 0;
            Time.timeScale = 0.3f;
            currentTimeControlGauge -= Time.deltaTime;
            if(currentTimeControlGauge <= 0)
            {
                isTimeControl =false;
                currentTimeControlGauge = 0;
            }
        }
        else
        {
            if (GameSceneManager.Instance.isGameover)
                return;
            Time.timeScale = 1;
            t += Time.deltaTime;

            if (t >= 2)
            {
                if(currentTimeControlGauge < maxTimeControlGauge)
                currentTimeControlGauge += Time.deltaTime;
            }
        }
            


    }

    public void getFollower()
    {
        for (int i = 0; i < follwerCnt; i++)
        {
            followers[i].SetActive(true);
        }
    }


    public IEnumerator firstCoroutine()
    {

        canMove = false;
        transform.position = new Vector2(0, -7.221f);
        float t = Time.time + 1.5f;
        while(t > Time.time)
        {
            yield return null;

            transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, -1.51f, 6 * Time.deltaTime));
        }
        canMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            SoundManager.Instance.PlaySFX("getItem",getItemClip);
            collision.GetComponent<Item>().Use();
        }
        if (collision.CompareTag("RedBloodCell") || collision.CompareTag("WhiteBloodCell"))
        {
            collision.GetComponent<Unit>().damaged(power);
        }
    }


    public IEnumerator bossClearCoroutine()
    {
        canMove = false;

        yield return new WaitForSeconds(4);
        float t = Time.time + 2;
        while(t > Time.time)
        {
            yield return null;
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, 9), 2 * Time.deltaTime);
        }
    }
}
