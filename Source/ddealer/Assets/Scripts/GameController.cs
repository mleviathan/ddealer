using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float INCREMENTAL_DIFFICULTY = 0.1f;
    /// <summary>
    /// Time between the update of difficulty
    /// </summary>
    public const float SECONDS_BETWEEN_DIFFICULTY_UPDATE = 1F;
    public static GameController Instance { get => _instance; }
    private static GameController _instance;

    public bool GameOver { get; set; } = false;

    public float BackgroundScrollSpeed; // Speed at which the elements will move
    public float JumpForce; // Force used to jump both for player and enemy
    public float FloorScrollSpeed;
    public ScrollingBehaviour ScrollingBehaviour;

    [SerializeField]
    private Transform _obstacleSpawner; // Position where to spawn elements like buildings and obstacles
    [SerializeField]
    private Transform _customerSpawner; // Position where to spawn elements like buildings and obstacles
    [SerializeField]
    private GameObject[] _obstaclePatterns; // Prefabs of obstacles patterns
    [SerializeField]
    private GameObject _customer; // Prefab of a customer


    private int _backpack = 0;
    private int _score = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

#if !UNITY_EDITOR
        _backpack = AppData.Instance.BackpackLoad;
#else
        _backpack = 0;
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.SetBackpackText(_backpack);
            GameUIManager.Instance.SetScoreText(_score);
        }

        StartCoroutine("IncreaseDifficulty");
        StartCoroutine("SpawnObstacles");
        StartCoroutine("SpawnCustomers");
    }

    /// <summary>
    /// Create random obstacle pattern randomly sorting from ObstaclePatterns
    /// And spawning them in Spawner position (x, y)
    /// </summary>
    public void SpawnRandomObstacle()
    {
        if (_obstaclePatterns.Length > 0)
        {
            var rand = Random.Range(0, _obstaclePatterns.Length);

            GameObject instantiatedObstaclePattern = Instantiate(_obstaclePatterns[rand], new Vector3(_obstacleSpawner.position.x, _obstacleSpawner.position.y), Quaternion.identity, _obstacleSpawner);

            for (var i = 0; i < instantiatedObstaclePattern.transform.childCount; i++)
            {
                ScrollingBehaviour.AddObstacleObject(instantiatedObstaclePattern.transform.GetChild(i).gameObject);
            }
        }
    }

    public void SpawnRandomCustomer()
    {
        GameObject instantiatedCustomer = Instantiate(_customer, new Vector3(_customerSpawner.position.x, _customerSpawner.position.y), Quaternion.identity, _customerSpawner);

        ScrollingBehaviour.AddCustomerObject(instantiatedCustomer);
    }

    /// <summary>
    /// Selling action -> Remove one element from backpack and money value add to score
    /// </summary>
    public void AddScore()
    {
        _backpack -= 1;
        _score += 1;
        GameUIManager.Instance.SetBackpackText(_backpack);
        GameUIManager.Instance.SetScoreText(_score);
    }

    public void SetGameOver(bool gameOver)
    {
        this.GameOver = gameOver;
    }

    private IEnumerator IncreaseDifficulty()
    {
        for (; ; )
        {
            BackgroundScrollSpeed += -INCREMENTAL_DIFFICULTY;
            FloorScrollSpeed += -INCREMENTAL_DIFFICULTY;

            ScrollingBehaviour.OnIncreaseDifficulty?.Invoke();
            yield return new WaitForSeconds(SECONDS_BETWEEN_DIFFICULTY_UPDATE);
        }
    }

    private IEnumerator SpawnObstacles()
    {
        for (; ; )
        {
            this.SpawnRandomObstacle();
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator SpawnCustomers()
    {
        for (; ; )
        {
            this.SpawnRandomCustomer();
            yield return new WaitForSeconds(5f);
        }
    }
}
