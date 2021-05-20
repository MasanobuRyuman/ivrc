using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLife : MonoBehaviour
{
    // Start is called before the first frame update
    public playerStatus.GESTURETYPE weakpoint ;
    private int hp=1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Warn(playerStatus.GESTURETYPE g)
    {
        if (weakpoint == playerStatus.GESTURETYPE.NONE){
            return false;
        }
        if ( g == weakpoint)
        {
            if (hp > 0){
                Debug.Log("kita");
                hp -= 1;
                return true;
            }

        }
        return false;
    }
}
