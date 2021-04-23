﻿using System.Collections;
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
        var isThumbStraight = IsStraightright(0.8f,OVRSkeleton.BoneId.Hand_Thumb1,OVRSkeleton.BoneId.Hand_Thumb2,OVRSkeleton.BoneId.Hand_Thumb3,OVRSkeleton.BoneId.Hand_ThumbTip);
        var isIndexStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
        var isMiddleStraight = IsStraightright(0.8f,OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        var isRingStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        var isPinkyStraight = IsStraightright(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);

        //指の曲げ伸ばしはあっているか
        if(isThumbStraight && isIndexStraight && !isMiddleStraight  && !isRingStraight  && !isPinkyStraight ){ //人差し指だけまっすぐで、その他が曲がっている
            extend="True";
        }else{
            extend="Folse";
        }



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



        //ひとさし指か親指が垂直かどうか
        //左手の人差し指が水平かどうか
        var leftIndexHorizontalValue=Mathf.Abs((leftIndexTip.y-leftIndex1.y)*100);
        if (2 >= leftIndexHorizontalValue){
            leftIndexHorizontal="True";
        }else{
            leftIndexHorizontal="Folse";
        }

        //左手の人差し指が垂直かどうか
        var leftIndexVerticalValue=Mathf.Abs((leftIndexTip.x-leftIndex1.x)*100);
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
            rightIndexVertical="Folse";
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
        if (extend=="True" & fingerAngle=="True" & pattern=="True" & handDistance=="True"){
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





}
