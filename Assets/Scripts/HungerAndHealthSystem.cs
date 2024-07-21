//// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա� + ����� Ǯ�϶�, ü�� ȸ�� + ���ӿ��� ���� �ҷ����� ����  UI ��������  ---- 9 ������ �� ������ ������ + �Ҹ�
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Drawing;

public class HungerAndHealthSystem : MonoBehaviour
{

    [SerializeField]
    private Image[] hungerIcons; // ����� ������ �迭
    [SerializeField]
    private Image[] healthIcons; // ü�� ������ �迭
    [SerializeField]
    private int foodRestoreAmount = 4; // ������ ȸ���ϴ� ������� ��
    [SerializeField]
    private int healthRestoreAmount = 1; // ������� �ִ��� �� ȸ���ϴ� ü���� ��
    [SerializeField]
    private float healthRestoreInterval = 1f; // ������� �ִ��� �� ü�� ȸ�� ����

    private int currentHunger; // ���� �����
    private int currentHealth; // ���� ü��

    private float hungerTimer = 0f; // ����� Ÿ�̸�
    private float healthTimer = 0f; // ü�� Ÿ�̸�
    private float hungerDecreaseInterval = 3f; // ����� ���� ����
    private float healthDecreaseInterval = 1f; // ü�� ���� ����

    private bool isFoodSelected = false; // ������ ���õǾ����� ����
    private bool isGameOver = false; // ���� ���� ����
    private bool isTakingDamage = false; // �����κ��� ���ظ� �ް� �ִ��� ����

    public GameObject gameOverUI; // ���� ���� �̹��� ������Ʈ

    // �ִϸ�����
    //public Animator animator;

    public AudioClip[] sounds; //�Ҹ� �迭
    AudioSource audioSource;


    void Start()
    {

        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����

        // ���� ���� UI �ʱ� ��Ȱ��ȭ
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        // �����
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���� ���� ������ ��� ������Ʈ ����
        if (isGameOver)
            return;

        hungerTimer += Time.deltaTime;

        // ����� ���� ���ݿ� �������� �� ����� ����
        if (hungerTimer >= hungerDecreaseInterval)
        {
            hungerTimer = 0f;
            DecreaseHunger();
        }

        // ������� 0�� �� ü�� ����
        if (currentHunger == 0)
        {
            healthTimer += Time.deltaTime;

            if (healthTimer >= healthDecreaseInterval)
            {
                healthTimer = 0f;
                DecreaseHealth();
            }
        }
        // ������� �ִ��� �� ü�� ȸ��
        else if (currentHunger == hungerIcons.Length) // ������� �ִ�ġ�� ��
        {
            healthTimer += Time.deltaTime;

            if (healthTimer >= healthRestoreInterval)
            {
                healthTimer = 0f;
                IncreaseHealth();
            }
        }
        // ���� ����
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            isFoodSelected = true;
        }
        // ���� ���
        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
        {
            UseFood();

            // ���Դ� �Ҹ��� �����Ѵ�.
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

            // �´� �Ҹ��� �����Ѵ�.
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

        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
        isFoodSelected = false;
    }

    // ���� ������ �Ǹ� ������ �Լ�
    public void GameOver()
    {
        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
        gameOverUI.SetActive(true);

        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� �����)
        Time.timeScale = 0;
    }


    //// ���� �ٽ� ����
    //void RetryGame()
    //{
    //    Time.timeScale = 1; // ���� �Ͻ����� ����
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
    //}
    // ���� �޴� ����
    void GoToMainMenu()
    {
        Time.timeScale = 1; // ���� �Ͻ����� ����
        SceneManager.LoadScene("StartScene"); // ���� �޴� ������ �ε� (StartScene�� ���� ���� �޴� ���� �̸����� ��ü)
    }

    public void RestartGame()
    {
        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
        Time.timeScale = 1.0f;

        //  ���� ���� �ٽ� �����Ѵ�.
        SceneManager.LoadScene(1);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // ���� ���� �� ���ظ� �ޱ� ����
            StartCoroutine(TakeDamageOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // ���� ������ ������ ���ظ� �ߴ�
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
    // ���� ȿ������ �÷����ϴ� �Լ�
    public void PlayExplosionSound()
    {
        audioSource.clip = sounds[1];
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
}

////// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա� + ����� Ǯ�϶�, ü�� ȸ�� + ���ӿ��� ���� �ҷ����� ����  UI ��������  ---- 9 ������ �� ������ ������
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{

//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private int foodRestoreAmount = 4; // ������ ȸ���ϴ� ������� ��
//    [SerializeField]
//    private int healthRestoreAmount = 1; // ������� �ִ��� �� ȸ���ϴ� ü���� ��
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // ������� �ִ��� �� ü�� ȸ�� ����

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 3f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����
//    private bool isTakingDamage = false; // �����κ��� ���ظ� �ް� �ִ��� ����

//    public GameObject gameOverUI; // ���� ���� �̹��� ������Ʈ


//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����

//        // ���� ���� UI �ʱ� ��Ȱ��ȭ
//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        // ���� ���� ������ ��� ������Ʈ ����
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // ����� ���� ���ݿ� �������� �� ����� ����
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // ������� 0�� �� ü�� ����
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // ������� �ִ��� �� ü�� ȸ��
//        else if (currentHunger == hungerIcons.Length) // ������� �ִ�ġ�� ��
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // ���� ����
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // ���� ���
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    // ���� ������ �Ǹ� ������ �Լ�
//    public void GameOver()
//    {
//        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
//        gameOverUI.SetActive(true);

//        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� �����)
//        Time.timeScale = 0;
//    }


//    //// ���� �ٽ� ����
//    //void RetryGame()
//    //{
//    //    Time.timeScale = 1; // ���� �Ͻ����� ����
//    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
//    //}
//    // ���� �޴� ����
//    void GoToMainMenu()
//    {
//        Time.timeScale = 1; // ���� �Ͻ����� ����
//        SceneManager.LoadScene("StartScene"); // ���� �޴� ������ �ε� (StartScene�� ���� ���� �޴� ���� �̸����� ��ü)
//    }

//    public void RestartGame()
//    {
//        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
//        Time.timeScale = 1.0f;

//        //  ���� ���� �ٽ� �����Ѵ�.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ���� �� ���ظ� �ޱ� ����
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ������ ������ ���ظ� �ߴ�
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

////// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա� + ����� Ǯ�϶�, ü�� ȸ�� + ���ӿ��� ���� �ҷ����� ����  UI ��������  -------------���� Ÿ�� ����
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private int foodRestoreAmount = 4; // ������ ȸ���ϴ� ������� ��
//    [SerializeField]
//    private int healthRestoreAmount = 1; // ������� �ִ��� �� ȸ���ϴ� ü���� ��
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // ������� �ִ��� �� ü�� ȸ�� ����

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 3f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����
//    private bool isTakingDamage = false; // �����κ��� ���ظ� �ް� �ִ��� ����

//    public GameObject gameOverUI; // ���� ���� �̹��� ������Ʈ

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����

//        // ���� ���� UI �ʱ� ��Ȱ��ȭ
//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        // ���� ���� ������ ��� ������Ʈ ����
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // ����� ���� ���ݿ� �������� �� ����� ����
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // ������� 0�� �� ü�� ����
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // ������� �ִ��� �� ü�� ȸ��
//        else if (currentHunger == hungerIcons.Length) // ������� �ִ�ġ�� ��
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // ���� ����
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // ���� ���
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    // ���� ������ �Ǹ� ������ �Լ�
//    public void GameOver()
//    {
//        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
//        gameOverUI.SetActive(true);

//        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� �����)
//        Time.timeScale = 0;
//    }


//    //// ���� �ٽ� ����
//    //void RetryGame()
//    //{
//    //    Time.timeScale = 1; // ���� �Ͻ����� ����
//    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
//    //}
//    // ���� �޴� ����
//    void GoToMainMenu()
//    {
//        Time.timeScale = 1; // ���� �Ͻ����� ����
//        SceneManager.LoadScene("StartScene"); // ���� �޴� ������ �ε� (StartScene�� ���� ���� �޴� ���� �̸����� ��ü)
//    }

//    public void RestartGame()
//    {
//        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
//        Time.timeScale = 1.0f;

//        //  ���� ���� �ٽ� �����Ѵ�.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ���� �� ���ظ� �ޱ� ����
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ������ ������ ���ظ� �ߴ�
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

////// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա� + ����� Ǯ�϶�, ü�� ȸ�� + ���ӿ��� ����
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private int foodRestoreAmount = 4; // ������ ȸ���ϴ� ������� ��
//    [SerializeField]
//    private int healthRestoreAmount = 1; // ������� �ִ��� �� ȸ���ϴ� ü���� ��
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // ������� �ִ��� �� ü�� ȸ�� ����

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 3f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����
//    private bool isTakingDamage = false; // �����κ��� ���ظ� �ް� �ִ��� ����

//    private GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
//    }

//    void Update()
//    {
//        // ���� ���� ������ ��� ������Ʈ ����
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // ����� ���� ���ݿ� �������� �� ����� ����
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // ������� 0�� �� ü�� ����
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // ������� �ִ��� �� ü�� ȸ��
//        else if (currentHunger == hungerIcons.Length) // ������� �ִ�ġ�� ��
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // ���� ����
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // ���� ���
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources �������� ���� ���� �̹����� �ε��մϴ�.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas�� ã�ų� �����մϴ�.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image ������Ʈ�� �����Ͽ� ���� ���� �̹����� �����մϴ�.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // �̹��� ũ�� ����
//            rectTransform.anchoredPosition = Vector2.zero; // �̹��� ��ġ ����

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));


//        }

//        Time.timeScale = 0; // ���� �Ͻ�����
//    }


//// ���� �ٽ� ����
//void RetryGame()
//{
//    Time.timeScale = 1; // ���� �Ͻ����� ����
//    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
//}
//// ���� �޴� ����
//void GoToMainMenu()
//{
//    Time.timeScale = 1; // ���� �Ͻ����� ����
//    SceneManager.LoadScene("StartScene"); // ���� �޴� ������ �ε� (StartScene�� ���� ���� �޴� ���� �̸����� ��ü)
//}

//    public void RestartGame()
//    {
//        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
//        Time.timeScale = 1.0f;

//        //  ���� ���� �ٽ� �����Ѵ�.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ���� �� ���ظ� �ޱ� ����
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ������ ������ ���ظ� �ߴ�
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

////// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա� + ����� Ǯ�϶�, ü�� ȸ�� + ���ӿ��� ���� + ESC ��ư UI �ڵ�� ¥���ٰ� ����
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.Drawing;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private int foodRestoreAmount = 4; // ������ ȸ���ϴ� ������� ��
//    [SerializeField]
//    private int healthRestoreAmount = 1; // ������� �ִ��� �� ȸ���ϴ� ü���� ��
//    [SerializeField]
//    private float healthRestoreInterval = 1f; // ������� �ִ��� �� ü�� ȸ�� ����

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 3f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����
//    private bool isTakingDamage = false; // �����κ��� ���ظ� �ް� �ִ��� ����

//    private GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
//    }

//    void Update()
//    {
//        // ���� ���� ������ ��� ������Ʈ ����
//        if (isGameOver)
//            return;

//        hungerTimer += Time.deltaTime;

//        // ����� ���� ���ݿ� �������� �� ����� ����
//        if (hungerTimer >= hungerDecreaseInterval)
//        {
//            hungerTimer = 0f;
//            DecreaseHunger();
//        }

//        // ������� 0�� �� ü�� ����
//        if (currentHunger == 0)
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthDecreaseInterval)
//            {
//                healthTimer = 0f;
//                DecreaseHealth();
//            }
//        }
//        // ������� �ִ��� �� ü�� ȸ��
//        else if (currentHunger == hungerIcons.Length) // ������� �ִ�ġ�� ��
//        {
//            healthTimer += Time.deltaTime;

//            if (healthTimer >= healthRestoreInterval)
//            {
//                healthTimer = 0f;
//                IncreaseHealth();
//            }
//        }
//        // ���� ����
//        if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            isFoodSelected = true;
//        }
//        // ���� ���
//        if (isFoodSelected && Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources �������� ���� ���� �̹����� �ε��մϴ�.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas�� ã�ų� �����մϴ�.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image ������Ʈ�� �����Ͽ� ���� ���� �̹����� �����մϴ�.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // �̹��� ũ�� ����
//            rectTransform.anchoredPosition = Vector2.zero; // �̹��� ��ġ ����

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));

//            // ��ư�� �߰�
//            AddButton("RetryButton", "Retry", new Vector2(0, -100), RetryGame, canvas.transform);
//            AddButton("MainMenuButton", "Main Menu", new Vector2(0, -300), GoToMainMenu, canvas.transform);
//        }

//        Time.timeScale = 0; // ���� �Ͻ�����
//    }
//    // ��ư �߰�
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
//    // ���� �ٽ� ����
//    void RetryGame()
//    {
//        Time.timeScale = 1; // ���� �Ͻ����� ����
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
//    }
//    // ���� �޴� ����
//    void GoToMainMenu()
//    {
//        Time.timeScale = 1; // ���� �Ͻ����� ����
//        SceneManager.LoadScene("StartScene"); // ���� �޴� ������ �ε� (StartScene�� ���� ���� �޴� ���� �̸����� ��ü)
//    }

//    public void RestartGame()
//    {
//        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
//        Time.timeScale = 1.0f;

//        //  ���� ���� �ٽ� �����Ѵ�.
//        SceneManager.LoadScene(1);

//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ���� �� ���ظ� �ޱ� ����
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ������ ������ ���ظ� �ߴ�
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



//// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա� +����� Ǯ�϶�, ü�� ȸ��(�ȵ�) + ���ӿ��� ����
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
//    private float hungerDecreaseInterval = 2f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����
//    private float healthRecoverInterval = 5f; // ü�� ȸ�� ���� (������� �ִ��� ��)

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����

//    private GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�. 
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources �������� ���� ���� �̹����� �ε��մϴ�.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas�� ã�ų� �����մϴ�.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image ������Ʈ�� �����Ͽ� ���� ���� �̹����� �����մϴ�.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // �̹��� ũ�� ����
//            rectTransform.anchoredPosition = Vector2.zero; // �̹��� ��ġ ����

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0; // ���� �Ͻ�����
//    }
//}

//// ���� ���ݼ��� ������(( ������� �ִ��ε� ü�� �ȿ���
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


////// ü�� + ����� + ����ȸ�� + ������� + �� ������ �Ա�(�ȵ�) + ����� Ǯ�϶�, ü�� ȸ��

//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//public class HungerAndHealthSystem : MonoBehaviour
//{
//    [SerializeField]
//    private Image[] hungerIcons; // ����� ������ �迭
//    [SerializeField]
//    private Image[] healthIcons; // ü�� ������ �迭
//    [SerializeField]
//    private int foodRestoreAmount = 3; // ������ ȸ���ϴ� ������� ��
//    [SerializeField]
//    private int healthRestoreAmount = 1; // ������� �ִ��� �� ȸ���ϴ� ü���� ��
//    [SerializeField]
//    private float healthRestoreInterval = 5f; // ������� �ִ��� �� ü�� ȸ�� ����

//    private int currentHunger; // ���� �����
//    private int currentHealth; // ���� ü��

//    private float hungerTimer = 0f; // ����� Ÿ�̸�
//    private float healthTimer = 0f; // ü�� Ÿ�̸�
//    private float hungerDecreaseInterval = 2f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����
//    private bool isTakingDamage = false; // �����κ��� ���ظ� �ް� �ִ��� ����

//    private GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
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
//        else if (currentHunger == hungerIcons.Length) // ������� �ִ�ġ�� ��
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�.
//        isFoodSelected = false;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources �������� ���� ���� �̹����� �ε��մϴ�.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas�� ã�ų� �����մϴ�.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image ������Ʈ�� �����Ͽ� ���� ���� �̹����� �����մϴ�.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // �̹��� ũ�� ����
//            rectTransform.anchoredPosition = Vector2.zero; // �̹��� ��ġ ����

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0; // ���� �Ͻ�����
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ���� �� ���ظ� �ޱ� ����
//            StartCoroutine(TakeDamageOverTime());
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            // ���� ������ ������ ���ظ� �ߴ�
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



//// ü�� + ����� + ����ȸ�� + ������� 
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
//    private float hungerDecreaseInterval = 2f; // ����� ���� ����
//    private float healthDecreaseInterval = 1f; // ü�� ���� ����

//    private bool isFoodSelected = false; // ������ ���õǾ����� ����
//    private bool isGameOver = false; // ���� ���� ����

//    private GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

//    void Start()
//    {
//        currentHunger = hungerIcons.Length; // �ʱ� ������� �ִ밪���� ����
//        currentHealth = healthIcons.Length; // �ʱ� ü���� �ִ밪���� ����
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

//        // ������ ����� �� ������ ���õ��� ���� ���·� ����ϴ�. ---------�� �ϴϱ� 9�� ���� ������ �ִ� ���¿��� �� ���콺 ��Ŭ���� �ص� �������� �ν��� ���ؼ� ����� ȸ���� �ȵ�.�׷��� true�� �ٲ�µ� true�� �ϴϱ� �ѹ� ���� �԰��� �� ������ �ƹ� Ű ������ �� ��Ŭ���� �ϸ� ���� ��� ����(����� ȸ��)
//        isFoodSelected = true;
//    }

//    void GameOver()
//    {
//        isGameOver = true;

//        // Resources �������� ���� ���� �̹����� �ε��մϴ�.
//        Texture2D gameOverTexture = Resources.Load<Texture2D>("GameOverImage");
//        if (gameOverTexture != null)
//        {
//            // Canvas�� ã�ų� �����մϴ�.
//            Canvas canvas = FindObjectOfType<Canvas>();
//            if (canvas == null)
//            {
//                GameObject canvasObject = new GameObject("Canvas");
//                canvas = canvasObject.AddComponent<Canvas>();
//                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//                canvasObject.AddComponent<CanvasScaler>();
//                canvasObject.AddComponent<GraphicRaycaster>();
//            }

//            // Image ������Ʈ�� �����Ͽ� ���� ���� �̹����� �����մϴ�.
//            gameOverImage = new GameObject("GameOverImage");
//            gameOverImage.transform.SetParent(canvas.transform);
//            RectTransform rectTransform = gameOverImage.AddComponent<RectTransform>();
//            rectTransform.sizeDelta = new Vector2(1920, 1080); // �̹��� ũ�� ����
//            rectTransform.anchoredPosition = Vector2.zero; // �̹��� ��ġ ����

//            Image image = gameOverImage.AddComponent<Image>();
//            image.sprite = Sprite.Create(gameOverTexture, new Rect(0, 0, gameOverTexture.width, gameOverTexture.height), new Vector2(0.5f, 0.5f));
//        }

//        Time.timeScale = 0; // ���� �Ͻ�����
//    }

//}


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
