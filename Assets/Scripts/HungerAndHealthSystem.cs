// 체력 + 배고픔 + 음식회복 + 사망적용 
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
    private float hungerDecreaseInterval = 2f; // 배고픔 감소 간격
    private float healthDecreaseInterval = 1f; // 체력 감소 간격

    private bool isFoodSelected = false; // 음식이 선택되었는지 여부
    private bool isGameOver = false; // 게임 오버 상태

    private GameObject gameOverImage; // 게임 오버 이미지 오브젝트

    void Start()
    {
        currentHunger = hungerIcons.Length; // 초기 배고픔을 최대값으로 설정
        currentHealth = healthIcons.Length; // 초기 체력을 최대값으로 설정
    }

    void Update()
    {
        if (isGameOver)
            return;

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

            if (currentHealth == 0)
            {
                GameOver();
            }
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

        // 음식을 사용한 후 음식을 선택되지 않은 상태로 만듭니다. ---------를 하니까 9번 놓고 가만히 있는 상태에서 또 마우스 우클릭을 해도 음식으로 인식을 못해서 배고픔 회복이 안됨.그래서 true로 바꿨는데 true로 하니까 한번 음식 먹고나면 그 다음엔 아무 키 눌러도 다 우클릭만 하면 음식 사용 가능(배고픔 회복)
        isFoodSelected = true;
    }

    void GameOver()
    {
        isGameOver = true;

        // Resources 폴더에서 게임 오버 이미지를 로드합니다.
        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
        if (gameOverTexture != null)
        {
            // Canvas를 찾거나 생성합니다.
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObject = new GameObject("Canvas");
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObject.AddComponent<CanvasScaler>();
                canvasObject.AddComponent<GraphicRaycaster>();
            }

            // Image 오브젝트를 생성하여 게임 오버 이미지를 설정합니다.
            gameOverImage = new GameObject("GameOverImage");
            gameOverImage.transform.SetParent(canvas.transform);
            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(1920, 1080); // 이미지 크기 설정
            rectTransform.anchoredPosition = Vector2.zero; // 이미지 위치 설정

            Image image = gameOverImage.AddComponent<Image>();
            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
        }

        Time.timeScale = 0; // 게임 일시정지
    }
}


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
