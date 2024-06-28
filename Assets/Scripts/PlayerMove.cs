using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    //�÷��̾� �ӵ��� ����
    public float MoveSpeed = 2.0f;
    public Vector3 direction;

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

    // �޸���
    public float RunSpeed = 4.0f;

    //// ȭ�� ������ ȸ��
    //public float rotspeed = 200f;

    //// ȸ�� �� ����
    //float mx = 0;
    //float my = 0;

    private void Start()
    {
        
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        // �÷��̾� �յ�, �¿�, ����
        float ws = Input.GetAxis("Horizontal");
        float ad = Input.GetAxis("Vertical");
        //float space = Input.GetAxisRaw("Jump");

        // �޸���

        
        //if (Input.GetButtonDown("Run"))
        //{
        //    Vector3 Forward = new Vector3(ws, 0,0);
        //    transform.position += Forward * RunSpeed * Time.deltaTime;
        //}

        // �̵� ���� ����
        //Vector3 direction = new Vector3(ws, space, ad);
        Vector3 direction = new Vector3(ws, 0, ad);
        direction.Normalize();

        // ī�޶� �������� ���� ��ȯ
        direction = Camera.main.transform.TransformDirection(direction);

        // ĳ���� �����ӵ��� �߷°��� �����Ѵ�.
        yVelocity += gravity * Time.deltaTime;
        direction.y = yVelocity;

        // �����̽��� ������
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            // ĳ���� ���� �ӵ��� ������ �ۿ�
            yVelocity = jumpPower;
            isJumping = true;

            // ĳ���� ���� �ӵ��� �߷� ���� �����Ѵ�.
            yVelocity += gravity * Time.deltaTime;
            direction.y = yVelocity;
        }
        
        // �ٴڿ� �����ߴٸ�
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            ////���� ���̾��ٸ�
            //if (isJumping)
            //{
            //    //���� �� ���·� �ʱ�ȭ
            //    isJumping = false;

            //    // ĳ���� ���� �ӵ��� 0���� �����
            //    yVelocity = 0;
            //}
        }
        else
        {
            isJumping = true;
        }


        // �����̽� ������ ���� ��������
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            //ĳ���� �����ӵ��� �������� �����ϰ� ���� ���·� ����
            yVelocity = jumpPower;
            isJumping = true;
        }




        // �̵� �ӵ��� ���� �̵��Ѵ�

        //transform.position += direction * MoveSpeed * Time.deltaTime;
        cc.Move(direction * MoveSpeed * Time.deltaTime);

        //// ȭ�� ������
        ////1. ���콺 �Է� �ޱ�
        //float mouse_X = Input.GetAxis("Mouse X");
        //float mouse_Y = Input.GetAxis("Mouse Y");

        ////1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ������Ų��.
        //mx += mouse_X * rotspeed * Time.deltaTime;
        //my += mouse_Y * rotspeed * Time.deltaTime;

        ////1-2. ���콺 ���� �̵� ȸ�� ����(my)�� ���� -90��~90�� ���̷� �����Ѵ�.
        //my = Mathf.Clamp(my, -90f, 90f);

        ////2. ���콺 �Է� ���� �̿��� ȸ�� ������ �����Ѵ�.
        //Vector3 dir = new Vector3(-my, mx, 0);

        ////3. ȸ�� �������� ��ü�� ȸ����Ų��.
        //transform.eulerAngles = dir;

        ////4. x�� ȸ��(���� ȸ��) ���� -90��~90�� ���̷� �����Ѵ�.
        ////Vector3 rot = transform.eulerAngles;
        ////rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        ////transform.eulerAngles = rot;

    }

    // ctrl�� �޸��⸦ �ֱ��ߴµ�, �ΰ��� ������ ��� ����
    // ������ �ϸ� �ȳ������� ��õ �ع����µ� ��� ����

   
}
