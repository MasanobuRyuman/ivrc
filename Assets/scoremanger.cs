using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject score_object = null; // Textオブジェクト
    GameObject gameloop;
    gameloop gl;
    void Start()
    {
        gameloop=GameObject.Find("gameloop");
        gl=gameloop.GetComponent<gameloop>();
    }

    // Update is called once per frame
    void Update()
    {
        Text score_text = score_object.GetComponent<Text> ();
        // テキストの表示を入れ替える
        score_text.text = "socre"+gl.score;
    }
}
