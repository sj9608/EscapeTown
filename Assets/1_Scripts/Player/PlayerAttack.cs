using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 칼, 총 등 무기 오브젝트로 공격하는 script component
public class PlayerAttack : MonoBehaviour
{
    GameManager GMI;
    // 사용할 총
    public Gun gun;

    // 현재 상태 
    public bool isGun;
    // 에임 조준 상태
    bool isAim;
    private Animator playerAnimator; // 애니메이터 컴포넌트
    private void Awake() {
        GMI = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        isGun = true;
        isAim = false;
    }

    void Update()
    {
        if (GMI.isGameOver || GMI.isLoading || GMI.isInteractioning)
        {
            // 게임오버거나 로딩중이거나 대화중이면 입력키 막음
            return;
        }
        // 조준 bool값은 Input으로 판단
        isAim = Input.GetButton("Fire2");
        if (isGun == true)
        {
            // 총을 들고 있으면 조준선 표시
            GMI.IsAimAction(isAim);
        }
        // 입력을 감지하고 총 발사하거나 재장전
        if (isAim && Input.GetButton("Fire1"))
        {
            // 발사 입력 감지시 총 발사
            Attack();
        }
        else if (isGun && Input.GetKeyDown(KeyCode.R))
        {
            // 재장전 입력 감지시 재장전
            if (gun.Reload())
            {
                // 재장전 성공시에만 재장전 애니메이션 재생
                playerAnimator.SetTrigger("Reload");
            }
        }
    }
    // 공격 시도
    public void Attack()
    {
        if (isGun)
        {
            // 총 들었을때 건에 공격 요청
            gun.Fire();
        }
    }    
}