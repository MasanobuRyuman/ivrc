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
    string fingerAngle;
    string leftIndexHorizontal;
    string leftIndexVertical;
    string rightIndexHorizontal;
    string rightIndexVertical;
    string leftIndexStatus;
    string rightIndexStatus;
    string indexStatus;
    string handDistance;

    string leftSign;
    string rightSign;
    string sign;

    void Start()
    {
        _skeleton=leftHand.GetComponent<OVRSkeleton>();
        righthand=rightHand.GetComponent<OVRSkeleton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            masuku();
        }
        if (Input.GetKey(KeyCode.G)){
            var isThumbStraight = IsStraight(0.8f,OVRSkeleton.BoneId.Hand_Thumb1,OVRSkeleton.BoneId.Hand_Thumb2,OVRSkeleton.BoneId.Hand_Thumb3,OVRSkeleton.BoneId.Hand_ThumbTip);
            var isIndexStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
            var isMiddleStraight = IsStraight(0.8f,OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
            var isRingStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
            var isPinkyStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);
            Debug.Log("親指は"+isThumbStraight);
            Debug.Log("人差し指は"+isIndexStraight);
            Debug.Log("中指は"+isMiddleStraight);
            Debug.Log("薬指は"+isRingStraight);
            Debug.Log("小指は"+isPinkyStraight);
            if(isThumbStraight && isIndexStraight && !isMiddleStraight  && !isRingStraight  && !isPinkyStraight ){ //人差し指だけまっすぐで、その他が曲がっている
                Debug.Log("マスク");
            }
        }

        if (Input.GetKey(KeyCode.H)){
            var isThumbStraight = IsStraightright(0.8f,OVRSkeleton.BoneId.Hand_Thumb1,OVRSkeleton.BoneId.Hand_Thumb2,OVRSkeleton.BoneId.Hand_Thumb3,OVRSkeleton.BoneId.Hand_ThumbTip);
            var isIndexStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
            var isMiddleStraight = IsStraightright(0.8f,OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
            var isRingStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
            var isPinkyStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);
            Debug.Log("親指は"+isThumbStraight);
            Debug.Log("人差し指は"+isIndexStraight);
            Debug.Log("中指は"+isMiddleStraight);
            Debug.Log("薬指は"+isRingStraight);
            Debug.Log("小指は"+isPinkyStraight);
            if(isThumbStraight && isIndexStraight && !isMiddleStraight  && !isRingStraight  && !isPinkyStraight ){ //人差し指だけまっすぐで、その他が曲がっている
                Debug.Log("マスク");
            }
        }
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
        var leftThumbTip=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        var leftThumb2=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        var leftIndexTip=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        var leftIndex1=_skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Index1].Transform.position;
        var rightThumbTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        var rightThumb2=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        var rightIndexTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        var rightIndex1=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Index1].Transform.position;
        //左手の人差し指と親指の
        var distance=(leftThumbTip-leftThumb2).normalized;
        var distance2=(leftIndexTip-leftThumb2).normalized;
        //左手の指の角度
        leftFingerAngle=Vector3.Dot(distance,distance2);
        Debug.Log(leftFingerAngle);

　　　　 //右手の人差し指と親指の角度の計算
        var rightdistance=(rightThumbTip-rightThumb2).normalized;
        var rightdistance2=(rightIndexTip-rightThumb2).normalized;
        //右手の指の角度
        rightFingerAngle=Vector3.Dot(rightdistance,rightdistance2);
        Debug.Log(rightFingerAngle);

        if (0.5 >leftFingerAngle & 0.5 > rightFingerAngle ){
            fingerAngle="True";
        }else{
            fingerAngle="Folse";
        }



    //片方の手が片方の指の間にはいっているかどうか
        if (leftIndexTip.y >= leftThumbTip.y){
            if (leftThumb2.x < rightThumb2.x & leftThumb2.y > rightThumb2.y){
                leftSign="Ture";
            }else{
                leftSign="Folse";
            }
        }

        if (leftIndexTip.y <= leftThumbTip.y){
            if (leftThumb2.x <rightThumb2.x & leftThumb2.y < rightThumb2.y){
                rightSign="True";
            }else{
                rightSign="Folse";
            }
        }
        //両方の手が片方の手の中にはいっているか
        if (leftSign=="True" & rightSign=="True"){
            sign="True";
        }else{
            sign="Folse";
        }


        //ひとさし指か親指が垂直かどうか
        //左手の人差し指が水平かどうか
        var leftIndexHorizontalValue=Mathf.Abs((leftIndexTip.y-leftIndex1.y)*100);
        if (2 >= leftIndexHorizontalValue){
            leftIndexHorizontal="True";
        }else{
            leftIndexHorizontal="Folse";
        }

        //左手の人差し指が垂直かどうか
        var leftIndexVerticalValue=Mathf.Abs((leftIndexTip.x=leftIndex1.x)*100);
        if (2 >= leftIndexVerticalValue){
            leftIndexVertical="True";
        }else{
            leftIndexVertical="Folse";
        }

        //右手のひとさし指が水平かどうか
        var rightIndexHorizontalValue=Mathf.Abs((rightIndexTip.y-rightIndex1.y)*100);
        if (2 >= rightIndexHorizontalValue){
            rightIndexHorizontal="True";
        }else{
            rightIndexHorizontal="Folse";
        }

        var rightIndexVerticalValue=Mathf.Abs((rightIndexTip.x-rightIndex1.x)*100);
        if (2 >= rightIndexVerticalValue){
            rightIndexVertical="True";
        }else{
            rightIndexHorizontal="Folse";
        }
        //左手のひと差し指が正しいか
        if(leftThumbTip.y > leftIndexTip.y & leftIndexHorizontal=="True"){
            leftIndexStatus="True";
        }else if(leftIndexTip.y > leftThumbTip.y & leftIndexVertical=="True"){
            leftIndexStatus="True";
        }else{
            leftIndexStatus="Folse";
        }

        //右手の人差し指が正しいか
        if(rightIndexTip.y > rightThumbTip.y & rightIndexHorizontal=="True"){
            rightIndexStatus="True";
        }else if(rightThumbTip.y > rightIndexTip.y & rightIndexVertical=="True"){
            rightIndexStatus="True";
        }else{
            rightIndexStatus="Folse";
        }

        //人差し指が正しいかどうか
        if (leftIndexStatus=="True" & rightIndexStatus=="True"){
            indexStatus="True";
        }else{
            indexStatus="Folse";
        }

        if (leftThumb2.y > rightThumb2.y & 5 > leftThumb2.y / rightThumb2.y ){
            handDistance="True";
        }else if(rightThumb2.y > leftThumb2.y & 5 > rightThumb2.y / leftThumb2.y){
            handDistance="True";
        }else{
            handDistance="Folse";
        }

        //マスクができているか
        if (fingerAngle=="True" & sign=="True" & indexStatus=="True" & handDistance=="True"){

        }










    }





}
