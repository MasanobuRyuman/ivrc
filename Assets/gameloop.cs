using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class gameloop : MonoBehaviour
{
    // Start is called before the first frame update
    public int score=0;
    GameObject hand;
    HandsManager skeleton;


    void Start()
    {
        hand=GameObject.Find("HandsManager");
        skeleton=hand.GetComponent<HandsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            Debug.Log("kikiki");


            Debug.Log(skeleton.RightHand.transform.position);
        }
    }

}
