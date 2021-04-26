using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private OVRSkeleton _skeleton; //右手、もしくは左手の Bone情報
    private OVRSkeleton righthand;
    public GameObject leftHand;
    public GameObject rightHand;
    float leftFingerAngle;
    float rightFingerAngle;
    string extend;
    string fingerAngle;
    string leftIndexHorizontal;
    string leftIndexVertical;
    string rightIndexHorizontal;
    string rightIndexVertical;
    string leftIndexStatus;
    string rightIndexStatus;
    string indexStatus;
    string handDistance1;
    string handDistance2;
    string handDistance;

    string leftpattern1;
    string rightpattern1;
    string leftpattern2;
    string rightpattern2;
    string pattern1;
    string pattern2;
    string pattern;
    string msk;
    string leaveStatus;

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

    string stright;

    string leftIndexTilt;
    string leftThumbTilt;
    string rightIndexTilt;
    string rightThumbTilt;

    string quietStatus;


    List<Vector3> rightleave=new List<Vector3>();
    List<Vector3> leftleave=new List<Vector3>();

    void Start()
    {
        _skeleton=leftHand.GetComponent<OVRSkeleton>();
        righthand=rightHand.GetComponent<OVRSkeleton>();
        StartCoroutine("leave");
    }

    // Update is called once per frame
    void Update()
    {
        leftThumbTip=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        leftThumb2=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        leftIndexTip=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        leftIndex1=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Index1].Transform.position;
        rightThumbTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        rightThumb2=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        rightIndexTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        rightIndex1=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Index1].Transform.position;


        time=Time.deltaTime;
        if (Input.GetKey(KeyCode.A)){
            masuku();
        }

        if (Input.GetKey(KeyCode.S)){
            leave();
        }

        if (Input.GetKey(KeyCode.G)){
            quiet();
        }
        //ひとさし指か親指が垂直かどうか
        //左手の人差し指が水平かどうか
        var leftIndexHorizontalValue=Mathf.Abs(leftIndexTip.y-leftIndex1.y);
        if (0.03 >= leftIndexHorizontalValue){
            leftIndexHorizontal="True";
        }else{
            leftIndexHorizontal="Folse";
        }

        //左手の人差し指が垂直かどうか
        var leftIndexVerticalValue=Mathf.Abs(leftIndexTip.x-leftIndex1.x);
        if (0.03 >= leftIndexVerticalValue){
            leftIndexVertical="True";
        }else{
            leftIndexVertical="Folse";
        }

        //右手のひとさし指が水平かどうか
        var rightIndexHorizontalValue=Mathf.Abs(rightIndexTip.y-rightIndex1.y);
        if (0.03 >= rightIndexHorizontalValue){
            rightIndexHorizontal="True";
        }else{
            rightIndexHorizontal="Folse";
        }

        //右手の人差し指が垂直かどうか
        var rightIndexVerticalValue=Mathf.Abs(rightIndexTip.x-rightIndex1.x);
        if (0.03 >= rightIndexVerticalValue){
            rightIndexVertical="True";
        }else{
            rightIndexVertical="Folse";
        }

        //左手の人差し指が傾いていないか
        if (0.03 > Mathf.Abs(leftIndexTip.z-leftIndex1.z)){
            leftIndexTilt="True";
        }else{
            leftIndexTilt="Folse";
        }

        if (0.03 > Mathf.Abs(leftThumbTip.z-leftThumb2.z)){
            leftThumbTilt="True";
        }else{
            leftThumbTilt="Folse";
        }

        if (0.03 > Mathf.Abs(rightIndexTip.z - rightIndex1.z)){
            rightIndexTilt="True";
        }else{
            rightIndexTilt="Folse";
        }

        if (0.03 > Mathf.Abs(rightThumbTip.z - rightThumb2.z)){
            rightThumbTilt="True";
        }else{
            rightThumbTilt="Folse";
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
        var leftExtend="Folse";
        var rightExtend="Folse";

        //指の曲げ伸ばしはあっているか
        if(isThumbStraight && isIndexStraight && !isMiddleStraight  && !isRingStraight  && !isPinkyStraight ){ //人差し指だけまっすぐで、その他が曲がっている
            leftExtend="True";
        }else{
            leftExtend="Folse";
        }
        if(rightisThumbStraight & rightisIndexStraight & !rightisMiddleStraight & !rightisRingStraight & !rightisPinkyStraight){
            rightExtend="True";
        }else{
            rightExtend="Folse";
        }

        if (leftExtend=="True" & rightExtend =="True"){
            extend="True";
        }else{
            extend="Folse";
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



        if (0.8 >leftFingerAngle & 0.8 > rightFingerAngle ){
            fingerAngle="True";
        }else{
            fingerAngle="Folse";
        }





        //片方の手がもう片方のなかにいる
        if (leftIndexHorizontal=="True" & leftThumbTip.y>leftIndexTip.y ||leftIndexVertical=="True" & leftIndexTip.y > leftThumbTip.y ){
            if(rightThumb2.x>leftThumb2.x & rightThumb2.y > leftThumb2.y){
                leftpattern1="True";
            }else{
                leftpattern1="Folse";
            }
        }else{
            leftpattern1="Folse";
        }

        if (rightIndexHorizontal=="True" & rightIndexTip.y > rightThumbTip.y || rightIndexVertical=="True" & rightThumbTip.y>rightIndexTip.y){
            if(rightThumb2.x > leftThumb2.x & rightThumb2.y > leftThumb2.y){
                rightpattern1="True";
            }else{
                rightpattern1="Folse";
            }
        }else{
            rightpattern1="Folse";
        }

        if(leftIndexHorizontal=="True" & leftIndexTip.y > leftThumbTip.y || leftIndexVertical=="True" & leftThumbTip.y > leftIndexTip.y){
            if (rightThumb2.x > leftThumb2.x & leftThumb2.y > rightThumb2.y){
                leftpattern2="True";
            }else{
                leftpattern2="Folse";
            }
        }else{
            leftpattern2="Folse";
        }

        if(rightIndexHorizontal=="True" & rightThumbTip.y > rightIndexTip.y || rightIndexVertical=="True" & rightIndexTip.y > rightThumbTip.y){
            if(rightThumb2.x > leftThumb2.x & leftThumb2.y > rightThumb2.y){
                rightpattern2="True";
            }else{
                rightpattern2="Folse";
            }
        }else{
            rightpattern2="Folse";
        }

        if(leftpattern1 == "True" & rightpattern1== "True"){
            pattern1="True";
        }else{
            pattern1="Folse";
        }
        if(leftpattern2 == "True" & leftpattern2 == "True"){
            pattern2="True";
        }else{
            pattern2="Folse";
        }

        if(pattern1=="True" || pattern2=="True"){
            pattern="True";
        }else{
            pattern="Folse";
        }



        //手は離れすぎていないか
        float xdistance=Mathf.Abs(leftThumb2.x-rightThumb2.x);
        float ydistance=Mathf.Abs(leftThumb2.y-rightThumb2.y);
        Debug.Log("xdistance"+xdistance);
        Debug.Log("ydistance"+ydistance);
        if (xdistance / ydistance > 1.5 & 3.5 > xdistance / ydistance){
            if(0.3 > ydistance){
                handDistance="True";
            }else{
                handDistance="Folse";
            }

        }else{
            handDistance="Folse";
        }



        //マスクができているか
        if (extend=="True" & fingerAngle=="True" & pattern=="True" & handDistance=="True" & leftIndexTilt=="True" & leftThumbTilt=="True" & rightIndexTilt == "True" & rightThumbTilt == "True"){
            msk="True";
        }else{
            msk="Folse";
        }
        Debug.Log("指の曲げ伸ばし"+extend);
        Debug.Log("指の角度"+fingerAngle);
        Debug.Log("片方の手に入っているか"+pattern);
        Debug.Log("てのきょり"+handDistance);
        Debug.Log(msk);

    }
    public IEnumerator leave(){
        var rightLeaveJudgment="Folse";
        var leftLeaveJudgment="Folse";
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
            leftLeaveJudgment="True";
        }else{
            leftLeaveJudgment="Folse";
        }
        if (rightmaximum - rightminimum > 0.1){
            rightLeaveJudgment="True";
        }else{
            rightLeaveJudgment="Folse";
        }

        if (leftLeaveJudgment == "True" & rightLeaveJudgment == "True" & leftIndexTip.y > leftThumbTip.y & rightIndexTip.y > rightThumbTip.y & 1 > Mathf.Abs(leftThumb2.y-rightThumb2.y) ){
            leaveStatus="True";
        }else{
            leaveStatus="Folse";
        }


        yield return new WaitForSeconds(0.02f);
        StartCoroutine("leave");
    }

    public void quiet(){
        var leftstright="Folse";
        var rightstright="Folse";
        var leftstatus="Folse";
        var rightstatus="Folse";

        //指の曲げ伸ばしが正しいか
        if (!isThumbStraight & isIndexStraight & !isMiddleStraight & !isRingStraight & !isPinkyStraight){
            leftstright="True";
        }else{
            leftstright="Folse";
        }

        if(!rightisThumbStraight & rightisIndexStraight & !rightisMiddleStraight & !rightisRingStraight & !rightisPinkyStraight){
            rightstright = "True";
        }else{
            rightstright = "Folse";
        }


　　　　//指の状態は正しいか
        if (leftIndexTilt=="True" & leftIndexVertical=="True"){
            leftstatus="True";
        }else{
            leftstatus="Folse";
        }

        if (rightIndexTilt=="True" & rightIndexVertical=="True"){
            rightstatus="True";
        }else{
            rightstatus="Folse";
        }

        //静かにしなさいジェスチャーがただしいかどうか
        if (leftstright=="True" & leftstatus=="True" & leftThumb2.y > rightThumb2.y){
            quietStatus="True";

        }else if (rightstright=="True" & rightstatus=="True" & rightThumb2.y > leftThumb2.y){
            quietStatus="True";

        }else{
            quietStatus="Folse";
        }


    }


}
