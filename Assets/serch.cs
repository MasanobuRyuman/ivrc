using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serch : MonoBehaviour
{
    // Start is called before the first frame update
    playerStatus ps;
    GameObject gameloop;
    gameloop gl;

    void Start()
    {
        ps=gameObject.GetComponent<playerStatus>();
        gameloop = GameObject.Find("gameloop");
        gl = gameloop.GetComponent<gameloop>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
