using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class playeMove : MonoBehaviour
{
    // Start is called before the first frame update
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    void Start()
    {
        m_Character = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        // read inputs
        //crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        Vector3 m_Move;
        m_Move = new Vector3(0.0f,0.0f,0.5f);
        //Debug.Log(m_Move);
        m_Character.Move(m_Move, false, false);


    }
}
