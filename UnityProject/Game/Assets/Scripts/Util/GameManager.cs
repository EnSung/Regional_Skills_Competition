using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isGameControl;

    public float musicVoloume;
    public float sfxVoloume;

    public bool isMusicMute;
    public bool isSfxMute;
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public List<string> rankName = new List<string>();
    public List<int> rankScore = new List<int>();
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            rankName.Add("Unknown");
            rankScore.Add(0);
        }
    }

}
