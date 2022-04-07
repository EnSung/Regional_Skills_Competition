using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetText : MonoBehaviour
{


    public TextMeshProUGUI text;
    public float tAmount;
    void Start()
    {
        transform.parent = GameSceneManager.Instance.UIController.textCanvas.transform;
        StartCoroutine(cou());
    }

    IEnumerator cou()
    {
        Color c = text.color;
        Vector2 pos = new Vector2(transform.position.x,transform.position.y + 0.5f);
        float t = Time.time + .6f;
        while(t > Time.time)
        {
            yield return null;

            c.a -= tAmount;
            text.color = c;
            transform.position = Vector2.Lerp(transform.position,pos,1 * Time.deltaTime);
        }



        Destroy(gameObject);
    }
}
