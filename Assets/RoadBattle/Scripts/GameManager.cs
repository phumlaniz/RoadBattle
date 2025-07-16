using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Lane Configuration")]
    [SerializeField]
    private Lane[] CurrentLanes;
    [SerializeField]
    private Lane DefaultLane;
    public static GameManager Instance { get; private set; }

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

    public Lane SpwanLane
    {
        get
        {
            return DefaultLane;
        }
    }

}
