using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<GameManager>();
            if (_instance == null) _instance = new GameObject().AddComponent<GameManager>();
            return _instance;
        }
    }

    private int fieldSize;
    public int FieldSize { get; set; }

    private void Awake()
    {
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }
}
