using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : UIManager
{

    public GameObject fadeObjct;
    [Header("HPGauge")]

    public Slider hpSlider;
    public Text hpPercent;

    public Slider painSlider;
    public Text painPercent;

    [Header("Score")]
    public Text scoreText;

    [Header("resultPanel")]
    public GameObject resultPanel;
    public Text resultScoreText;
    public Text resultTitleText;

    public GameObject nicknamePanel;
    public InputField nicknameField;
    public Text nicknameInputTitle;
    [Header("rank")]
    public GameObject[] rankObj;

    [Header("Boom")]
    public GameObject boomLayout;
    public GameObject boomUIPrefab;

    [Header("SpecialAttack")]
    public Slider specialAttackGauge;

    [Header("Cheat")]
    public Slider cheatHPGauge;
    public Slider cheatPainGauge;

    [Header("TimeControl")]
    public Slider TimeControlGauge;

    [Header("Boss UI")]
    public GameObject bossUIObject;
    public Slider bossHpGauge;
    public Text bossNameText;
    public Text bossHpPercent;

    [Header("WarningPanel")]
    public GameObject warnningPanel;

    [Header("Get")]
    public Canvas textCanvas;
    private void Start()
    {
        UI_setting();
    }

    public void Update()
    {
        UI_Update();
    }


    public void UI_setting()
    {
        hpSlider.maxValue = GameSceneManager.Instance.player.MaxHp;
        painSlider.maxValue = GameSceneManager.Instance.maxStageHp;
        specialAttackGauge.maxValue = GameSceneManager.Instance.player.maxSpecialAttackGauge;

        cheatHPGauge.maxValue = GameSceneManager.Instance.player.MaxHp;
        cheatHPGauge.value = cheatHPGauge.maxValue;
        cheatPainGauge.maxValue = GameSceneManager.Instance.maxStageHp;
        cheatPainGauge.value = 0;

        TimeControlGauge.maxValue = GameSceneManager.Instance.player.maxTimeControlGauge;

    }

    public void UI_Update()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, GameSceneManager.Instance.player.Hp, 6 * Time.deltaTime);
        painSlider.value = Mathf.Lerp(painSlider.value,GameSceneManager.Instance.maxStageHp - GameSceneManager.Instance.StageHp, 6 * Time.deltaTime);


        hpPercent.text = (GameSceneManager.Instance.player.Hp / GameSceneManager.Instance.player.MaxHp * 100).ToString() + "%";
        painPercent.text = (100 - (GameSceneManager.Instance.StageHp / GameSceneManager.Instance.maxStageHp * 100)).ToString() + "%";

        specialAttackGauge.value = Mathf.Lerp(specialAttackGauge.value, GameSceneManager.Instance.player.currentSpecialAttackGauge, 5 * Time.deltaTime);


        scoreText.text = GameSceneManager.Instance.score.ToString();


        TimeControlGauge.value = Mathf.Lerp(TimeControlGauge.value, GameSceneManager.Instance.player.currentTimeControlGauge, 7 * Time.deltaTime);
    }



    public void rankUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            Text[] objs = rankObj[i].GetComponentsInChildren<Text>();

            objs[1].text = GameManager.Instance.rankName[i];
            objs[2].text = GameManager.Instance.rankScore[i].ToString();
        }

        resultScoreText.text = GameSceneManager.Instance.score.ToString();
    }

    public void enterNickname()
    {
        if (nicknameField.text.Length == 0)
            return;

        SoundManager.Instance.PlaySFX("BTN", BtnClickClip);
         GameManager.Instance.rankName[4] = nicknameField.text;


        for (int i = 4; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (GameManager.Instance.rankScore[j] <= GameManager.Instance.rankScore[j + 1])
                {
                    int temp1 = GameManager.Instance.rankScore[j];
                    GameManager.Instance.rankScore[j] = GameManager.Instance.rankScore[j + 1];
                    GameManager.Instance.rankScore[j + 1] = temp1;

                    string temp2 = GameManager.Instance.rankName[j];
                    GameManager.Instance.rankName[j] = GameManager.Instance.rankName[j + 1];
                    GameManager.Instance.rankName[j + 1] = temp2;
                }
            }
        }
        rankUpdate();

        Time.timeScale = 1;
        nicknamePanel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("T");

        Invoke("nameInvo",0.5f);
        

    }

    public void nameInvo()
    {
        nicknamePanel.SetActive(false);

        Time.timeScale = 0;
    }
    public void boomSetting(bool Use)
    {
        if (Use)
        {
            Destroy(boomLayout.transform.GetChild(0).gameObject);

        }
        else
        {
            Instantiate(boomUIPrefab, boomLayout.transform).transform.localPosition = Vector2.zero;

        }
    }


    public void cheatPainChange()
    {
        GameSceneManager.Instance.StageHp = GameSceneManager.Instance.maxStageHp - cheatPainGauge.value;
    }

    public void cheatHPChange()
    {
        GameSceneManager.Instance.player.Hp = cheatHPGauge.value;
    }

    public void cheatHpInit()
    {
        cheatHPGauge.value = GameSceneManager.Instance.player.Hp;
        cheatPainGauge.value = GameSceneManager.Instance.maxStageHp- GameSceneManager.Instance.StageHp;
    }
}
