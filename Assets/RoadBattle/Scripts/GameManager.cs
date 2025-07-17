using RoadBattle;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Lane Configuration")]
    [SerializeField]
    private Lane[] currentLanes;
    [SerializeField]
    private Lane DefaultLane;

    [Header("Lane Configuration")]
    [SerializeField, Range(0f, 20f)]
    private float SpawnRate = 5f;
    [SerializeField]
    private float SpawnDistance = 200f;
    [SerializeField]
    private SpawningModel spawningModel;

    public static GameManager Instance { get; private set; }


    float spawnTimer = 0;
    float deltaTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        MainCar = FindAnyObjectByType<CarControllerScript>();
        spawnTimer = 0;
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;

        spawnTimer += deltaTime;
        if (spawnTimer >= SpawnRate)
        {
            spawnTimer = 0;
            spawningModel.SpawnRegularCarAt(SpawnDistance);
        }
    }

    public Lane SpwanLane
    {
        get
        {
            return DefaultLane;
        }
    }

    public Lane[] CurrentLanes
    {
        get
        {
            return currentLanes;
        }
    }

    public CarControllerScript MainCar
    {
        get;
        private set;
    }
}