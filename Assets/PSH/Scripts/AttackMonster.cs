using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    public GameObject affectedObject; // 특정 오브젝트에만 적용할 대상 오브젝트를 설정하는 public 변수

    private int clickCount = 0;
    private bool isObjectActive = true;
    private Rigidbody rb;
    private float moveForce = 5f;
    private float jumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 동작
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭된 위치의 오브젝트 가져오기
            GameObject clickedObject = GetClickedObject();

            // affectedObject가 설정되어 있고 클릭된 오브젝트가 affectedObject일 때만 동작
            if (clickedObject != null && clickedObject == affectedObject && isObjectActive)
            {
                clickCount++;

                // 클릭 횟수에 따라 다른 동작 수행
                if (clickCount <= 10)
                {
                    // 오브젝트를 뒤로 밀어내기 (물리적 힘을 사용하여)
                    rb.AddForce(-transform.forward * moveForce, ForceMode.Impulse);

                    // 점프 효과 주기
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
                else
                {
                    // 오브젝트를 비활성화 (사라지게 함)
                    gameObject.SetActive(false);
                }
            }
        }
    }

    GameObject GetClickedObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}