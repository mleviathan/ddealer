using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get => _instance; }
    private static GameController _instance;

    public bool GameOver { get; set; } = false;

    public float ScrollSpeed; // Speed at which the elements will move
    public float JumpForce; // Force used to jump both for player and enemy

    [SerializeField]
    private Transform _obstacleSpawner; // Position where to spawn elements like buildings and obstacles
    [SerializeField]
    private Transform _buildingSpawner; // Position where to spawn elements like buildings and obstacles
    [SerializeField]
    private GameObject[] _obstaclePatterns; // Prefabs of obstacles patterns
    [SerializeField]
    public GameObject[] _buildingPatterns; // Prefabs of buildings patterns
    [SerializeField]
    private GameObject _customer; // Prefab of a customer
    private int _backpack = 999;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        GameUIManager.Instance.SetBackpackText(_backpack);
        GameUIManager.Instance.SetScoreText(_score);
    }

    // Update is called once per frame
    void Update()
    {
        CheckClickOnCustomer();
    }

    /// <summary>
    /// TO-DO: find a way to attach the script to the Customer object?
    /// </summary>
    private void CheckClickOnCustomer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var clickPos = Input.mousePosition;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(clickPos).x, Camera.main.ScreenToWorldPoint(clickPos).y), Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.CompareTag("Customer"))
                {
                    Destroy(hit.collider.gameObject);
                    _backpack -= 1;
                    _score += 1;
                    GameUIManager.Instance.SetBackpackText(_backpack);
                    GameUIManager.Instance.SetScoreText(_score);
                }
            }
        }
    }

    /// <summary>
    /// Create random obstacle pattern randomly sorting from ObstaclePatterns
    /// And spawning them in Spawner position (x, y)
    /// </summary>
    public void SpawnRandomObstacle()
    {
        var rand = Random.Range(0, _obstaclePatterns.Length);

        Instantiate(_obstaclePatterns[rand], new Vector3(_obstacleSpawner.position.x, _obstacleSpawner.position.y), Quaternion.identity);
    }

    /// <summary>
    /// Create random obstacles randomly sorting from BuildingPatterns
    /// And spawning them in Spawner position (x, y)
    /// Also create some customers in its windows
    /// </summary>
    public void SpawnRandomBuilding()
    {
        var rand = Random.Range(0, _buildingPatterns.Length);
        GameObject buildingPattern = _buildingPatterns[rand];
        var patternGO = Instantiate(buildingPattern, new Vector3(_buildingSpawner.position.x, _buildingSpawner.position.y), Quaternion.identity);

        var buildingQty = patternGO.transform.childCount;
        int customersSpawned = 0;

        for (var bui = 0; bui < buildingQty; bui++)
        {
            Transform building = patternGO.transform.GetChild(bui);
            int windowsQty = building.childCount;

            for (var win = 0; win < windowsQty; win++)
            {
                Transform window = building.transform.GetChild(win);
                bool spawn = Random.Range(0, 2) == 1 ? true : false; // "Dice roll" to estabilish if in this window will be spawned a customer

                if (spawn)
                {
                    Instantiate(_customer, new Vector3(window.position.x, window.position.y), Quaternion.identity, window);
                    customersSpawned++;
                }
            }
        }
    }

    /// <summary>
    /// Seeling action -> Remove one element from backpack and money value add to score
    /// </summary>
    public void AddScore()
    {
        _backpack -= 1;
        _score += 1;
        GameUIManager.Instance.SetBackpackText(_backpack);
        GameUIManager.Instance.SetScoreText(_score);
    }

}
