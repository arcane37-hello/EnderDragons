using UnityEngine;
using UnityEngine.UI;

public class HungerAndHealthSystem : MonoBehaviour
{
    [SerializeField]
    public Image[] hungerIcons; // 배고픔 아이콘 배열
    [SerializeField]
    public Image[] healthIcons; // 체력 아이콘 배열

    private int currentHunger; // 현재 배고픔
    private int currentHealth; // 현재 체력

    private float hungerTimer = 0f; // 배고픔 타이머
    private float healthTimer = 0f; // 체력 타이머
    private float hungerDecreaseInterval = 5f; // 배고픔 감소 간격
    private float healthDecreaseInterval = 5f; // 체력 감소 간격

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
}
