using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCheck : MonoBehaviour
{
    // ������Ʈ 1�� ������Ʈ 2�� �巡�� �� ������� �Ҵ��մϴ�.
    public GameObject object1;
    public GameObject object2;

    void Start()
    {
        // ������Ʈ 2�� ��Ȱ��ȭ�մϴ�. 
        // (���� �ÿ� ������Ʈ 2�� ��Ȱ��ȭ�� ���¶�� �� �ڵ�� ��� �˴ϴ�)
        if (object2 != null)
        {
            object2.SetActive(false);
        }
    }

    void Update()
    {
        // ������Ʈ 1�� �ı��Ǿ����� Ȯ���մϴ�.
        if (object1 == null)
        {
            // ������Ʈ 2�� Ȱ��ȭ�մϴ�.
            if (object2 != null)
            {
                object2.SetActive(true);
            }
        }
    }
}