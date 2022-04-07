using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : Singleton<GameSceneManager>
{

    [SerializeField] private float stageHp;
    public float maxStageHp;

    public int score;
    
    public int currentStage;
    public GameObject[] stages;


    public bool isGameover;

    [Header("Boss")]
    public bool isBoss;
    public float currentBossTime;
    public float BossTime;

    public List<GameObject> monsterList = new List<GameObject>();
    [Header("Reference")]
    public Player player;
    public GameSceneUIManager UIController;
    [Header("Prefab")]
    public GameObject[] itemPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject  redBloodCellPrefab;
    public GameObject  whiteBloodCellPrefab;

    public GameObject dieEffectPrefab;
    public GameObject enemySpawner;
    public GameObject bulletMudPrefab;

    public GameObject getTextPrefab;

    [Header("Audio")]
    public AudioClip stageClip;
    public AudioClip bossClip; 
    public AudioClip clearClip; 
    public AudioClip gameoverClip; 
    public float StageHp
    {
        get { return stageHp; }
        set { stageHp = value; 
        if(stageHp > maxStageHp)
                stageHp = maxStageHp;
        }
    }



    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").GetComponentInChildren<Player>();

    }
    void Start()
    {
        stageHp = maxStageHp;
        SoundManager.Instance.backgroundSoundPlay(stageClip, false);

    }

    void Update()
    {
        inputCheat();
        spawnBoss();
        gameover();
    }


    public void spawnBoss()
    {
        currentBossTime += Time.deltaTime;
        if(currentBossTime >= BossTime && !isBoss)
        {
            isBoss = true;
            Instantiate(bossPrefabs[currentStage], new Vector2(0, 8), Quaternion.identity);
            
            SoundManager.Instance.backgroundSoundPlay(bossClip,false);

        }

    }
    public void damaged(float power)
    {
        stageHp -= power;
        player.currentSpecialAttackGauge += power;
    }
    public void changeStage(int num, bool isCheat)
    {

        Collider2D[] colliders = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(10, 10), 0);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Bullet"))
            {
                Destroy(col.gameObject);
            }

        }
        UIController.fadeObjct.GetComponentInChildren<Text>().text = "Stage " +(num +1).ToString();
        UIController.fadeObjct.SetActive(false);
        UIController.fadeObjct.SetActive(true);
        StartCoroutine(player.firstCoroutine());
        if (!isCheat)
        {
            addLastScore();
        }

        GameObject[] arr = monsterList.ToArray();

            foreach (GameObject OBJ in arr)
            {
                Destroy(OBJ);
            }

        if (num > 1)
            num = 1;

        switch (num)
        {


            case 0:
                stageHp = maxStageHp * 0.9f;
                stages[0].SetActive(true);
                stages[1].SetActive(false);
                break;
            case 1:
                stageHp = maxStageHp * 0.7f;
                stages[0].SetActive(false);
                stages[1].SetActive(true);
                break;
            default:
                break;
        }

        currentStage = num;

        player.Hp = player.MaxHp;


        player.boomCount = 3;

        for (int i = 0; i < UIController.boomLayout.transform.childCount; i++)
        {
            Destroy(UIController.boomLayout.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            UIController.boomSetting(false);
        }

        player.currentSpecialAttackGauge = 0;
        currentBossTime = -2f;

        isBoss = false;

        SoundManager.Instance.backgroundSoundPlay(stageClip,false);

    }
    public void addLastScore()
    {
        score += (int)(player.Hp * 10);
        score += (int)(stageHp * 100);
    }
    void gameover()
    {
        if (!isGameover)
        {

            if (stageHp < maxStageHp * 0.3f || player.Hp < player.MaxHp * 0.3f)
            {
                UIController.warnningPanel.SetActive(true);
            }
            else
            {
                UIController.warnningPanel.SetActive(false);
            }
            if (stageHp <= 0)
                gameEnd(true);
        }
    }
    public void gameEnd(bool isGameovers)
    {
        if(UIController.warnningPanel != null)
            Destroy(UIController.warnningPanel);

        if (isGameover)
            return;
        isGameover = true;



        SoundManager.Instance.backgroundSoundPlay(null, true);
        Time.timeScale = 0;
        if (!isGameovers)
        {
            UIController.resultTitleText.text = "게임 클리어!";
            UIController.nicknameInputTitle.text = "게임 클리어!";
            SoundManager.Instance.PlaySFX("clear", clearClip);
            addLastScore();
        }
        else
        {
            UIController.resultTitleText.color = Color.red;
            UIController.nicknameInputTitle.color = Color.red;
            UIController.resultTitleText.text = "게임 오버";
            UIController.nicknameInputTitle.text = "게임 오버";

            SoundManager.Instance.PlaySFX("gameover", gameoverClip);
        }
        

        Time.timeScale = 0;
        UIController.resultPanel.SetActive(true);
        if(score > GameManager.Instance.rankScore[4])
        {
            GameManager.Instance.rankScore[4] = score;
            UIController.nicknamePanel.SetActive(true);
        }
        
        UIController.rankUpdate();


    }
    public void inputCheat()
    {
        if (isGameover)
            return;
        else if (GameManager.Instance.isGameControl)
            return;


        if (Input.GetKeyDown(KeyCode.U))
            player.AttackLevel++;
        else if (Input.GetKeyDown(KeyCode.I))
            player.cheatInvincivility = !player.cheatInvincivility;
        else if (Input.GetKeyDown(KeyCode.O))
            Instantiate(whiteBloodCellPrefab, new Vector2(0, 8), Quaternion.identity);
        else if (Input.GetKeyDown(KeyCode.P))
            Instantiate(redBloodCellPrefab, new Vector2(Random.Range(-4f, 4f), 8), Quaternion.identity);
        else if (Input.GetKeyDown(KeyCode.J))
        {
            UIController.cheatPainGauge.transform.parent.gameObject.SetActive(!UIController.cheatPainGauge.transform.parent.gameObject.activeSelf);
            if (UIController.cheatHPGauge.transform.parent.gameObject.activeSelf)
            {
                UIController.cheatHpInit();
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject[] arr = monsterList.ToArray();

            foreach (GameObject OBJ in arr)
            {
                OBJ.GetComponent<Unit>().die();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeStage(0, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeStage(1, true);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            player.currentSpecialAttackGauge = player.maxSpecialAttackGauge;
        }

    }
}
