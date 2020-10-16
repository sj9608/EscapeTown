using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 칼, 총 등 무기 오브젝트로 공격하는 script component
public class PlayerAttack : MonoBehaviour
{
    // 사용할 무기
    // 사용할 총
    public Gun gun;
    // 사용할 칼
    public Knife knife;
    Transform weaponPivot; // IK용 배치의 기준점
    Transform gunPivot; // 총 배치의 기준점
    Transform knifePivot; // 칼 배치의 기준점
    Transform leftHandMount; // IK의 왼손이 위치할 지점
    Transform rightHandMount; // IK의 오른손이 위치할 지점
    public Transform gunLeftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform gunRightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점
    public Transform knifeLeftHandMount; // 칼의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform knifeRightHandMount; // 칼의 오른쪽 손잡이, 오른손이 위치할 지점

    // 현재 상태 
    bool isGun;
    bool isKnife;
    
    private GameKeyInput gameKeyInput; // 플레이어의 입력
    private Animator playerAnimator; // 애니메이터 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        gameKeyInput = GameManager.Instance.gameKeyInput;
        playerAnimator = GetComponent<Animator>();

        gunPivot = gun.transform.parent;
        knifePivot = knife.transform.parent;

        isGun = false;
        isKnife = true;

        playerAnimator.SetBool("isGun", isGun);
        playerAnimator.SetBool("isKnife", isKnife);
        gun.transform.parent.gameObject.SetActive(isGun);
        //knife.transform.parent.gameObject.SetActive(isKnife);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameKeyInput.changeWeapon)
        {
            ChangeWeapon();
        }
        // 입력을 감지하고 총 발사하거나 재장전
        if (gameKeyInput.fire)
        {
            // 발사 입력 감지시 총 발사
            Attack();
        }
        else if (isGun && gameKeyInput.reload)
        {
            // 재장전 입력 감지시 재장전
            if (gun.Reload())
            {
                // 재장전 성공시에만 재장전 애니메이션 재생
                playerAnimator.SetTrigger("Reload");
            }
        }
    }
    public void Attack()
    {
        if (isGun)
        {
            gun.Fire();
        }
        else if (isKnife)
        {

        }
    }
    public void ChangeWeapon()
    {
        isGun = !isGun;
        isKnife = !isKnife;
        playerAnimator.SetBool("isGun", isGun);
        playerAnimator.SetBool("isKnife", isKnife);
        gun.transform.parent.gameObject.SetActive(isGun);
        knife.transform.parent.gameObject.SetActive(isKnife);
        if (isGun)
        {
            weaponPivot = gunPivot;
            leftHandMount = gunLeftHandMount;
            rightHandMount = gunRightHandMount;
        }
        else
        {
            weaponPivot = knifePivot;
            leftHandMount = knifeLeftHandMount;
            rightHandMount = knifeLeftHandMount;
        }
        Debug.Log("무기 바뀜");
    }
    // 애니메이터의 IK 갱신
    //private void OnAnimatorIK(int layerIndex)
    //{
    //    if (weaponPivot == null)
    //    {
    //        return;
    //    }
    //    // 총의 기준점 gunPivot을 3D 모델의 오른쪽 팔꿈치 위치로 이동
    //    weaponPivot.position =
    //        playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

    //    // IK를 사용하여 왼손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

    //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand,
    //        leftHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand,
    //        leftHandMount.rotation);

    //    // IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

    //    playerAnimator.SetIKPosition(AvatarIKGoal.RightHand,
    //        rightHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.RightHand,
    //        rightHandMount.rotation);
    //}
}
