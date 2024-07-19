using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    public float rotationSpeed = 45f; // 회전 속도

    void Update()
    {
        // 프리팹을 제자리에서 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // 충돌한 대상의 태그가 "PlayerObject"라면 Player 오브젝트를 제거한다.
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        // 충돌한 대상에 관계없이 자기 자신을 제거한다.
    //        Destroy(gameObject);
    //    }
    //}

    // 물리적 충돌 없이 충돌 감지만 했을 때 실행되는 이벤트 함수
    private void OnTriggerEnter(Collider col)
    {
        // 충돌한 게임 오브젝트를 제거한다.
        RotatingBlock smallblock = col.gameObject.GetComponent<RotatingBlock>();

        // enemy 변수에 값이 있다면...
        if (smallblock != null)
        {
            // 충돌한 에너미 오브젝트를 제거한다.
            Destroy(col.gameObject);

            // GameManager에 있는 currentScore 값을 1 추가한다.
            //GameManager.gm.AddScore(11);      

            // PlayerFire 컴포넌트에 있는 PlayExplotionSound 함수를 실행한다.
            //playerFire.PlayExplosionSound();
        }
               
           Destroy(gameObject);
        
    }
}

