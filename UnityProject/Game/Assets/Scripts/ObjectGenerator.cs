using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{

    public enum generType
    {
        Enemy,
        RedBloodCell,
        WhiteBloodCell,
    }

    public generType type;

    public float minTime;
    public float maxTime;

    float spawnTime;
    float t;
    void Start()
    {
        if (type == generType.Enemy)
            spawnTime = 0;
        else
            spawnTime = Random.Range(minTime,maxTime + 0.1f);

    }

    void Update()
    {

        if (GameSceneManager.Instance.isBoss)
            return;


        t += Time.deltaTime;
        
        if(t >= spawnTime)
        {
            spawn();
            spawnTime = Random.Range(minTime,maxTime + 0.1f);
            t = 0;
        }
    }

    public void spawn()
    {
        switch (type)
        {
            case generType.Enemy:

                int r = Random.Range(2 + GameSceneManager.Instance.currentStage * 2, 6 * (GameSceneManager.Instance.currentStage + 1));
                for (int i = 0; i < r; i++)
                {
                    Instantiate(GameSceneManager.Instance.enemySpawner, new Vector2(Random.Range(-4f, 4f), Random.Range(6.1f,9f)), Quaternion.identity);
                }
                break;
            case generType.RedBloodCell:
                Instantiate(GameSceneManager.Instance.redBloodCellPrefab, new Vector2(Random.Range(-4f, 4f), 8), Quaternion.identity);
                break;
            case generType.WhiteBloodCell:
                Instantiate(GameSceneManager.Instance.whiteBloodCellPrefab, new Vector2(Random.Range(-4f, 4f), 8), Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
