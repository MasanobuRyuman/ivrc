using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class search : MonoBehaviour
{
    // Start is called before the first frame update
    playerStatus ps;

    void Start()
    {
        ps=gameObject.GetComponent<playerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other){
        if(other.tag == "masuku"){
            //Debug.Log("enemy1");

        }
        if(other.tag == "release"){
            //Debug.Log("enemy2");

        }
        if(other.tag == "talk"){
            //Debug.Log("enemy3");
            
        }
        if(other.tag == "enemy4"){
            //Debug.Log("enemy4");
        }
    }
}
