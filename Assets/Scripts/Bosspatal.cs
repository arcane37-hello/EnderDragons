using UnityEngine.SceneManagement;
using UnityEngine;

public class Bosspatal : MonoBehaviour
{
    // �浹�� ������ Ʈ���� ����
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            // �� ��ȯ: "2"�� ������ �̵�
            SceneManager.LoadScene(2);  // 2�� ���� �ε����Դϴ�. 
        }
    }

    // Ʈ���Ÿ� Ȱ��ȭ�Ϸ��� �ش� �ݶ��̴��� "Is Trigger"�� üũ�ؾ� �մϴ�.
}