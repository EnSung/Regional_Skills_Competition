using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneUIManager : UIManager
{

    public GameObject[] rankObj;
    [Header("Help")]

    public GameObject[] pages;
    public GameObject prevBtn;
    public GameObject nextBtn;
    public GameObject startBtn;
    public int currentPage;

    public int minPage = 0;
    public int maxPage;

    void Start()
    {
        
    }

    void Update()
    {
        if (currentPage == minPage)
        {
            prevBtn.SetActive(false);
        }
        else prevBtn.SetActive(true);

        if (currentPage == maxPage)
        {
            nextBtn.SetActive(false);
            startBtn.SetActive(true);
        }
        else
        {
            nextBtn.SetActive(true);
            startBtn.SetActive(false);
        }
    }


    public void prev_page()
    {
        SoundManager.Instance.PlaySFX("BTn", BtnClickClip);
        pages[currentPage--].SetActive(false);
        pages[currentPage].SetActive(true);
    }

    public void next_page()
    {
        SoundManager.Instance.PlaySFX("BTn", BtnClickClip);
        pages[currentPage++].SetActive(false);
        pages[currentPage].SetActive(true);
    }
    public void rankUpdate()
    {

        for (int i = 0; i < 5; i++)
        {
            Text[] objs = rankObj[i].GetComponentsInChildren<Text>();

            objs[1].text = GameManager.Instance.rankName[i];
            objs[2].text = GameManager.Instance.rankScore[i].ToString();
        }

    }
    public void gameQuit()
    {
        Application.Quit();
    }

    public void updateCurrentPage()
    {
        currentPage = 0;
    }

    public void openHelp()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[currentPage].SetActive(true);
    }
}
