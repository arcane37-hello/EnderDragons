// 알파에서 애니메이션 추가 2번째 시도 (새로운 ai 도움)
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어 속도랑 방향
    public float MoveSpeed = 3.0f;
    public float RunSpeed = 7.0f;

    // 캐릭터 컨트롤러 변수
    CharacterController cc;

    // 중력 변수
    float gravity = -20f;

    // 수직 속력 변수
    public float yVelocity = 0;

    // 점프력 변수
    public float jumpPower = 5f;

    // 점프 확인 변수
    public bool isJumping = false;

    // 애니메이터
    private Animator animator;

    public float ws = Input.GetAxis("Horizontal");
    public float ad = Input.GetAxis("Vertical");

    private void Start()
    {
        // 캐릭터 컨트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 플레이어 앞뒤, 좌우, 점프
        float ws = Input.GetAxis("Horizontal");
        float ad = Input.GetAxis("Vertical");

        animator.SetFloat("ForwardMove", ws);
        animator.SetFloat("BackMove", ad);

        // 이동 방향 설정
        Vector3 direction = new Vector3(ws, 0, ad);
        direction.Normalize();

        // 달리기 체크 ( ctrl 키 누르면 달리기)
        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

        // 카메라 기준으로 방향 변환
        direction = Camera.main.transform.TransformDirection(direction);
        Vector3 horizontalMovement = direction * currentSpeed * Time.deltaTime;

        // 캐릭터 수직속도에 중력값을 적용한다.
        if (cc.isGrounded)
        {
            yVelocity = -1f; // 바닥에 있을 때 수직 속도를 약간 음수로 설정하여 붙어 있게 함
            if (Input.GetButtonDown("Jump"))
            {
                // 캐릭터 수직 속도에 점프력 작용
                yVelocity = jumpPower;
                isJumping = true;
            }
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        // 이동 속도에 맞춰 이동한다
        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;
        cc.Move(horizontalMovement + verticalMovement);

        //if(horizontalMove == )
        //animator.SetBool("isRunning", false);


        // 이동 중일 때만 애니메이션 재생
        //if (horizontalMovement.magnitude > 0.1f || verticalMovement.magnitude > 0.1f)
        //{
        //    animator.SetBool("IsMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("IsMoving", false);
        //}
        if (ws > 0.2f)
        {
            animator.SetTrigger("BackMove");
        }
        else if (ws < -0.2f)
        {
            animator.SetTrigger("ForwardMove");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        if (ad > 0.2f)
        {
            animator.SetTrigger("BackMove");
        }
        else if (ad < -0.2f)
        {
            animator.SetTrigger("ForwardMove");
        }
        else
        {
            animator.SetTrigger("Idle");
        }


        //if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
        //    animator.CrossFade("walk");
        //else
        //    animator.CrossFade("idle");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ws > 0.2f)
        {
            animator.SetTrigger("BackMove");
        }
        else if (ws < -0.2f)
        {
            animator.SetTrigger("ForwardMove");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        if (ad > 0.2f)
        {
            animator.SetTrigger("BackMove");
        }
        else if (ad < -0.2f)
        {
            animator.SetTrigger("ForwardMove");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }


}



//// 알파에서 애니메이션 추가 (((안됨)))
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    // 플레이어 속도랑 방향
//    public float MoveSpeed = 3.0f;
//    public float RunSpeed = 7.0f;

//    // 캐릭터 컨트롤러 변수
//    CharacterController cc;

//    // 중력 변수
//    float gravity = -20f;

//    // 수직 속력 변수
//    public float yVelocity = 0;

//    // 점프력 변수
//    public float jumpPower = 5f;

//    // 점프 확인 변수
//    public bool isJumping = false;

//    // 애니메이터
//    private Animator Foward;

//    private void Start()
//    {
//        // 캐릭터 컨트롤러 컴포넌트 받아오기
//        cc = GetComponent<CharacterController>();

//        // 나에게 붙어있는  Animator 컴포넌트 가져오기
//        Foward = GetComponent<Animator>();

//        //// 애니메이터 컴포넌트 받아오기
//        //animator = GetComponent<Animator>();
//        //if (animator == null)
//        //{
//        //    Debug.LogError("Animator component not found on player.");
//        //}
//    }

//    void Update()
//    {
//        // 플레이어 앞뒤, 좌우, 점프
//        float ws = Input.GetAxis("Horizontal");
//        float ad = Input.GetAxis("Vertical");

//        // 이동 방향 설정
//        Vector3 direction = new Vector3(ws, 0, ad);
//        direction.Normalize();

//        // 달리기 체크 ( ctrl 키 누르면 달리기)
//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

//        // 카메라 기준으로 방향 변환
//        direction = Camera.main.transform.TransformDirection(direction);
//        Vector3 horizontalMovement = direction * currentSpeed * Time.deltaTime;

//        // 캐릭터 수직속도에 중력값을 적용한다.
//        if (cc.isGrounded)
//        {
//            yVelocity = -1f; // 바닥에 있을 때 수직 속도를 약간 음수로 설정하여 붙어 있게 함
//            if (Input.GetButtonDown("Jump"))
//            {
//                // 캐릭터 수직 속도에 점프력 작용
//                yVelocity = jumpPower;
//                isJumping = true;
//            }
//        }
//        else
//        {
//            yVelocity += gravity * Time.deltaTime;
//        }

//        // 이동 속도에 맞춰 이동한다
//        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;
//        cc.Move(horizontalMovement + verticalMovement);

//        //// 애니메이터에 속도 값 전달
//        //animator.SetFloat("Speed", horizontalMovement.magnitude);

//        //// 점프 애니메이션 처리
//        //animator.SetBool("isJumping", !cc.isGrounded);

//        // 애니메이터를 사용한 회전 애니메이션 주기
//        if (ws == 0)
//        {
//            Foward.SetTrigger("Idle");

//        }
//        else 
//        {
//            Foward.SetTrigger("forwardLeg");
//            Foward.SetTrigger("backLeg");
//        }

//    }
//}

// 전처럼 점프가 부자연스러워짐
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 2.0f;
//    public float runSpeed = 4.0f;
//    public float jumpPower = 5f;
//    public CharacterController cc;
//    private float gravity = -20f;
//    private float yVelocity = 0;
//    private bool isJumping = false;

//    public Camera mainCamera; // 카메라 참조 변수

//    void Start()
//    {
//        cc = GetComponent<CharacterController>();
//        mainCamera = Camera.main;
//    }

//    void Update()
//    {
//        mainCamera = Camera.main;

//        float ws = Input.GetAxis("Vertical");
//        float ad = Input.GetAxis("Horizontal");

//        Vector3 direction = new Vector3(ad, 0, ws);
//        direction = mainCamera.transform.TransformDirection(direction);
//        direction.y = 0; // y축 방향을 무시하고 수평면에서의 방향만 고려
//        direction.Normalize();

//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? runSpeed : moveSpeed;

//        yVelocity += gravity * Time.deltaTime;
//        direction.y = yVelocity;

//        if (Input.GetButtonDown("Jump") && cc.isGrounded)
//        {
//            yVelocity = jumpPower;
//            isJumping = true;
//        }

//        if (cc.collisionFlags == CollisionFlags.Below)
//        {
//            isJumping = false;
//            yVelocity = 0;
//        }
//        else
//        {
//            isJumping = true;
//            yVelocity += gravity * Time.deltaTime;
//        }

//        cc.Move(direction * currentSpeed * Time.deltaTime);
//    }
//}

//// 카메라 이동시에 점프가 안됨. 여전히 달리기 점프시 점프력 2배 버그
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 2.0f;
//    public float runSpeed = 4.0f;
//    public float jumpPower = 5f;
//    public CharacterController cc;
//    private float gravity = -20f;
//    private float yVelocity = 0;
//    private bool isJumping = false;

//    void Start()
//    {
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        float ws = Input.GetAxis("Vertical");
//        float ad = Input.GetAxis("Horizontal");

//        Vector3 direction = new Vector3(ad, 0, ws);
//        direction = Camera.main.transform.TransformDirection(direction);
//        direction.Normalize();

//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? runSpeed : moveSpeed;

//        yVelocity += gravity * Time.deltaTime;
//        direction.y = yVelocity;

//        if (Input.GetButtonDown("Jump") && cc.isGrounded)
//        {
//            yVelocity = jumpPower;
//            isJumping = true;
//        }

//        if (cc.collisionFlags == CollisionFlags.Below)
//        {
//            isJumping = false;
//            yVelocity = 0;
//        }
//        else
//        {
//            isJumping = true;
//            yVelocity += gravity * Time.deltaTime;
//        }

//        cc.Move(direction * currentSpeed * Time.deltaTime);
//    }
//}


//// 점프력 2배 버그 다시 발생
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 2.0f;
//    public float runSpeed = 4.0f;
//    public float jumpPower = 5f;
//    private float yVelocity = 0;
//    private float gravity = -20f;
//    private bool isJumping = false;

//    private CharacterController cc;

//    public Camera firstPersonCamera;
//    public Camera thirdPersonCamera;

//    void Start()
//    {
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        float ws = Input.GetAxis("Vertical");
//        float ad = Input.GetAxis("Horizontal");

//        Vector3 direction = new Vector3(ad, 0, ws);
//        direction.Normalize();

//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? runSpeed : moveSpeed;

//        Camera activeCamera = firstPersonCamera.gameObject.activeSelf ? firstPersonCamera : thirdPersonCamera;
//        direction = activeCamera.transform.TransformDirection(direction);

//        yVelocity += gravity * Time.deltaTime;
//        direction.y = yVelocity;

//        if (Input.GetButtonDown("Jump") && cc.isGrounded)
//        {
//            yVelocity = jumpPower;
//            isJumping = true;
//            yVelocity += gravity * Time.deltaTime;
//            direction.y = yVelocity;
//        }

//        if (cc.collisionFlags == CollisionFlags.Below)
//        {
//            isJumping = false;
//        }
//        else
//        {
//            yVelocity += gravity * Time.deltaTime;
//        }

//        cc.Move(direction * currentSpeed * Time.deltaTime + Vector3.up * yVelocity * Time.deltaTime);
//    }
//}



//// 기존 정상 작동  점프력 2배 버그 수정ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ알파
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting.FullSerializer;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    // 플레이어 속도랑 방향
//    public float MoveSpeed = 3.0f;
//    public float RunSpeed = 7.0f;

//    // 캐릭터 컨트롤러 변수
//    CharacterController cc;

//    // 중력 변수
//    float gravity = -20f;

//    // 수직 속력 변수
//    public float yVelocity = 0;

//    // 점프력 변수
//    public float jumpPower = 5f;

//    // 점프 확인 변수
//    public bool isJumping = false;

//    // 애니메이터
//    private Animator Foward;

//    private void Start()
//    {
//        // 캐릭터 컨트롤러 컴포넌트 받아오기
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        // 플레이어 앞뒤, 좌우, 점프
//        float ws = Input.GetAxis("Horizontal");
//        float ad = Input.GetAxis("Vertical");

//        // 이동 방향 설정
//        Vector3 direction = new Vector3(ws, 0, ad);
//        direction.Normalize();

//        // 달리기 체크 ( ctrl 키 누르면 달리기)
//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

//        // 카메라 기준으로 방향 변환
//        direction = Camera.main.transform.TransformDirection(direction);
//        Vector3 horizontalMovement = direction * currentSpeed * Time.deltaTime;

//        // 캐릭터 수직속도에 중력값을 적용한다.
//        if (cc.isGrounded)
//        {
//            yVelocity = -1f; // 바닥에 있을 때 수직 속도를 약간 음수로 설정하여 붙어 있게 함
//            if (Input.GetButtonDown("Jump"))
//            {
//                // 캐릭터 수직 속도에 점프력 작용
//                yVelocity = jumpPower;
//                isJumping = true;
//            }
//        }
//        else
//        {
//            yVelocity += gravity * Time.deltaTime;
//        }

//        // 이동 속도에 맞춰 이동한다
//        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;
//        cc.Move(horizontalMovement + verticalMovement);

//    }
//}


//// 점프와 달리기 동시에 하면 점프력도 2배가 됨.
///
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{


//    //플레이어 속도랑 방향
//    public float MoveSpeed = 2.0f;
//    public Vector3 direction;
//    public float RunSpeed = 4.0f;

//    // 캐릭터 컨트롤러 변수
//    CharacterController cc;

//    // 중력 변수
//    float gravity = -20f;

//    // 수직 속력 변수
//    public float yVelocity = 0;

//    // 점프력 변수
//    public float jumpPower = 5f;

//    // 점프 확인 변수
//    public bool isJumping = false;

//    // 달리기
//    //public float RunSpeedValue = 4.0f;

//    //// 화면 돌리기 회전
//    //public float rotspeed = 200f;

//    //// 회전 값 변수
//    //float mx = 0;
//    //float my = 0;

//    private void Start()
//    {

//        // 캐릭터 컨트롤러 컴포넌트 받아오기
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {

//        // 플레이어 앞뒤, 좌우, 점프
//        float ws = Input.GetAxis("Horizontal");
//        float ad = Input.GetAxis("Vertical");
//        //float space = Input.GetAxisRaw("Jump");

//        // 이동 방향 설정
//        //Vector3 direction = new Vector3(ws, space, ad);
//        Vector3 direction = new Vector3(ws, 0, ad);
//        direction.Normalize();

//        // 달리기 체크 ( ctrl 키 누르면 달리기)
//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

//        // 카메라 기준으로 방향 변환
//        direction = Camera.main.transform.TransformDirection(direction);


//        // 캐릭터 수직속도에 중력값을 적용한다.
//        yVelocity += gravity * Time.deltaTime;
//        direction.y = yVelocity;

//        // 스페이스바 누르면
//        if (Input.GetButtonDown("Jump") && cc.isGrounded)
//        {
//            // 캐릭터 수직 속도에 점프력 작용
//            yVelocity = jumpPower;
//            isJumping = true;

//            // 캐릭터 수직 속도에 중력 값을 적용한다.
//            yVelocity += gravity * Time.deltaTime;
//            direction.y = yVelocity;
//        }

//        // 바닥에 착지했다면
//        if (cc.collisionFlags == CollisionFlags.Below)
//        {
//            isJumping = false;
//            ////점프 중이었다면
//            //if (isJumping)
//            //{
//            //    //점프 전 상태로 초기화
//            //    isJumping = false;

//            //    // 캐릭터 수직 속도를 0으로 만든다
//            //    yVelocity = 0;
//            //}
//        }
//        else
//        {
//            isJumping = true;
//            yVelocity += gravity * Time.deltaTime;
//        }


//        // 스페이스 누르고 점프 안했으면
//        if (Input.GetButtonDown("Jump") && !isJumping)
//        {
//            //캐릭터 수직속도에 점프력을 적용하고 점프 상태로 변경
//            yVelocity = jumpPower;
//            isJumping = true;
//        }





//        // 이동 속도에 맞춰 이동한다

//        //transform.position += direction * MoveSpeed * Time.deltaTime;
//        cc.Move(direction * currentSpeed * Time.deltaTime + Vector3.up * yVelocity * Time.deltaTime);

//        //// 화면 돌리기
//        ////1. 마우스 입력 받기
//        //float mouse_X = Input.GetAxis("Mouse X");
//        //float mouse_Y = Input.GetAxis("Mouse Y");

//        ////1-1. 회전 값 변수에 마우스 입력 값만큼 미리 누적시킨다.
//        //mx += mouse_X * rotspeed * Time.deltaTime;
//        //my += mouse_Y * rotspeed * Time.deltaTime;

//        ////1-2. 마우스 상하 이동 회전 변수(my)의 값을 -90도~90도 사이로 제한한다.
//        //my = Mathf.Clamp(my, -90f, 90f);

//        ////2. 마우스 입력 값을 이용해 회전 방향을 결정한다.
//        //Vector3 dir = new Vector3(-my, mx, 0);

//        ////3. 회전 방향으로 물체를 회전시킨다.
//        //transform.eulerAngles = dir;

//        ////4. x축 회전(상하 회전) 값을 -90도~90도 사이로 제한한다.
//        ////Vector3 rot = transform.eulerAngles;
//        ////rot.x = Mathf.Clamp(rot.x, -90f, 90f);
//        ////transform.eulerAngles = rot;

//    }

//    // ctrl에 달리기를 넣긴했는데, 인게임 구현은 어떻게 하지
//    // 점프를 하면 안내려오고 승천 해버리는데 어떻게 하지


//}
