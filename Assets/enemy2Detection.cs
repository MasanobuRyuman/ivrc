using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Detection : MonoBehaviour
{
    // Start is called before the first frame update
    int hp = 0;
    GameObject player;
    playerStatus ps;
    GameObject gameloop;
    gameloop gl;


    void Start()
    {
        player = GameObject.Find("player");
        ps = player.GetComponent<playerStatus>();

        gameloop = GameObject.Find("gameloop");
        gl = gameloop.GetComponent<gameloop>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
