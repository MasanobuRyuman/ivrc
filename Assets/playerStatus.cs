using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public enum GESTURETYPE {NONE,MASK,DISTANCE,QUIET};
    [SerializeField]
    private OVRSkeleton _skeleton; //右手、もしくは左手の Bone情報
    private OVRSkeleton righthand;
    public GameObject leftHand;
    public GameObject rightHand;
    float leftFingerAngle;
    float rightFingerAngle;
    bool extend;
    bool fingerAngle;
    bool leftIndexHorizontal;
    bool leftIndexVertical;
    bool rightIndexHorizontal;
    bool rightIndexVertical;
    bool leftIndexStatus;
    bool rightIndexStatus;
    bool indexStatus;
    bool handDistance1;
    bool handDistance2;
    bool handDistance;

    bool leftpattern1;
    bool rightpattern1;
    bool leftpattern2;
    bool rightpattern2;
    bool pattern1;
    bool pattern2;
    bool pattern;

    Vector3 leftThumbTip;
    Vector3 leftThumb2;
    Vector3 leftIndexTip;
    Vector3 leftIndex1;
    Vector3 rightThumbTip;
    Vector3 rightThumb2;
    Vector3 rightIndexTip;
    Vector3 rightIndex1;

    float leftminimum;
    float leftmaxium;
    float rightminimum;
    float rightmaximum;

    float time;

    bool isThumbStraight;
    bool isIndexStraight;
    bool isMiddleStraight;
    bool isRingStraight;
    bool isPinkyStraight;

    bool rightisThumbStraight;
    bool rightisIndexStraight;
    bool rightisMiddleStraight;
    bool rightisRingStraight;
    bool rightisPinkyStraight;

    bool stright;

    bool leftIndexTilt;
    bool leftThumbTilt;
    bool rightIndexTilt;
    bool rightThumbTilt;

    bool leftQuietStatus = false;
    bool rightQuietStatus = false;
    bool quietStatus = false;
    bool msk = false;
    bool leaveStatus = false;

    GameObject gameloop;
    gameloop gl;
    humanGenerate ge;


    enum Looking {
        LEFT,
        RIGHT,
    };

    Looking lookingDirection;


    List<Vector3> rightleave=new List<Vector3>();
    List<Vector3> leftleave=new List<Vector3>();

    GameObject camera;

    void Start()
    {
        _skeleton=leftHand.GetComponent<OVRSkeleton>();
        righthand=rightHand.GetComponent<OVRSkeleton>();
        StartCoroutine("leave");
        gameloop = GameObject.Find("gameloop");
        gl = gameloop.GetComponent<gameloop>();
        ge = gameloop.GetComponent<humanGenerate>();
        camera = GameObject.Find("OVRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(_skeleton);
//        Debug.Log(_skeleton.Bones.Count);
        if( _skeleton.Bones.Count == 0 )
        {
            msk = Input.GetKey(KeyCode.S);
            quietStatus = Input.GetKey(KeyCode.G);
            return;
        }

        leftThumbTip=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        leftThumb2=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        leftIndexTip=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        leftIndex1=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Index1].Transform.position;
        rightThumbTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        rightThumb2=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        rightIndexTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        rightIndex1=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Index1].Transform.position;


        time=Time.deltaTime;
        //masuku();

        //ひとさし指か親指が垂直かどうか
        //左手の人差し指が水平かどうか
        var leftIndexHorizontalValue=Mathf.Abs(leftIndexTip.y-leftIndex1.y);
        if (0.03 >= leftIndexHorizontalValue){
            leftIndexHorizontal=true;
        }else{
            leftIndexHorizontal=false;
        }

        //左手の人差し指が垂直かどうか
        var leftIndexVerticalValue=Mathf.Abs(leftIndexTip.x-leftIndex1.x);
        if (0.03 >= leftIndexVerticalValue){
            leftIndexVertical=true;
        }else{
            leftIndexVertical=false;
        }

        //右手のひとさし指が水平かどうか
        var rightIndexHorizontalValue=Mathf.Abs(rightIndexTip.y-rightIndex1.y);
        if (0.03 >= rightIndexHorizontalValue){
            rightIndexHorizontal=true;
        }else{
            rightIndexHorizontal=false;
        }

        //右手の人差し指が垂直かどうか
        var rightIndexVerticalValue=Mathf.Abs(rightIndexTip.x-rightIndex1.x);
        if (0.06 >= rightIndexVerticalValue){
            rightIndexVertical=true;
        }else{
            rightIndexVertical=false;
        }

        //左手の人差し指が傾いていないか
        if (0.06 > Mathf.Abs(leftIndexTip.z-leftIndex1.z)){
            leftIndexTilt=true;
        }else{
            leftIndexTilt=false;
        }

        if (0.04 > Mathf.Abs(leftThumbTip.z-leftThumb2.z)){
            leftThumbTilt=true;
        }else{
            leftThumbTilt=false;
        }

        if (0.06 > Mathf.Abs(rightIndexTip.z - rightIndex1.z)){
            rightIndexTilt=true;
        }else{
            rightIndexTilt=false;
        }

        if (0.04 > Mathf.Abs(rightThumbTip.z - rightThumb2.z)){
            rightThumbTilt=true;
        }else{
            rightThumbTilt=false;
        }


        //指の曲げ伸ばし左
        isThumbStraight = IsStraight(0.8f,OVRSkeleton.BoneId.Hand_Thumb1,OVRSkeleton.BoneId.Hand_Thumb2,OVRSkeleton.BoneId.Hand_Thumb3,OVRSkeleton.BoneId.Hand_ThumbTip);
        isIndexStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
        isMiddleStraight = IsStraight(0.8f,OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        isRingStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        isPinkyStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);

        //指の曲げ伸ばし右

        rightisThumbStraight = IsStraightright(0.8f,OVRSkeleton.BoneId.Hand_Thumb1,OVRSkeleton.BoneId.Hand_Thumb2,OVRSkeleton.BoneId.Hand_Thumb3,OVRSkeleton.BoneId.Hand_ThumbTip);
        rightisIndexStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
        rightisMiddleStraight = IsStraightright(0.8f,OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        rightisRingStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        rightisPinkyStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);

        masuku();
        quiet();
        look();
        Debug.Log("updateQuietstatus"+quietStatus);
    }




    /// <summary>
    /// 指定した全てのBoneIDが直線状にあるかどうか調べる
    /// </summary>
    /// <param name="threshold">閾値 1に近いほど厳しい</param>
    /// <param name="boneids"></param>
    /// <returns></returns>
    private bool IsStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        if (boneids.Length < 3) return false;   //調べようがない
        Vector3? oldVec = null;
        var dot = 1.0f;
        for (var index = 0; index < boneids.Length-1; index++)
        {
            var v = (_skeleton.Bones[(int)boneids[index+1]].Transform.position - _skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value); //内積の値を総乗していく
            }
            oldVec = v;//ひとつ前の指ベクトル
        }
        return dot >= threshold; //指定したBoneIDの内積の総乗が閾値を超えていたら直線とみなす
    }

    private bool IsStraightright(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        if (boneids.Length < 3) return false;   //調べようがない
        Vector3? oldVec = null;
        var dot = 1.0f;
        for (var index = 0; index < boneids.Length-1; index++)
        {
            var v = (righthand.Bones[(int)boneids[index+1]].Transform.position - righthand.Bones[(int)boneids[index]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value); //内積の値を総乗していく
            }
            oldVec = v;//ひとつ前の指ベクトル
        }
        return dot >= threshold; //指定したBoneIDの内積の総乗が閾値を超えていたら直線とみなす
    }

    public void masuku(){
        var leftExtend=false;
        var rightExtend=false;

        //指の曲げ伸ばしはあっているか
        if(isThumbStraight && isIndexStraight && !isMiddleStraight  && !isRingStraight  && !isPinkyStraight ){ //人差し指だけまっすぐで、その他が曲がっている
            leftExtend=true;
        }else{
            leftExtend=false;
        }
        if(rightisThumbStraight && rightisIndexStraight && !rightisMiddleStraight && !rightisRingStraight && !rightisPinkyStraight){
            rightExtend=true;
        }else{
            rightExtend=false;
        }

        if (leftExtend==true && rightExtend ==true){
            extend=true;
        }else{
            extend=false;
        }



        //左手の人差し指と親指の
        var distance=(leftThumbTip-leftThumb2).normalized;
        var distance2=(leftIndexTip-leftThumb2).normalized;
        //左手の指の角度
        leftFingerAngle=Vector3.Dot(distance,distance2);


　　　　 //右手の人差し指と親指の角度の計算
        var rightdistance=(rightThumbTip-rightThumb2).normalized;
        var rightdistance2=(rightIndexTip-rightThumb2).normalized;
        //右手の指の角度
        rightFingerAngle=Vector3.Dot(rightdistance,rightdistance2);



        if (0.8 >leftFingerAngle && 0.8 > rightFingerAngle ){
            fingerAngle=true;
        }else{
            fingerAngle=false;
        }





        //片方の手がもう片方のなかにいる
        if (leftIndexHorizontal==true && leftThumbTip.y>leftIndexTip.y ||leftIndexVertical==true && leftIndexTip.y > leftThumbTip.y ){
            if(rightThumb2.x>leftThumb2.x && rightThumb2.y > leftThumb2.y){
                leftpattern1=true;
            }else{
                leftpattern1=false;
            }
        }else{
            leftpattern1=false;
        }

        if (rightIndexHorizontal==true && rightIndexTip.y > rightThumbTip.y || rightIndexVertical==true && rightThumbTip.y>rightIndexTip.y){
            if(rightThumb2.x > leftThumb2.x && rightThumb2.y > leftThumb2.y){
                rightpattern1=true;
            }else{
                rightpattern1=false;
            }
        }else{
            rightpattern1=false;
        }

        if(leftIndexHorizontal==true && leftIndexTip.y > leftThumbTip.y || leftIndexVertical==true && leftThumbTip.y > leftIndexTip.y){
            if (rightThumb2.x > leftThumb2.x && leftThumb2.y > rightThumb2.y){
                leftpattern2=true;
            }else{
                leftpattern2=false;
            }
        }else{
            leftpattern2=false;
        }

        if(rightIndexHorizontal==true && rightThumbTip.y > rightIndexTip.y || rightIndexVertical==true && rightIndexTip.y > rightThumbTip.y){
            if(rightThumb2.x > leftThumb2.x && leftThumb2.y > rightThumb2.y){
                rightpattern2=true;
            }else{
                rightpattern2=false;
            }
        }else{
            rightpattern2=false;
        }

        if(leftpattern1 == true && rightpattern1== true){
            pattern1=true;
        }else{
            pattern1=false;
        }
        if(leftpattern2 == true && leftpattern2 == true){
            pattern2=true;
        }else{
            pattern2=false;
        }

        if(pattern1==true || pattern2==true){
            pattern=true;
        }else{
            pattern=false;
        }



        //手は離れすぎていないか
        float xdistance=Mathf.Abs(leftThumb2.x-rightThumb2.x);
        float ydistance=Mathf.Abs(leftThumb2.y-rightThumb2.y);
        //Debug.Log("xdistance"+xdistance);
        //Debug.Log("ydistance"+ydistance);
        //Debug.Log(xdistance/ydistance);
        if (xdistance / ydistance > 1.5 && 3.5 > xdistance / ydistance){
            if(0.3 > ydistance){
                handDistance=true;
            }else{
                handDistance=false;
            }

        }else{
            handDistance=false;
        }



        //マスクができているか
        if (extend==true && fingerAngle==true && pattern==true && handDistance==true && leftIndexTilt==true && leftThumbTilt==true && rightIndexTilt == true && rightThumbTilt == true){
            msk=true;
        }else{
            msk=false;
        }
        //Debug.Log("指の曲げ伸ばし"+extend);
        //Debug.Log("指の角度"+fingerAngle);
        //Debug.Log("片方の手に入っているか"+pattern);
        //Debug.Log("てのきょり"+handDistance);
        //Debug.Log("左手の人差し指の傾き"+ leftIndexTilt);
        //Debug.Log("左手の親指の傾き"+leftThumbTilt);
        //Debug.Log("右手の人差し指の傾き"+rightIndexTilt);
        //Debug.Log("右手の親指のかたむき"+rightThumbTilt);
        //Debug.Log(msk);

    }
    public IEnumerator leave(){
        var rightLeaveJudgment=false;
        var leftLeaveJudgment=false;
        rightleave.Add(rightThumb2);
        leftleave.Add(leftThumb2);


        if (10 >= leftleave.Count){
            if(leftleave.Count == 1){
                leftminimum = leftleave[0].x;
                leftmaxium = leftleave[0].x;
            }else{
                foreach(Vector3 i in leftleave){
                    if (leftminimum > i.x){
                        leftminimum = i.x;
                    }else if (i.x > leftmaxium){
                        leftmaxium = i.x;
                    }
                }
            }
        }else if (leftleave.Count > 10){
            leftleave.RemoveAt(0);
            foreach(Vector3 u in leftleave){
                if (leftleave.IndexOf(u) == 0){
                    leftminimum = u.x;
                    leftmaxium = u.x;
                }
                if (leftminimum > u.x){
                    leftminimum = u.x;
                }else if (u.x > leftmaxium){
                    leftmaxium = u.x;
                }
            }

        }

        if (10 >= rightleave.Count){
            if(rightleave.Count == 1){
                rightminimum = rightleave[0].x;
                rightmaximum = rightleave[0].x;
            }else{
                foreach(Vector3 i in rightleave){
                    if (rightminimum > i.x){
                        rightminimum = i.x;
                    }else if (i.x > rightmaximum){
                        rightmaximum = i.x;
                    }
                }
            }
        }else if (rightleave.Count > 10){
            rightleave.RemoveAt(0);
            foreach(Vector3 u in rightleave){
                if (rightleave.IndexOf(u) == 0){
                    rightminimum=u.x;
                    rightmaximum=u.x;
                }
                if (rightminimum > u.x){
                    rightminimum = u.x;
                }else if (u.x > rightmaximum){
                    rightmaximum = u.x;
                }
            }
        }
        if (leftmaxium - leftminimum > 0.1){
            leftLeaveJudgment=true;
        }else{
            leftLeaveJudgment=false;
        }
        if (rightmaximum - rightminimum > 0.1){
            rightLeaveJudgment=true;
        }else{
            rightLeaveJudgment=false;
        }

        if (leftLeaveJudgment == true && rightLeaveJudgment == true && leftIndexTip.y > leftThumbTip.y && rightIndexTip.y > rightThumbTip.y && 1 > Mathf.Abs(leftThumb2.y-rightThumb2.y) ){
            leaveStatus=true;
        }else{
            leaveStatus=false;
        }


        yield return new WaitForSeconds(0.02f);
        StartCoroutine("leave");
    }

    public void quiet(){
        var leftstright=false;
        var rightstright=false;
        var leftstatus=false;
        var rightstatus=false;

        //指の曲げ伸ばしが正しいか
        if (!isThumbStraight && isIndexStraight && !isMiddleStraight && !isRingStraight && !isPinkyStraight){
            leftstright=true;
        }else{
            leftstright=false;
        }

        if(!rightisThumbStraight && rightisIndexStraight && !rightisMiddleStraight && !rightisRingStraight && !rightisPinkyStraight){
            rightstright = true;
        }else{
            rightstright = false;
        }


        //指の状態は正しいか
        if (leftIndexTilt==true && leftIndexVertical==true){
            leftstatus=true;
        }else{
            leftstatus=false;
        }

        if (rightIndexTilt==true && rightIndexVertical==true){
            rightstatus=true;
        }else{
            rightstatus=false;
        }

        //静かにしなさいジェスチャーがただしいかどうか
        if (leftstright==true && leftstatus==true && leftThumb2.y > rightThumb2.y){
            leftQuietStatus=true;
        }else {
            leftQuietStatus=false;
        }
        if (rightstright==true && rightstatus==true && rightThumb2.y > leftThumb2.y){
            rightQuietStatus=true;
        }else{
            rightQuietStatus = false;
        }

        if (leftQuietStatus == true | rightQuietStatus == true){
            quietStatus = true;
        }else{
            quietStatus = false;
        }

    }

    void look(){
        //Debug.Log("kita");
        //Debug.Log("カメラ位置"+camera.transform.position.x);
        if (leftQuietStatus == true){
            if (camera.transform.position.x > leftIndexTip.x){
                lookingDirection = Looking.LEFT;
            } else {
                lookingDirection = Looking.RIGHT;
            }
        }

        if (rightQuietStatus == true){
            if (camera.transform.position.x > rightIndexTip.x){
                lookingDirection = Looking.LEFT;
            } else {
                lookingDirection = Looking.RIGHT;
            }
        }

        if (msk == true || leaveStatus == true){
            if (leftThumb2.z > rightThumb2.z){
                lookingDirection = Looking.RIGHT;
            } else {
                lookingDirection = Looking.LEFT;
            }
        }
    }


    private void OnTriggerStay(Collider other){
        GESTURETYPE gesture = GESTURETYPE.NONE;
        GameObject en ;
        enemyLife el;
        Debug.Log(lookingDirection);
        if (lookingDirection == Looking.RIGHT && other.transform.position.x > 0 || lookingDirection == Looking.LEFT && other.transform.position.x < 0){
            if (other.tag == "enemy"){

                en = other.gameObject;
                el = en.GetComponent<enemyLife>();

                if ( msk == true ) {
                    gesture=GESTURETYPE.MASK;
                }
                if ( quietStatus == true ) {
                    gesture = GESTURETYPE.QUIET;
                }
                if ( leaveStatus == true ) {
                    gesture = GESTURETYPE.DISTANCE;
                }

                if (el.Warn(gesture) == true){
                    gl.score+=1;
                }
            }
        }
    }

}
