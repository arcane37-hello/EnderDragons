// ü�� + ����� + ����ȸ�� + ������� 
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
    private float hungerDecreaseInterval = 2f; // ����� ���� ����
    private float healthDecreaseInterval = 1f; // ü�� ���� ����

    private bool isFoodSelected = false; // ������ ���õǾ����� ����
    private bool isGameOver = false; // ���� ���� ����

    private GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

    void Start()
    {
        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
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

        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�. ---------�� �ϴϱ� 9�� ���� ������ �ִ� ���¿��� �� ���콺 ��Ŭ���� �ص� �������� �ν��� ���ؼ� ����� ȸ���� �ȵ�.�׷��� true�� �ٲ�µ� true�� �ϴϱ� �ѹ� ���� �԰��� �� ������ �ƹ� Ű ������ �� ��Ŭ���� �ϸ� ���� ��� ����(����� ȸ��)
        isFoodSelected = true;
    }

    void GameOver()
    {
        isGameOver = true;

        // Resources �������� ���� ���� �̹����� �ε��մϴ�.
        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
        if (gameOverTexture != null)
        {
            // Canvas�� ã�ų� �����մϴ�.
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObject = new GameObject("Canvas");
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObject.AddComponent<CanvasScaler>();
                canvasObject.AddComponent<GraphicRaycaster>();
            }

            // Image ������Ʈ�� �����Ͽ� ���� ���� �̹����� �����մϴ�.
            gameOverImage = new GameObject("GameOverImage");
            gameOverImage.transform.SetParent(canvas.transform);
            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(1920, 1080); // �̹��� ũ�� ����
            rectTransform.anchoredPosition = Vector2.zero; // �̹��� ��ġ ����

            Image image = gameOverImage.AddComponent<Image>();
            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
        }

        Time.timeScale = 0; // ���� �Ͻ�����
    }
}


//// ü�� + ����� + ����ȸ�� + ������� (�Ǳ��ѵ� �ָ���)
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private GameObject gameOverPanel; // ���� ���� �г�
//    [SerializeField]
//    private int foodRestoreAmount = 3; // ������ ȸ���ϴ� ������� ��

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 0.11f; // ����� ���� ����
//    private float healthDecreaseInterval = 0.1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
//        gameOverPanel.SetActive(false); // ���� ���� �г� ����
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

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;
//        gameOverPanel.SetActive(true); // ���� ���� �г� ǥ��
//        Time.timeScale = 0; // ���� �Ͻ�����
//    }
//}


//// ü�� + ����� + ���� ȸ�� + ��� ���� (�ȵ�)
//using UnityEngine;
//using UnityEngine.UI;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private Image gameOverText; // ���� ���� �ؽ�Ʈ
//    [SerializeField]
//    private int foodRestoreAmount = 3; // ������ ȸ���ϴ� ������� ��

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 1f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
//        gameOverText.gameObject.SetActive(false); // ���� ���� �ؽ�Ʈ ����
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

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;
//        gameOverText.gameObject.SetActive(true); // ���� ���� �ؽ�Ʈ ǥ��
//        Time.timeScale = 0; // ���� �Ͻ�����
//    }
//}

//// ü�� + ����� + ���� ȸ�� ����
//using UnityEngine;
//using UnityEngine.UI;


//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private int foodRestoreAmount = 3; // ������ ȸ���ϴ� ������� ��

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 5f; // ����� ���� ����
//    private float healthDecreaseInterval = 5f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����

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

//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }

//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }
//}

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
