using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // ȸ�� �ӵ� ����
    float rotSpeed = 400f;

    // ȸ�� �� ����
    float mx = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ������� ���콺 �Է��� �޾� �÷��̾ ȸ����Ű�� �ʹ�.
        // 1. ���콺 �¿� �Է��� �޴´�.
        float mouse_X = Input.GetAxis("mouse X");

        // 1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ������Ų��.
        mx += mouse_X * rotSpeed * Time.deltaTime;

        // 2. ȸ�� �������� ��ü�� ȸ����Ų��.
        transform.eulerAngles = new Vector3(0,mx,0);
    }
}