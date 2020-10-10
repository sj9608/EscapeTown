using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller; // 사용할 캐릭터 컨트롤러
    public float speed = 6f; // 캐릭터의 속도
    public float jumpHeight = 3f; // 캐릭터의 점프력
    public float gravity = -9.81f; // 중력

    /* 그라운드 체크 */
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity; // 낙하속도 계산할 벡터(중력값에 대한 계산을 할 속도벡터)
    bool isGrounded;

    public Transform cam; // 캐릭터를 카메라가 보는 방향에 대응하게 만들기 위한 카메라의 트랜스폼을 받아올 오브젝트 // 메인카메라를 사용해야함.

    private float turnSmoothVelocity; // 캐릭터의 부드러운 방향전환을 위한 회전속도 변수
    public float turnSmoothTime = 0.1f; // 캐릭터의 회전을 부드럽게 만들어줄 변수

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        /* 지면 체크 및 중력(낙하속도)*/
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // 캐릭터가 땅에 닿았는지 체크 닿았으면 true 반환
        Debug.Log(isGrounded);
        if(isGrounded && velocity.y < 0) // 땅 , 낙하속도 검사
        {
            velocity.y = -2f; // 땅에 닿아있고 낙하속도가 0 보다 낮으면 낙하속도를 -2f 로 초기화
        }

        /* 캐릭터 중력에 대한 움직임 */
        velocity.y += gravity * Time.deltaTime; // 중력값 계산
        controller.Move(velocity * Time.deltaTime); // 실제 중력 적용

        /* 점프 */
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // 중력값에 비례한 점프력 계산 ( 중간 -2는 중력값이 마이너스 값이라서 위로 뛰려면 양수로 만들어줘야함)
        }

        // 플레이어 이동 방향에 대한 벡터
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; 

        // 움직임 검사
        if(direction.magnitude >= 0.1f) // 움직임이 있다면 (어느방향으로든 방향벡터값이)
        {
            // 플레이어가 움직이는 방향에 대한 회전을 하기 위해서
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // (x, z)벡터에 대한 각도 * 라디안 단위로 변환 + ( 카메라의 회전에 캐릭터가 대응하게 하기 위한 값)
            
            // 캐릭터의 회전을 부드럽게 만들기 위한 처리 과정
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // 실제 회전 진행
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // 카메라가 바라보는 방향에 대응하게 하기위한 벡터 (실제 움직임 벡터)
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir * speed * Time.deltaTime); // 해당 벡터의 방향으로 speed 수치만큼 frame간격마다 이동 (카메라가 바라보는 방향으로 움직이게 하기위해서 direction --> moveDir 교체)
        }
    }
}
