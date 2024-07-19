using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    public float damagePerClick = 2f; // 클릭당 입히는 기본 데미지
    public float swordDamageBonus = 5f; // 칼이 활성화되었을 때 추가로 입히는 데미지

    private bool isSwordActive = false; // 칼이 활성화되었는지 여부
    private Rigidbody rb;
    private float moveForce = 5f;
    private float jumpForce = 5f;

    void Start()
    {
        // affectedObject를 이 스크립트가 붙어 있는 오브젝트 자신으로 설정
        // 'affectedObject' 변수는 더 이상 필요 없으므로 삭제
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 칼이 활성화되었는지 여부 체크
        isSwordActive = InventoryNumber.Instance.IsSwordActive();

        // 마우스 왼쪽 버튼 클릭 시 동작
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭된 위치의 오브젝트 가져오기
            GameObject clickedObject = GetClickedObject();

            // 클릭된 오브젝트가 이 스크립트가 붙어 있는 오브젝트와 같은 경우에만 동작
            if (clickedObject != null && clickedObject == gameObject)
            {
                // 데미지 적용
                float totalDamage = damagePerClick;
                if (isSwordActive)
                {
                    totalDamage += swordDamageBonus;
                }

                // 이 부분에서 totalDamage를 int로 변환하여 사용합니다.
                int damageInt = Mathf.RoundToInt(totalDamage);

                // Enemy 태그를 가진 오브젝트에서 MonsterHealth 컴포넌트를 가져와서 데미지 적용
                MonsterHealth monsterHealth = GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.TakeDamage(damageInt);

                    // 오브젝트를 뒤로 밀어내기 (물리적 힘을 사용하여)
                    rb.AddForce(-transform.forward * moveForce, ForceMode.Impulse);

                    // 점프 효과 주기
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
                else
                {
                    Debug.LogError("This object does not have MonsterHealth component.");
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