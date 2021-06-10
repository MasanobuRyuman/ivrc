using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public GameObject[] enemies;
    GameObject enemy2;
    GameObject enemy3;
    GameObject enemy4;

    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)){
            Debug.Log("okokok");
            generate();
        }
    }

    public void generate(){
        Debug.Log("kita");
        int value = Random.Range(0, 4 + 1);
        int rightOrLeft = Random.Range(0,1+1);
        float direction ;
        if (rightOrLeft == 0){
            direction = 5.0f;
        }else{
            direction = -5.0f;
        }
        if (value == 1){
            Instantiate(enemies[value], new Vector3( player.transform.position.x + direction , 0f, player.transform.position.z + 30.0f), Quaternion.identity);
            Instantiate(enemies[value], new Vector3( player.transform.position.x + direction +1 , 0f, player.transform.position.z + 30.0f), Quaternion.identity);
            Instantiate(enemies[value], new Vector3( player.transform.position.x + direction -1 , 0f, player.transform.position.z + 30.0f), Quaternion.identity);
        }else {
            Instantiate(enemies[value], new Vector3( player.transform.position.x + direction , 0f, player.transform.position.z + 30.0f), Quaternion.identity);
        }
    }
}
