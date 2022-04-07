using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Boss : Enemy
{

    public string bossName;
    public float rotSpeed;

    public GameObject spinObj;

    public Vector2 dir;

    public override void Start()
    {
        base.Start();
        UI_setting();
        GameSceneManager.Instance.UIController.bossUIObject.SetActive(true);
        Invoke("attack", 5);
        StartCoroutine(moveCoroutine());
    }

    public override void Update()
    {
        move();
        spinObj.transform.Rotate(new Vector3(0, 0, rotSpeed * 100 * Time.deltaTime));
        UI_Update();
    }

    public void UI_setting()
    {
        GameSceneManager.Instance.UIController.bossHpGauge.maxValue = MaxHp;
        GameSceneManager.Instance.UIController.bossNameText.text = bossName;
    }

    public virtual void UI_Update()
    {
        GameSceneManager.Instance.UIController.bossHpGauge.value = Mathf.Lerp(GameSceneManager.Instance.UIController.bossHpGauge.value, Hp, 6 * Time.deltaTime);
        GameSceneManager.Instance.UIController.bossHpPercent.text = Mathf.Round((Hp / MaxHp * 100)).ToString() + "%";
    }
    public override void move()
    {
        if (transform.position.y > 3.49f)
            transform.Translate(Vector2.down * applySpeed * Time.deltaTime);


        transform.Translate(dir * applySpeed * Time.deltaTime);


        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3.45f, 3.45f), transform.position.y);
    }
    public override void attack()
    {

        int ran = Random.Range(0, 7);
        switch (ran)
        {
            case 0:
                StartCoroutine(spinShot());
                break;
            case 1:
                StartCoroutine(circleShot());
                break;
            case 2:
                StartCoroutine(normalShot());
                break;
            case 3:
                StartCoroutine(spawnShot());
                break;
            case 4:
                StartCoroutine(circleAndFollowBugShot());
                break;
            case 5:
                StartCoroutine(spinShot_Two());
                break;
            case 6:
                StartCoroutine(circleAndFollowShot());
                break;
            default:
                break;
        }

    }


    IEnumerator spinShot()
    {
        float t = Time.time + 3;
        rotSpeed = 41;

        while (t > Time.time)
        {
            yield return null;
            Bullet bul = Instantiate(bulletPrefab, transform.position, spinObj.transform.rotation).GetComponent<Bullet>();

            bul.speed = bulletSpeed;
            bul.dir = Vector2.right;
            bul.power = power;

            yield return new WaitForSeconds(0.003f);

        }

        Invoke("attack", 2);
    }

    IEnumerator spinShot_Two()
    {
        float t = Time.time + 3;
        int cnt = 0;
        rotSpeed = 21;
        while (t > Time.time)
        {
            if(cnt >= 3)
            {
                yield return new WaitForSeconds(0.006f);
                cnt = 0;
            }
            yield return null;
            Bullet bul = Instantiate(bulletPrefab, transform.position, spinObj.transform.rotation).GetComponent<Bullet>();

            bul.speed = bulletSpeed;
            bul.dir = Vector2.right;
            bul.power = power;

            cnt++;
        }

        Invoke("attack", 2);
    }
    IEnumerator circleShot()
    {


        for (int i = 0; i < 20; i++)
        {
            float ran = Random.Range(-6f, 6f);
            for (int j = 0; j < 360; j += 15)
            {
                yield return null;
                Bullet bul = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
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
            bul.dir = (GameSceneManager.Instance.player.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)) - transform.position).normalized;
            bul.power = power;

            yield return new WaitForSeconds(0.19f);
        }





        Invoke("attack", 2);
    }


    IEnumerator circleAndFollowBugShot()
    {
        for (int i = 0; i < 20; i++)
        {
            List<Bullet> list = new List<Bullet>();
            float ran = Random.Range(-6f, 6f);
            for (int j = 0; j < 360; j += 15)
            {
                yield return null;
                Bullet bul = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
                bul.transform.rotation = Quaternion.Euler(0, 0, j + ran);
                bul.speed = bulletSpeed;
                bul.dir = Vector2.right;
                bul.power = power;

                list.Add(bul);


            }

            yield return new WaitForSeconds(0.4f);

            try
            {
                Bullet[] arr = list.ToArray();

                foreach (Bullet a in arr)
                {
                    a.dir = (GameSceneManager.Instance.player.transform.position - a.transform.position).normalized;
                }

                list.Clear();
            }
            catch
            {

            }
            

        }


        Invoke("attack", 2);
    }


    IEnumerator circleAndFollowShot()
    {
        List<Bullet> list = new List<Bullet>();

        for (int i = 0; i < 20; i++)
        {
            float ran = Random.Range(-6f, 6f);
            for (int j = 0; j < 360; j += 15)
            {
                yield return null;
                Bullet bul = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
                bul.transform.rotation = Quaternion.Euler(0, 0, j + ran);
                bul.speed = bulletSpeed;
                bul.dir = Vector2.right;
                bul.power = power;

                list.Add(bul);


            }

            yield return new WaitForSeconds(0.6f);

            try
            {
                Bullet[] arr = list.ToArray();

                foreach (Bullet a in arr)
                {
                    a.transform.rotation = Quaternion.identity;
                    a.dir = (GameSceneManager.Instance.player.transform.position - a.transform.position).normalized;
                }

            }
            catch
            {

            }

            list.Clear();
            yield return new WaitForSeconds(0.4f);

        }


        Invoke("attack", 2);
    }
    IEnumerator spawnShot()
    {
        int ran = Random.Range(1, 6);
        for (int i = 0; i < ran; i++)
        {
            yield return null;
            Instantiate(GameSceneManager.Instance.enemyPrefabs[Random.Range(0, GameSceneManager.Instance.enemyPrefabs.Length)], transform.position + new Vector3(Random.Range(-2.1f, 2.1f), -1.9f), Quaternion.identity);

        }


        Invoke("attack", 2);
    }

    public override void die()
    {
        if (!isDie)
        {
            Instantiate(GameSceneManager.Instance.bossPrefabs[2], transform.position + new Vector3(-2, 0), Quaternion.identity);
            Instantiate(GameSceneManager.Instance.bossPrefabs[2], transform.position + new Vector3(2, 0), Quaternion.identity);
            base.die();
        }
    }


    IEnumerator moveCoroutine()
    {
        while(Hp > 0)
        {
            yield return null;

            int ran = Random.Range(-1,2);

            dir = new Vector2(ran, 0);

            yield return new WaitForSeconds(0.5f);
        }
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
