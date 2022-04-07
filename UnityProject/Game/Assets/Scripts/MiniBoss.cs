using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Two_Boss
{
    public static int bossCnt = 2;

    public static int numbering = 0;


    public override void Start()
    {
        bossCnt = 2;
        base.Start();
        bossName = bossName + (numbering + 1).ToString();
        numbering++;
        GameSceneManager.Instance.UIController.bossUIObject.SetActive(true);

    }

    public override void Update()
    {
        move();
        spinObj.transform.Rotate(new Vector3(0, 0, rotSpeed * 100 * Time.deltaTime));
    }

    public override void damaged(float power)
    {
        base.damaged(power);
        UI_Update();
    }


    public override void UI_Update()
    {
        GameSceneManager.Instance.UIController.bossUIObject.SetActive(true);

        GameSceneManager.Instance.UIController.bossHpGauge.maxValue = MaxHp;
        GameSceneManager.Instance.UIController.bossNameText.text = bossName;
        GameSceneManager.Instance.UIController.bossHpGauge.value = Hp;
        GameSceneManager.Instance.UIController.bossHpPercent.text = Mathf.Round((Hp / MaxHp * 100)).ToString() + "%";
    }

    public override void die()
    {
        if (!isDie)
        {
            isDie = true;
            bossCnt--;

            if (bossCnt == 0)
            {
                GameSceneManager.Instance.score += getScore;
                GameSceneManager.Instance.UIController.bossUIObject.SetActive(false);
                GameSceneManager.Instance.gameEnd(false);
                Destroy(gameObject);
            }
            else
            {
                GameSceneManager.Instance.score += getScore;
                
                Destroy(gameObject);
            }

        }
    }
}
