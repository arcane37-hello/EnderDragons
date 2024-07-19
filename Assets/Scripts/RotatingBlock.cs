using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    public float rotationSpeed = 45f; // ȸ�� �ӵ�

    void Update()
    {
        // �������� ���ڸ����� ȸ��
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // �浹�� ����� �±װ� "PlayerObject"��� Player ������Ʈ�� �����Ѵ�.
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        // �浹�� ��� ������� �ڱ� �ڽ��� �����Ѵ�.
    //        Destroy(gameObject);
    //    }
    //}

    // ������ �浹 ���� �浹 ������ ���� �� ����Ǵ� �̺�Ʈ �Լ�
    private void OnTriggerEnter(Collider col)
    {
        // �浹�� ���� ������Ʈ�� �����Ѵ�.
        RotatingBlock smallblock = col.gameObject.GetComponent<RotatingBlock>();

        // enemy ������ ���� �ִٸ�...
        if (smallblock != null)
        {
            // �浹�� ���ʹ� ������Ʈ�� �����Ѵ�.
            Destroy(col.gameObject);

            // GameManager�� �ִ� currentScore ���� 1 �߰��Ѵ�.
            //GameManager.gm.AddScore(11);      

            // PlayerFire ������Ʈ�� �ִ� PlayExplotionSound �Լ��� �����Ѵ�.
            //playerFire.PlayExplosionSound();
        }
               
           Destroy(gameObject);
        
    }
}

