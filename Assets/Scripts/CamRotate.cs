using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // 화면 돌리기 회전
    public float rotspeed = 200f;

    // 회전 값 변수
    float mx = 0;
    float my = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 화면 돌리기
        //1. 마우스 입력 받기
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        //1-1. 회전 값 변수에 마우스 입력 값만큼 미리 누적시킨다.
        mx += mouse_X * rotspeed * Time.deltaTime;
        my += mouse_Y * rotspeed * Time.deltaTime;

        //1-2. 마우스 상하 이동 회전 변수(my)의 값을 -90도~90도 사이로 제한한다.
        my = Mathf.Clamp(my, -90f, 90f);

        //2. 마우스 입력 값을 이용해 회전 방향을 결정한다.
        Vector3 dir = new Vector3(-my, mx, 0);

        //3. 회전 방향으로 물체를 회전시킨다.
        transform.eulerAngles = dir;

        //4. x축 회전(상하 회전) 값을 -90도~90도 사이로 제한한다.
        //Vector3 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        //transform.eulerAngles = rot;
    }
}
