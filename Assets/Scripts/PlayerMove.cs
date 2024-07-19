// ���Ŀ��� �ִϸ��̼� �߰� 2��° �õ� (���ο� ai ����)
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �÷��̾� �ӵ��� ����
    public float MoveSpeed = 3.0f;
    public float RunSpeed = 7.0f;

    // ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;

    // �߷� ����
    float gravity = -20f;

    // ���� �ӷ� ����
    public float yVelocity = 0;

    // ������ ����
    public float jumpPower = 5f;

    // ���� Ȯ�� ����
    public bool isJumping = false;

    // �ִϸ�����
    private Animator animator;

    public float ws = Input.GetAxis("Horizontal");
    public float ad = Input.GetAxis("Vertical");

    private void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // �÷��̾� �յ�, �¿�, ����
        float ws = Input.GetAxis("Horizontal");
        float ad = Input.GetAxis("Vertical");

        animator.SetFloat("ForwardMove", ws);
        animator.SetFloat("BackMove", ad);

        // �̵� ���� ����
        Vector3 direction = new Vector3(ws, 0, ad);
        direction.Normalize();

        // �޸��� üũ ( ctrl Ű ������ �޸���)
        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

        // ī�޶� �������� ���� ��ȯ
        direction = Camera.main.transform.TransformDirection(direction);
        Vector3 horizontalMovement = direction * currentSpeed * Time.deltaTime;

        // ĳ���� �����ӵ��� �߷°��� �����Ѵ�.
        if (cc.isGrounded)
        {
            yVelocity = -1f; // �ٴڿ� ���� �� ���� �ӵ��� �ణ ������ �����Ͽ� �پ� �ְ� ��
            if (Input.GetButtonDown("Jump"))
            {
                // ĳ���� ���� �ӵ��� ������ �ۿ�
                yVelocity = jumpPower;
                isJumping = true;
            }
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        // �̵� �ӵ��� ���� �̵��Ѵ�
        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;
        cc.Move(horizontalMovement + verticalMovement);

        //if(horizontalMove == )
        //animator.SetBool("isRunning", false);


        // �̵� ���� ���� �ִϸ��̼� ���
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



//// ���Ŀ��� �ִϸ��̼� �߰� (((�ȵ�)))
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    // �÷��̾� �ӵ��� ����
//    public float MoveSpeed = 3.0f;
//    public float RunSpeed = 7.0f;

//    // ĳ���� ��Ʈ�ѷ� ����
//    CharacterController cc;

//    // �߷� ����
//    float gravity = -20f;

//    // ���� �ӷ� ����
//    public float yVelocity = 0;

//    // ������ ����
//    public float jumpPower = 5f;

//    // ���� Ȯ�� ����
//    public bool isJumping = false;

//    // �ִϸ�����
//    private Animator Foward;

//    private void Start()
//    {
//        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
//        cc = GetComponent<CharacterController>();

//        // ������ �پ��ִ�  Animator ������Ʈ ��������
//        Foward = GetComponent<Animator>();

//        //// �ִϸ����� ������Ʈ �޾ƿ���
//        //animator = GetComponent<Animator>();
//        //if (animator == null)
//        //{
//        //    Debug.LogError("Animator component not found on player.");
//        //}
//    }

//    void Update()
//    {
//        // �÷��̾� �յ�, �¿�, ����
//        float ws = Input.GetAxis("Horizontal");
//        float ad = Input.GetAxis("Vertical");

//        // �̵� ���� ����
//        Vector3 direction = new Vector3(ws, 0, ad);
//        direction.Normalize();

//        // �޸��� üũ ( ctrl Ű ������ �޸���)
//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

//        // ī�޶� �������� ���� ��ȯ
//        direction = Camera.main.transform.TransformDirection(direction);
//        Vector3 horizontalMovement = direction * currentSpeed * Time.deltaTime;

//        // ĳ���� �����ӵ��� �߷°��� �����Ѵ�.
//        if (cc.isGrounded)
//        {
//            yVelocity = -1f; // �ٴڿ� ���� �� ���� �ӵ��� �ణ ������ �����Ͽ� �پ� �ְ� ��
//            if (Input.GetButtonDown("Jump"))
//            {
//                // ĳ���� ���� �ӵ��� ������ �ۿ�
//                yVelocity = jumpPower;
//                isJumping = true;
//            }
//        }
//        else
//        {
//            yVelocity += gravity * Time.deltaTime;
//        }

//        // �̵� �ӵ��� ���� �̵��Ѵ�
//        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;
//        cc.Move(horizontalMovement + verticalMovement);

//        //// �ִϸ����Ϳ� �ӵ� �� ����
//        //animator.SetFloat("Speed", horizontalMovement.magnitude);

//        //// ���� �ִϸ��̼� ó��
//        //animator.SetBool("isJumping", !cc.isGrounded);

//        // �ִϸ����͸� ����� ȸ�� �ִϸ��̼� �ֱ�
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

// ��ó�� ������ ���ڿ���������
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

//    public Camera mainCamera; // ī�޶� ���� ����

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
//        direction.y = 0; // y�� ������ �����ϰ� ����鿡���� ���⸸ ���
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

//// ī�޶� �̵��ÿ� ������ �ȵ�. ������ �޸��� ������ ������ 2�� ����
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


//// ������ 2�� ���� �ٽ� �߻�
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



//// ���� ���� �۵�  ������ 2�� ���� �����ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѾ���
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting.FullSerializer;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    // �÷��̾� �ӵ��� ����
//    public float MoveSpeed = 3.0f;
//    public float RunSpeed = 7.0f;

//    // ĳ���� ��Ʈ�ѷ� ����
//    CharacterController cc;

//    // �߷� ����
//    float gravity = -20f;

//    // ���� �ӷ� ����
//    public float yVelocity = 0;

//    // ������ ����
//    public float jumpPower = 5f;

//    // ���� Ȯ�� ����
//    public bool isJumping = false;

//    // �ִϸ�����
//    private Animator Foward;

//    private void Start()
//    {
//        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        // �÷��̾� �յ�, �¿�, ����
//        float ws = Input.GetAxis("Horizontal");
//        float ad = Input.GetAxis("Vertical");

//        // �̵� ���� ����
//        Vector3 direction = new Vector3(ws, 0, ad);
//        direction.Normalize();

//        // �޸��� üũ ( ctrl Ű ������ �޸���)
//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

//        // ī�޶� �������� ���� ��ȯ
//        direction = Camera.main.transform.TransformDirection(direction);
//        Vector3 horizontalMovement = direction * currentSpeed * Time.deltaTime;

//        // ĳ���� �����ӵ��� �߷°��� �����Ѵ�.
//        if (cc.isGrounded)
//        {
//            yVelocity = -1f; // �ٴڿ� ���� �� ���� �ӵ��� �ణ ������ �����Ͽ� �پ� �ְ� ��
//            if (Input.GetButtonDown("Jump"))
//            {
//                // ĳ���� ���� �ӵ��� ������ �ۿ�
//                yVelocity = jumpPower;
//                isJumping = true;
//            }
//        }
//        else
//        {
//            yVelocity += gravity * Time.deltaTime;
//        }

//        // �̵� �ӵ��� ���� �̵��Ѵ�
//        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;
//        cc.Move(horizontalMovement + verticalMovement);

//    }
//}


//// ������ �޸��� ���ÿ� �ϸ� �����µ� 2�谡 ��.
///
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{


//    //�÷��̾� �ӵ��� ����
//    public float MoveSpeed = 2.0f;
//    public Vector3 direction;
//    public float RunSpeed = 4.0f;

//    // ĳ���� ��Ʈ�ѷ� ����
//    CharacterController cc;

//    // �߷� ����
//    float gravity = -20f;

//    // ���� �ӷ� ����
//    public float yVelocity = 0;

//    // ������ ����
//    public float jumpPower = 5f;

//    // ���� Ȯ�� ����
//    public bool isJumping = false;

//    // �޸���
//    //public float RunSpeedValue = 4.0f;

//    //// ȭ�� ������ ȸ��
//    //public float rotspeed = 200f;

//    //// ȸ�� �� ����
//    //float mx = 0;
//    //float my = 0;

//    private void Start()
//    {

//        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {

//        // �÷��̾� �յ�, �¿�, ����
//        float ws = Input.GetAxis("Horizontal");
//        float ad = Input.GetAxis("Vertical");
//        //float space = Input.GetAxisRaw("Jump");

//        // �̵� ���� ����
//        //Vector3 direction = new Vector3(ws, space, ad);
//        Vector3 direction = new Vector3(ws, 0, ad);
//        direction.Normalize();

//        // �޸��� üũ ( ctrl Ű ������ �޸���)
//        bool isRunning = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W);
//        float currentSpeed = isRunning ? RunSpeed : MoveSpeed;

//        // ī�޶� �������� ���� ��ȯ
//        direction = Camera.main.transform.TransformDirection(direction);


//        // ĳ���� �����ӵ��� �߷°��� �����Ѵ�.
//        yVelocity += gravity * Time.deltaTime;
//        direction.y = yVelocity;

//        // �����̽��� ������
//        if (Input.GetButtonDown("Jump") && cc.isGrounded)
//        {
//            // ĳ���� ���� �ӵ��� ������ �ۿ�
//            yVelocity = jumpPower;
//            isJumping = true;

//            // ĳ���� ���� �ӵ��� �߷� ���� �����Ѵ�.
//            yVelocity += gravity * Time.deltaTime;
//            direction.y = yVelocity;
//        }

//        // �ٴڿ� �����ߴٸ�
//        if (cc.collisionFlags == CollisionFlags.Below)
//        {
//            isJumping = false;
//            ////���� ���̾��ٸ�
//            //if (isJumping)
//            //{
//            //    //���� �� ���·� �ʱ�ȭ
//            //    isJumping = false;

//            //    // ĳ���� ���� �ӵ��� 0���� �����
//            //    yVelocity = 0;
//            //}
//        }
//        else
//        {
//            isJumping = true;
//            yVelocity += gravity * Time.deltaTime;
//        }


//        // �����̽� ������ ���� ��������
//        if (Input.GetButtonDown("Jump") && !isJumping)
//        {
//            //ĳ���� �����ӵ��� �������� �����ϰ� ���� ���·� ����
//            yVelocity = jumpPower;
//            isJumping = true;
//        }





//        // �̵� �ӵ��� ���� �̵��Ѵ�

//        //transform.position += direction * MoveSpeed * Time.deltaTime;
//        cc.Move(direction * currentSpeed * Time.deltaTime + Vector3.up * yVelocity * Time.deltaTime);

//        //// ȭ�� ������
//        ////1. ���콺 �Է� �ޱ�
//        //float mouse_X = Input.GetAxis("Mouse X");
//        //float mouse_Y = Input.GetAxis("Mouse Y");

//        ////1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ������Ų��.
//        //mx += mouse_X * rotspeed * Time.deltaTime;
//        //my += mouse_Y * rotspeed * Time.deltaTime;

//        ////1-2. ���콺 ���� �̵� ȸ�� ����(my)�� ���� -90��~90�� ���̷� �����Ѵ�.
//        //my = Mathf.Clamp(my, -90f, 90f);

//        ////2. ���콺 �Է� ���� �̿��� ȸ�� ������ �����Ѵ�.
//        //Vector3 dir = new Vector3(-my, mx, 0);

//        ////3. ȸ�� �������� ��ü�� ȸ����Ų��.
//        //transform.eulerAngles = dir;

//        ////4. x�� ȸ��(���� ȸ��) ���� -90��~90�� ���̷� �����Ѵ�.
//        ////Vector3 rot = transform.eulerAngles;
//        ////rot.x = Mathf.Clamp(rot.x, -90f, 90f);
//        ////transform.eulerAngles = rot;

//    }

//    // ctrl�� �޸��⸦ �ֱ��ߴµ�, �ΰ��� ������ ��� ����
//    // ������ �ϸ� �ȳ������� ��õ �ع����µ� ��� ����


//}
