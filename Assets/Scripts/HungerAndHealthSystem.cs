// 체력 + 배고픔 + 음식 회복 적용
using UnityEngine;
using UnityEngine.UI;


public class HungerAndHealthSystem : MonoBehaviour
{
    [SerializeField]
    private Image[] hungerIcons; // 배고픔 아이콘 배열
    [SerializeField]
    private Image[] healthIcons; // 체력 아이콘 배열
    [SerializeField]
    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양

    private int currentHunger; // 현재 배고픔
    private int currentHealth; // 현재 체력

    private float hungerTimer = 0f; // 배고픔 타이머
    private float healthTimer = 0f; // 체력 타이머
    private float hungerDecreaseInterval = 5f; // 배고픔 감소 간격
    private float healthDecreaseInterval = 5f; // 체력 감소 간격

    private bool isFoodSelected = false; // 음식이 선택되었는지 여부

    void Start()
    {
        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
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

        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
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

        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
        isFoodSelected = false;
    }
}

// 체력 + 배고픔 적용

//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    public Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    public Image[] healthIcons; // 체력 아이콘 배열

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 5f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 5f; // 체력 감소 간격

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
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
