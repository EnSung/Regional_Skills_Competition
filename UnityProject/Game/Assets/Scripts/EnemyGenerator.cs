using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    float germ = 0.4f;
    float bacteria = 0.6f;
    float cancercell = 0.8f;
    float virus = 1f;
    void Start()
    {
        float ran = Random.Range(0f, 1.1f);

        if(ran <= germ)
        {
            Instantiate(GameSceneManager.Instance.enemyPrefabs[0],transform);
        }
        else if(ran <= bacteria)
        {
            Instantiate(GameSceneManager.Instance.enemyPrefabs[1], transform);
        }
        else if(ran <= cancercell)
        {
            Instantiate(GameSceneManager.Instance.enemyPrefabs[2], transform);
        }
        else
        {
            Instantiate(GameSceneManager.Instance.enemyPrefabs[3], transform);
        }
    }

}
