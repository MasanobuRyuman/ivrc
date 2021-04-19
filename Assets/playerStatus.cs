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
        var rightThumbTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        var rightThumb2=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_Thumb2].Transform.position;
        var rightIndexTip=righthand.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;

        var distance=(leftThumbTip-leftThumb2).normalized;
        var distance2=(leftIndexTip-leftThumb2).normalized;
        leftFingerAngle=Vector3.Dot(distance,distance2);
        Debug.Log(leftFingerAngle);



    }



}
