//// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기 + 배고픔 풀일때, 체력 회복 + 게임오버 직접 불러오기 말고  UI 설정으로  ---- 9 누르면 빵 나오게 수정중 + 소리
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Drawing;

public class HungerAndHealthSystem : MonoBehaviour
{

    [SerializeField]
    private Image[] hungerIcons; // 배고픔 아이콘 배열
    [SerializeField]
    private Image[] healthIcons; // 체력 아이콘 배열
    [SerializeField]
    private int foodRestoreAmount = 4; // 음식이 회복하는 배고픔의 양
    [SerializeField]
    private int healthRestoreAmount = 1; // 배고픔이 최대일 때 회복하는 체력의 양
    [SerializeField]
    private float healthRestoreInterval = 1f; // 배고픔이 최대일 때 체력 회복 간격

    private int currentHunger; // 현재 배고픔
    private int currentHealth; // 현재 체력

    private float hungerTimer = 0f; // 배고픔 타이머
    private float healthTimer = 0f; // 체력 타이머
    private float hungerDecreaseInterval = 3f; // 배고픔 감소 간격
    private float healthDecreaseInterval = 1f; // 체력 감소 간격

    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
    private bool isGameOver = false; // 게임 오버 상태
    private bool isTakingDamage = false; // 적으로부터 피해를 받고 있는지 여부

    public GameObject gameOverUI; // 게임 오버 이미지 오브젝트

    // 애니메이터
    //public Animator animator;

    public AudioClip[] sounds; //소리 배열
    AudioSource audioSource;


    void Start()
    {

        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정

        // 게임 오버 UI 초기 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        // 오디오
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        // 게임 오버 상태일 경우 업데이트 중지
        if (isGameOver)
            return;

        hungerTimer += Time.deltaTime;

        // 배고픔 감소 간격에 도달했을 때 배고픔 감소
        if (hungerTimer >= hungerDecreaseInterval)
        {
            hungerTimer = 0f;
            DecreaseHunger();
        }

        // 배고픔이 0일 때 체력 감소
        if (currentHunger == 0)
        {
            healthTimer += Time.deltaTime;

            if (healthTimer >= healthDecreaseInterval)
            {
                healthTimer = 0f;
                DecreaseHealth();
            }
        }
        // 배고픔이 최대일 때 체력 회복
        else if (currentHunger == hungerIcons.Length) // 배고픔이 최대치일 때
        {
            healthTimer += Time.deltaTime;

            if (healthTimer >= healthRestoreInterval)
            {
                healthTimer = 0f;
                IncreaseHealth();
            }
        }
        // 음식 선택
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            isFoodSelected = true;
        }
        // 음식 사용
        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
        {
            UseFood();

            // 빵먹는 소리를 실행한다.
            audioSource.clip = sounds[0];
            audioSource.volume = 3f;
            audioSource.Play();
        }
        //if (isFoodSelected && Input.GetMouseButtonDown(1))
        //{
        //    animator.SetBool("YummyFood", true);
        //}
        //else
        //{
        //    animator.SetBool("YummyFood", false);
        //}
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthIcons[currentHealth].enabled = false;

            if (currentHealth <= 0)
            {
                GameOver();
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

            // 맞는 소리를 실행한다.
            audioSource.clip = sounds[1];
            audioSource.volume = 1f;
            audioSource.Play();

            if (currentHealth == 0)
            {
                GameOver();
            }
        }
    }

    void IncreaseHealth()
    {
        int newHealth = currentHealth + healthRestoreAmount;
        for (int i = currentHealth; i < Mathf.Min(newHealth, healthIcons.Length); i++)
        {
            healthIcons[i].enabled = true;
        }
        currentHealth = Mathf.Min(newHealth, healthIcons.Length);
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

    // 게임 오버가 되면 실행할 함수
    public void GameOver()
    {
        // 게임 오버 UI 오브젝트를 활성화한다.
        gameOverUI.SetActive(true);

        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춘다)
        Time.timeScale = 0;
    }


    //// 게임 다시 시작
    //void RetryGame()
    //{
    //    Time.timeScale = 1; // 게임 일시정지 해제
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    //}
    // 메인 메뉴 복귀
    void GoToMainMenu()
    {
        Time.timeScale = 1; // 게임 일시정지 해제
        SceneManager.LoadScene("StartScene"); // 메인 메뉴 씬으로 로드 (StartScene은 실제 메인 메뉴 씬의 이름으로 대체)
    }

    public void RestartGame()
    {
        // 업데이트 시간을 다시 1배율로 변경한다.
        Time.timeScale = 1.0f;

        //  현재 씬을 다시 시작한다.
        SceneManager.LoadScene(1);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 적과 접촉 시 피해를 받기 시작
            StartCoroutine(TakeDamageOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 적과 접촉이 끝나면 피해를 중단
            StopCoroutine(TakeDamageOverTime());
            isTakingDamage = false;
        }
    }

    private IEnumerator TakeDamageOverTime()
    {
        isTakingDamage = true;
        while (isTakingDamage && !isGameOver)
        {
            DecreaseHealth();
            yield return new WaitForSeconds(1f);
        }
    }
    // 폭발 효과음을 플레이하는 함수
    public void PlayExplosionSound()
    {
        audioSource.clip = sounds[1];
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
}

////// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기 + 배고픔 풀일때, 체력 회복 + 게임오버 직접 불러오기 말고  UI 설정으로  ---- 9 누르면 빵 나오게 수정중
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{

//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 4; // 음식이 회복하는 배고픔의 양
//    [SerializeField]
//    private int healthRestoreAmount = 1; // 배고픔이 최대일 때 회복하는 체력의 양
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // 배고픔이 최대일 때 체력 회복 간격

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 3f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태
//    private bool isTakingDamage = false; // 적으로부터 피해를 받고 있는지 여부

//    public GameObject gameOverUI; // 게임 오버 이미지 오브젝트


//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정

//        // 게임 오버 UI 초기 비활성화
//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        // 게임 오버 상태일 경우 업데이트 중지
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // 배고픔 감소 간격에 도달했을 때 배고픔 감소
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // 배고픔이 0일 때 체력 감소
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // 배고픔이 최대일 때 체력 회복
//        else if (currentHunger == hungerIcons.Length) // 배고픔이 최대치일 때
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // 음식 선택
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // 음식 사용
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
//        }
//    }
//    public void TakeDamage(int damage)
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth -= damage;
//            healthIcons[currentHealth].enabled = false;

//            if (currentHealth <= 0)
//            {
//                GameOver();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void IncreaseHealth()
//    {
//        int newHealth = currentHealth + healthRestoreAmount;
//        for (int i = currentHealth; i < Mathf.Min(newHealth, healthIcons.Length); i++)
//        {
//            healthIcons[i].enabled = true;
//        }
//        currentHealth = Mathf.Min(newHealth, healthIcons.Length);
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    // 게임 오버가 되면 실행할 함수
//    public void GameOver()
//    {
//        // 게임 오버 UI 오브젝트를 활성화한다.
//        gameOverUI.SetActive(true);

//        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춘다)
//        Time.timeScale = 0;
//    }


//    //// 게임 다시 시작
//    //void RetryGame()
//    //{
//    //    Time.timeScale = 1; // 게임 일시정지 해제
//    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
//    //}
//    // 메인 메뉴 복귀
//    void GoToMainMenu()
//    {
//        Time.timeScale = 1; // 게임 일시정지 해제
//        SceneManager.LoadScene("StartScene"); // 메인 메뉴 씬으로 로드 (StartScene은 실제 메인 메뉴 씬의 이름으로 대체)
//    }

//    public void RestartGame()
//    {
//        // 업데이트 시간을 다시 1배율로 변경한다.
//        Time.timeScale = 1.0f;

//        //  현재 씬을 다시 시작한다.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉 시 피해를 받기 시작
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉이 끝나면 피해를 중단
//            StopCoroutine(TakeDamageOverTime());
//            isTakingDamage = false;
//        }
//    }

//    private IEnumerator TakeDamageOverTime()
//    {
//        isTakingDamage = true;
//        while (isTakingDamage && !isGameOver)
//        {
//            DecreaseHealth();
//            yield return new WaitForSeconds(1f);
//        }
//    }
//}

////// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기 + 배고픔 풀일때, 체력 회복 + 게임오버 직접 불러오기 말고  UI 설정으로  -------------알파 타입 적용
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 4; // 음식이 회복하는 배고픔의 양
//    [SerializeField]
//    private int healthRestoreAmount = 1; // 배고픔이 최대일 때 회복하는 체력의 양
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // 배고픔이 최대일 때 체력 회복 간격

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 3f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태
//    private bool isTakingDamage = false; // 적으로부터 피해를 받고 있는지 여부

//    public GameObject gameOverUI; // 게임 오버 이미지 오브젝트

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정

//        // 게임 오버 UI 초기 비활성화
//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        // 게임 오버 상태일 경우 업데이트 중지
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // 배고픔 감소 간격에 도달했을 때 배고픔 감소
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // 배고픔이 0일 때 체력 감소
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // 배고픔이 최대일 때 체력 회복
//        else if (currentHunger == hungerIcons.Length) // 배고픔이 최대치일 때
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // 음식 선택
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // 음식 사용
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
//        }
//    }
//    public void TakeDamage(int damage)
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth -= damage;
//            healthIcons[currentHealth].enabled = false;

//            if (currentHealth <= 0)
//            {
//                GameOver();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void IncreaseHealth()
//    {
//        int newHealth = currentHealth + healthRestoreAmount;
//        for (int i = currentHealth; i < Mathf.Min(newHealth, healthIcons.Length); i++)
//        {
//            healthIcons[i].enabled = true;
//        }
//        currentHealth = Mathf.Min(newHealth, healthIcons.Length);
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    // 게임 오버가 되면 실행할 함수
//    public void GameOver()
//    {
//        // 게임 오버 UI 오브젝트를 활성화한다.
//        gameOverUI.SetActive(true);

//        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춘다)
//        Time.timeScale = 0;
//    }


//    //// 게임 다시 시작
//    //void RetryGame()
//    //{
//    //    Time.timeScale = 1; // 게임 일시정지 해제
//    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
//    //}
//    // 메인 메뉴 복귀
//    void GoToMainMenu()
//    {
//        Time.timeScale = 1; // 게임 일시정지 해제
//        SceneManager.LoadScene("StartScene"); // 메인 메뉴 씬으로 로드 (StartScene은 실제 메인 메뉴 씬의 이름으로 대체)
//    }

//    public void RestartGame()
//    {
//        // 업데이트 시간을 다시 1배율로 변경한다.
//        Time.timeScale = 1.0f;

//        //  현재 씬을 다시 시작한다.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉 시 피해를 받기 시작
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉이 끝나면 피해를 중단
//            StopCoroutine(TakeDamageOverTime());
//            isTakingDamage = false;
//        }
//    }

//    private IEnumerator TakeDamageOverTime()
//    {
//        isTakingDamage = true;
//        while (isTakingDamage && !isGameOver)
//        {
//            DecreaseHealth();
//            yield return new WaitForSeconds(1f);
//        }
//    }
//}

////// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기 + 배고픔 풀일때, 체력 회복 + 게임오버 사진
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 4; // 음식이 회복하는 배고픔의 양
//    [SerializeField]
//    private int healthRestoreAmount = 1; // 배고픔이 최대일 때 회복하는 체력의 양
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // 배고픔이 최대일 때 체력 회복 간격

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 3f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태
//    private bool isTakingDamage = false; // 적으로부터 피해를 받고 있는지 여부

//    private GameObject gameOverImage; // 게임 오버 이미지 오브젝트

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//    }

//    void Update()
//    {
//        // 게임 오버 상태일 경우 업데이트 중지
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // 배고픔 감소 간격에 도달했을 때 배고픔 감소
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // 배고픔이 0일 때 체력 감소
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // 배고픔이 최대일 때 체력 회복
//        else if (currentHunger == hungerIcons.Length) // 배고픔이 최대치일 때
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // 음식 선택
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // 음식 사용
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
//        }
//    }
//    public void TakeDamage(int damage)
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth -= damage;
//            healthIcons[currentHealth].enabled = false;

//            if (currentHealth <= 0)
//            {
//                GameOver();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void IncreaseHealth()
//    {
//        int newHealth = currentHealth + healthRestoreAmount;
//        for (int i = currentHealth; i < Mathf.Min(newHealth, healthIcons.Length); i++)
//        {
//            healthIcons[i].enabled = true;
//        }
//        currentHealth = Mathf.Min(newHealth, healthIcons.Length);
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources 폴더에서 게임 오버 이미지를 로드합니다.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas를 찾거나 생성합니다.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image 오브젝트를 생성하여 게임 오버 이미지를 설정합니다.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // 이미지 크기 설정
//            rectTransform.anchoredPosition = Vector2.zero; // 이미지 위치 설정

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));


//        }

//        Time.timeScale = 0; // 게임 일시정지
//    }


//// 게임 다시 시작
//void RetryGame()
//{
//    Time.timeScale = 1; // 게임 일시정지 해제
//    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
//}
//// 메인 메뉴 복귀
//void GoToMainMenu()
//{
//    Time.timeScale = 1; // 게임 일시정지 해제
//    SceneManager.LoadScene("StartScene"); // 메인 메뉴 씬으로 로드 (StartScene은 실제 메인 메뉴 씬의 이름으로 대체)
//}

//    public void RestartGame()
//    {
//        // 업데이트 시간을 다시 1배율로 변경한다.
//        Time.timeScale = 1.0f;

//        //  현재 씬을 다시 시작한다.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉 시 피해를 받기 시작
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉이 끝나면 피해를 중단
//            StopCoroutine(TakeDamageOverTime());
//            isTakingDamage = false;
//        }
//    }

//    private IEnumerator TakeDamageOverTime()
//    {
//        isTakingDamage = true;
//        while (isTakingDamage && !isGameOver)
//        {
//            DecreaseHealth();
//            yield return new WaitForSeconds(1f);
//        }
//    }
//}

////// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기 + 배고픔 풀일때, 체력 회복 + 게임오버 사진 + ESC 버튼 UI 코드로 짜려다가 실패
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 4; // 음식이 회복하는 배고픔의 양
//    [SerializeField]
//    private int healthRestoreAmount = 1; // 배고픔이 최대일 때 회복하는 체력의 양
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // 배고픔이 최대일 때 체력 회복 간격

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 3f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태
//    private bool isTakingDamage = false; // 적으로부터 피해를 받고 있는지 여부

//    private GameObject gameOverImage; // 게임 오버 이미지 오브젝트

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//    }

//    void Update()
//    {
//        // 게임 오버 상태일 경우 업데이트 중지
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // 배고픔 감소 간격에 도달했을 때 배고픔 감소
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // 배고픔이 0일 때 체력 감소
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // 배고픔이 최대일 때 체력 회복
//        else if (currentHunger == hungerIcons.Length) // 배고픔이 최대치일 때
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // 음식 선택
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // 음식 사용
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
//        }
//    }
//    public void TakeDamage(int damage)
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth -= damage;
//            healthIcons[currentHealth].enabled = false;

//            if (currentHealth <= 0)
//            {
//                GameOver();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void IncreaseHealth()
//    {
//        int newHealth = currentHealth + healthRestoreAmount;
//        for (int i = currentHealth; i < Mathf.Min(newHealth, healthIcons.Length); i++)
//        {
//            healthIcons[i].enabled = true;
//        }
//        currentHealth = Mathf.Min(newHealth, healthIcons.Length);
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources 폴더에서 게임 오버 이미지를 로드합니다.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas를 찾거나 생성합니다.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image 오브젝트를 생성하여 게임 오버 이미지를 설정합니다.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // 이미지 크기 설정
//            rectTransform.anchoredPosition = Vector2.zero; // 이미지 위치 설정

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));

//            // 버튼들 추가
//            AddButton("RetryButton", "Retry", new Vector2(0, -100), RetryGame, canvas.transform);
//            AddButton("MainMenuButton", "Main Menu", new Vector2(0, -300), GoToMainMenu, canvas.transform);
//        }

//        Time.timeScale = 0; // 게임 일시정지
//    }
//    // 버튼 추가
//    void AddButton(string name, string text, Vector2 position, UnityEngine.Events.UnityAction action, Transform parent)
//    {
//        GameObject buttonObject = new GameObject(name);
//        buttonObject.transform.SetParent(parent);

//        RectTransform rectTransform = buttonObject.AddComponent<RectTransform>();
//        rectTransform.sizeDelta = size;
//        rectTransform.anchoredPosition = position;

//        Button button = buttonObject.AddComponent<Button>();
//        button.onClick.AddListener(action);

//        Image image = buttonObject.AddComponent<Image>();
//        image.color = buttonColor;

//        GameObject textObject = new GameObject("Text");
//        textObject.transform.SetParent(buttonObject.transform);

//        RectTransform textRectTransform = textObject.AddComponent<RectTransform>();
//        textRectTransform.sizeDelta = size;
//        textRectTransform.anchoredPosition = Vector2.zero;

//        Text buttonText = textObject.AddComponent<Text>();
//        buttonText.text = text;
//        buttonText.alignment = TextAnchor.MiddleCenter;
//        buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
//        buttonText.color = textColor;
//    }
//    // 게임 다시 시작
//    void RetryGame()
//    {
//        Time.timeScale = 1; // 게임 일시정지 해제
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
//    }
//    // 메인 메뉴 복귀
//    void GoToMainMenu()
//    {
//        Time.timeScale = 1; // 게임 일시정지 해제
//        SceneManager.LoadScene("StartScene"); // 메인 메뉴 씬으로 로드 (StartScene은 실제 메인 메뉴 씬의 이름으로 대체)
//    }

//    public void RestartGame()
//    {
//        // 업데이트 시간을 다시 1배율로 변경한다.
//        Time.timeScale = 1.0f;

//        //  현재 씬을 다시 시작한다.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉 시 피해를 받기 시작
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉이 끝나면 피해를 중단
//            StopCoroutine(TakeDamageOverTime());
//            isTakingDamage = false;
//        }
//    }

//    private IEnumerator TakeDamageOverTime()
//    {
//        isTakingDamage = true;
//        while (isTakingDamage && !isGameOver)
//        {
//            DecreaseHealth();
//            yield return new WaitForSeconds(1f);
//        }
//    }
//}



//// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기 +배고픔 풀일때, 체력 회복(안됨) + 게임오버 사진
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 2f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격
//    private float healthRecoverInterval = 5f; // 체력 회복 간격 (배고픔이 최대일 때)

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태

//    private GameObject gameOverImage; // 게임 오버 이미지 오브젝트

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1))
//        {
//            UseFood();
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth -= damage;
//            healthIcons[currentHealth].enabled = false;

//            if (currentHealth <= 0)
//            {
//                GameOver();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }
//    void RecoverHealth()
//    {
//        if (currentHealth < healthIcons.Length)
//        {
//            currentHealth++;
//            healthIcons[currentHealth - 1].enabled = true;
//        }
//    }
//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다. 
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources 폴더에서 게임 오버 이미지를 로드합니다.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas를 찾거나 생성합니다.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image 오브젝트를 생성하여 게임 오버 이미지를 설정합니다.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // 이미지 크기 설정
//            rectTransform.anchoredPosition = Vector2.zero; // 이미지 위치 설정

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0; // 게임 일시정지
//    }
//}

//// 적이 공격성공 하지만(( 배고픔이 최대인데 체력 안오름
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons;
//    [SerializeField]
//    private Image[] healthIcons;
//    [SerializeField]
//    private int foodRestoreAmount = 3;


//    private int currentHunger;
//    private int currentHealth;

//    private float hungerTimer = 0f;
//    private float healthTimer = 0f;
//    private float hungerDecreaseInterval = 2f;
//    private float healthDecreaseInterval = 1f;

//    private bool isFoodSelected = false;
//    private bool isGameOver = false;

//    private GameObject gameOverImage;

//    void Start()
//    {
//        currentHunger = hungerIcons.Length;
//        currentHealth = healthIcons.Length;
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1))
//        {
//            UseFood();
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        if (currentHealth > 0)
//        {
//            currentHealth -= damage;
//            healthIcons[currentHealth].enabled = false;

//            if (currentHealth <= 0)
//            {
//                GameOver();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);
//        isFoodSelected = true;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080);
//            rectTransform.anchoredPosition = Vector2.zero;

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0;
//    }
//}


////// 체력 + 배고픔 + 음식회복 + 사망적용 + 적 데미지 입기(안됨) + 배고픔 풀일때, 체력 회복

//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양
//    [SerializeField]
//    private int healthRestoreAmount = 1; // 배고픔이 최대일 때 회복하는 체력의 양
//    [SerializeField]
//    private float healthRestoreInterval = 5f; // 배고픔이 최대일 때 체력 회복 간격

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 2f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태
//    private bool isTakingDamage = false; // 적으로부터 피해를 받고 있는지 여부

//    private GameObject gameOverImage; // 게임 오버 이미지 오브젝트

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

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
//        else if (currentHunger == hungerIcons.Length) // 배고픔이 최대치일 때
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void IncreaseHealth()
//    {
//        int newHealth = currentHealth + healthRestoreAmount;
//        for (int i = currentHealth; i < Mathf.Min(newHealth, healthIcons.Length); i++)
//        {
//            healthIcons[i].enabled = true;
//        }
//        currentHealth = Mathf.Min(newHealth, healthIcons.Length);
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources 폴더에서 게임 오버 이미지를 로드합니다.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas를 찾거나 생성합니다.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image 오브젝트를 생성하여 게임 오버 이미지를 설정합니다.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // 이미지 크기 설정
//            rectTransform.anchoredPosition = Vector2.zero; // 이미지 위치 설정

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0; // 게임 일시정지
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉 시 피해를 받기 시작
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // 적과 접촉이 끝나면 피해를 중단
//            StopCoroutine(TakeDamageOverTime());
//            isTakingDamage = false;
//        }
//    }

//    private IEnumerator TakeDamageOverTime()
//    {
//        isTakingDamage = true;
//        while (isTakingDamage && !isGameOver)
//        {
//            DecreaseHealth();
//            yield return new WaitForSeconds(1f);
//        }
//    }
//}



//// 체력 + 배고픔 + 음식회복 + 사망적용 
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 2f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태

//    private GameObject gameOverImage; // 게임 오버 이미지 오브젝트

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다. ---------를 하니까 9번 놓고 가만히 있는 상태에서 또 마우스 우클릭을 해도 음식으로 인식을 못해서 배고픔 회복이 안됨.그래서 true로 바꿨는데 true로 하니까 한번 음식 먹고나면 그 다음엔 아무 키 눌러도 다 우클릭만 하면 음식 사용 가능(배고픔 회복)
//        isFoodSelected = true;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources 폴더에서 게임 오버 이미지를 로드합니다.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas를 찾거나 생성합니다.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image 오브젝트를 생성하여 게임 오버 이미지를 설정합니다.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // 이미지 크기 설정
//            rectTransform.anchoredPosition = Vector2.zero; // 이미지 위치 설정

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0; // 게임 일시정지
//    }

//}


//// 체력 + 배고픔 + 음식회복 + 사망적용 (되긴한데 애매함)
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private GameObject gameOverPanel; // 게임 오버 패널
//    [SerializeField]
//    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 0.11f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 0.1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//        gameOverPanel.SetActive(false); // 게임 오버 패널 숨김
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;
//        gameOverPanel.SetActive(true); // 게임 오버 패널 표시
//        Time.timeScale = 0; // 게임 일시정지
//    }
//}


//// 체력 + 배고픔 + 음식 회복 + 사망 적용 (안됨)
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private Image gameOverText; // 게임 오버 텍스트
//    [SerializeField]
//    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 1f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 1f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
//    private bool isGameOver = false; // 게임 오버 상태

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
//        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
//        gameOverText.gameObject.SetActive(false); // 게임 오버 텍스트 숨김
//    }

//    void Update()
//    {
//        if (isGameOver)
//            return;

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
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

//            if (currentHealth == 0)
//            {
//                GameOver();
//            }
//        }
//    }

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;
//        gameOverText.gameObject.SetActive(true); // 게임 오버 텍스트 표시
//        Time.timeScale = 0; // 게임 일시정지
//    }
//}

//// 체력 + 배고픔 + 음식 회복 적용
//using UnityEngine;
//using UnityEngine.UI;


//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // 배고픔 아이콘 배열
//    [SerializeField]
//    private Image[] healthIcons; // 체력 아이콘 배열
//    [SerializeField]
//    private int foodRestoreAmount = 3; // 음식이 회복하는 배고픔의 양

//    private int currentHunger; // 현재 배고픔
//    private int currentHealth; // 현재 체력

//    private float hungerTimer = 0f; // 배고픔 타이머
//    private float healthTimer = 0f; // 체력 타이머
//    private float hungerDecreaseInterval = 5f; // 배고픔 감소 간격
//    private float healthDecreaseInterval = 5f; // 체력 감소 간격

//    private bool isFoodSelected = false; // 음식이 선택되었는지 여부

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
//        {
//            UseFood();
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

//    void UseFood()
//    {
//        int newHunger = currentHunger + foodRestoreAmount;
//        for (int i = currentHunger; i < Mathf.Min(newHunger, hungerIcons.Length); i++)
//        {
//            hungerIcons[i].enabled = true;
//        }
//        currentHunger = Mathf.Min(newHunger, hungerIcons.Length);

//        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다.
//        isFoodSelected = false;
//    }
//}

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
