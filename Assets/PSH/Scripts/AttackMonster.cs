using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    public float damagePerClick = 2f; // 클릭당 입히는 기본 데미지
    public float swordDamageBonus = 5f; // 칼이 활성화되었을 때 추가로 입히는 데미지

    private GameObject affectedObject; // 특정 몬스터를 참조할 변수

    private bool isSwordActive = false; // 칼이 활성화되었는지 여부

    private Rigidbody rb;
    private float moveForce = 5f;
    private float jumpForce = 5f;

    void Start()
    {
        // 예제로 affectedObject를 Scene에서 "Enemy" 태그를 가진 첫 번째 오브젝트로 설정
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            affectedObject = enemies[0];
        }
        else
        {
            Debug.LogError("No enemy found with 'Enemy' tag.");
        }

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

            // affectedObject가 설정되어 있고 클릭된 오브젝트가 affectedObject일 때만 동작
            if (clickedObject != null && clickedObject == affectedObject)
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
                MonsterHealth monsterHealth = affectedObject.GetComponent<MonsterHealth>();
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
                    Debug.LogError("Affected object does not have MonsterHealth component.");
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