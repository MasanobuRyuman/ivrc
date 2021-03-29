using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class playerControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        bool crouch;
        int handshape;
        GameObject gameloop;
        gameloop gl;

        private void Start()
        {
            // get the transform of the main camera
            gameloop = GameObject.Find("gameloop");
            gl = gameloop.GetComponent<gameloop>();


            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                //m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            //crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character

            m_Move = new Vector3(0.0f,0.0f,0.7f);
            //Debug.Log(m_Move);
            m_Character.Move(m_Move, crouch, m_Jump);


        }

        private void OnTriggerStay(Collider other){
            if(other.tag == "masuku"){
                //Debug.Log("enemy1");
                if(caution()==1){
                    Debug.Log("masuku");
                    gl.score += 1;
                }
            }
            if(other.tag == "release"){
                //Debug.Log("enemy2");
                if(caution()==2){
                    Debug.Log("release");
                    gl.score += 1;
                }
            }
            if(other.tag == "talk"){
                //Debug.Log("enemy3");
                if(caution()==3){
                    Debug.Log("talk");
                    gl.score += 1;
                }
            }
            if(other.tag == "enemy4"){
                //Debug.Log("enemy4");
            }
        }

        public int caution(){
            if (Input.GetKeyUp(KeyCode.A)){
                //Debug.Log("押している");
                handshape=1;
            }else if (Input.GetKeyUp(KeyCode.S)){
                handshape=2;
            }else if (Input.GetKeyUp(KeyCode.D)){
                handshape=3;
            }else {
                handshape=0;
            }


            return handshape;
        }


    }
}
