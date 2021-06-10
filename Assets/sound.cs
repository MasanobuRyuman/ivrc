using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour {

    public AudioClip sound1;
    AudioSource audioSource;

    void Start () {
       //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {

    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Debug.Log("player");
            audioSource.PlayOneShot(sound1);
        }

    }

}
