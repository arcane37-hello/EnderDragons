// ü�� + ����� + ���� ȸ�� ����
using UnityEngine;
using UnityEngine.UI;


public class HungerAndHealthSystem : MonoBehaviour
{
    [SerializeField]
    private Image[] hungerIcons; // ����� ������ �迭
    [SerializeField]
    private Image[] healthIcons; // ü�� ������ �迭
    [SerializeField]
    private int foodRestoreAmount = 3; // ������ ȸ���ϴ� ������� ��

    private int currentHunger; // ���� �����
    private int currentHealth; // ���� ü��

    private float hungerTimer = 0f; // ����� Ÿ�̸�
    private float healthTimer = 0f; // ü�� Ÿ�̸�
    private float hungerDecreaseInterval = 5f; // ����� ���� ����
    private float healthDecreaseInterval = 5f; // ü�� ���� ����

    private bool isFoodSelected = false; // ������ ���õǾ����� ����

    void Start()
    {
        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
    }

    void Update()
    {
        hungerTimer += Time.deltaTime;

        if (hungerTimer >= hungerDecreaseInterval)
        {
            hungerTimer = 0f;
            DecreaseHunger();
        }

        if (currentHunger == 0)
        {
            healthTimer += Time.deltaTime;

            if (healthTimer >= healthDecreaseInterval)
            {
                healthTimer = 0f;
                DecreaseHealth();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            isFoodSelected = true;
        }

        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
        {
            UseFood();
        }
    }

    void DecreaseHunger()
    {
        if (currentHunger > 0)
        {
            currentHunger--;
            hungerIcons[currentHunger].enabled = false;
        }
    }

    void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            healthIcons[currentHealth].enabled = false;
        }
    }

    void UseFood()
    {
        int newHunger = currentHunger + foodRestoreAmount;
        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
        {
            hungerIcons[i].enabled = true;
        }
        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
        isFoodSelected = false;
    }
}

// ü�� + ����� ����

//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    public Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    public Image[] healthIcons; // ü�� ������ �迭

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 5f; // ����� ���� ����
//    private float healthDecreaseInterval = 5f; // ü�� ���� ����

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
//    }

//    void Update()
//    {
//        hungerTimer += Time.deltaTime;

//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//    }

//    void DecreaseHunger()
//    {
//        if (currentHunger > 0)
//        {
//            currentHunger--;
//            hungerIcons[currentHunger].enabled = false;
//        }
//    }

//    void DecreaseHealth()
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth--;
//            healthIcons[currentHealth].enabled = false;
//        }
//    }
//}
