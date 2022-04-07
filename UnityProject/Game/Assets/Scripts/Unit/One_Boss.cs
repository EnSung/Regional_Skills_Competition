using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss : Enemy
{

    public string bossName;
    public float rotSpeed;

    public GameObject spinObj;

    Coroutine atk;

    
    public override void Start()
    {
        base.Start();

        Invoke("attack", 4);

        UI_setting();
        GameSceneManager.Instance.UIController.bossUIObject.SetActive(true);
    }

    public override void Update()
    {
        move();
        spinObj.transform.Rotate(new Vector3(0, 0, rotSpeed * 100 * Time.deltaTime));
        UI_Update();
        if (isDie)
        {

            if(atk != null)
                StopCoroutine(atk);
            
            
            
            Collider2D[] colliders = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(10, 10), 0);

            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.CompareTag("Bullet"))
                {
                    Destroy(col.gameObject);
                }

            }
        }
    }

    public void UI_setting()
    {
        GameSceneManager.Instance.UIController.bossHpGauge.maxValue = MaxHp;
        GameSceneManager.Instance.UIController.bossNameText.text = bossName;
    }

    public void UI_Update()
    {
        GameSceneManager.Instance.UIController.bossHpGauge.value = Mathf.Lerp(GameSceneManager.Instance.UIController.bossHpGauge.value, Hp,6 * Time.deltaTime);
        GameSceneManager.Instance.UIController.bossHpPercent.text =  Mathf.Round((Hp / MaxHp * 100)).ToString() + "%";
    }
    public override void move()
    {
        if (transform.position.y > 3.49f)
            transform.Translate(Vector2.down * applySpeed * Time.deltaTime);
    }
    public override void attack()
    {
        if (isDie)
            return;


        int ran = Random.Range(0, 4);

        switch (ran)
        {
            case 0:
                atk = StartCoroutine(spinShot());
                break;
            case 1:
                atk = StartCoroutine(circleShot());
                break;
            case 2:
                atk = StartCoroutine(normalShot());
                break;
            case 3:
                atk = StartCoroutine(spawnShot());
                break;
            default:
                break;
        }
    }


    IEnumerator spinShot()
    {
        float t = Time.time + 3;

        while(t > Time.time)
        {
            yield return null;
            Bullet bul = Instantiate(bulletPrefab, transform.position, spinObj.transform.rotation).GetComponent<Bullet>();

            bul.speed = bulletSpeed;
            bul.dir = Vector2.right;
            bul.power = power;

            yield return new WaitForSeconds(0.004f);

        }

        Invoke("attack", 2);
    }


    IEnumerator circleShot()
    {


        for (int i = 0; i < 20; i++)
        {
            float ran = Random.Range(-6f, 6f);
            for (int j = 0; j < 360; j+= 15)
            {
                yield return null;
                Bullet bul = Instantiate(bulletPrefab, transform.position,transform.rotation).GetComponent<Bullet>();
                bul.transform.rotation = Quaternion.Euler(0, 0, j + ran);
                bul.speed = bulletSpeed;
                bul.dir = Vector2.right;
                bul.power = power;
            }

            yield return new WaitForSeconds(0.1f);
        }
           



        Invoke("attack", 2);
    }

    IEnumerator normalShot()
    {

        for (int i = 0; i < 50; i++)
        {
            Bullet bul = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
            bul.speed = bulletSpeed;
            bul.dir = (GameSceneManager.Instance.player.transform.position + new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f)) - transform.position).normalized;
            bul.power = power;

            yield return new WaitForSeconds(0.2f);
        }
      




        Invoke("attack", 2);
    }

    IEnumerator spawnShot()
    {
        int ran = Random.Range(1, 3);
        for (int i = 0; i < ran; i++)
        {
            yield return null;
            Instantiate(GameSceneManager.Instance.enemyPrefabs[Random.Range(0, GameSceneManager.Instance.enemyPrefabs.Length)], transform.position + new Vector3(Random.Range(-2.1f, 2.1f), -1.9f), Quaternion.identity);

        }


        Invoke("attack", 2);
    }
    public override void die()
    {
        isDie = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Instantiate(GameSceneManager.Instance.dieEffectPrefab, transform.position, transform.rotation);
        GetText t = Instantiate(GameSceneManager.Instance.getTextPrefab, transform.position + new Vector3(0, 0.5f), transform.rotation).GetComponent<GetText>();
        t.text.text = "+" + getScore.ToString();
        SoundManager.Instance.PlaySFX("enemyDie", dieClip);
        StartCoroutine(GameSceneManager.Instance.player.bossClearCoroutine());
        Invoke("dieInvo", 6.1f);
    }

    public void dieInvo()
    {

        GameSceneManager.Instance.changeStage(1, false);
        Instantiate(GameSceneManager.Instance.dieEffectPrefab, transform.position, transform.rotation);


        int ran = Random.Range(0, 1001);

        if (ran >= 850)
            Instantiate(GameSceneManager.Instance.itemPrefabs[Random.Range(0, GameSceneManager.Instance.itemPrefabs.Length)], transform.position, transform.rotation);
        GameSceneManager.Instance.score += getScore;



        Destroy(gameObject);

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            GameSceneManager.Instance.UIController.bossUIObject.SetActive(false);
        }
        catch
        {

        }
    }

}
