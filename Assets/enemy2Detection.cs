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
    private void OnTriggerStay(Collider other){
        if (transform.tag == "release"){
            Debug.Log("leaveStatus"+ps.leaveStatus);
            if(other.tag == "Player"){
                //Debug.Log("enemy1");
                if (ps.leaveStatus=="True"){
                    if (hp==0){
                        gl.score+=1;
                        hp+=1;
                    }
                }
            }
        }
    }
}
