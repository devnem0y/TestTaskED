using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;
    public static GameData Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<GameData>();
            if (_instance == null) _instance = new GameObject().AddComponent<GameData>();
            return _instance;
        }
    }

    private int fieldSize;
    public int FieldSize { get; set; }
    
    private bool isPause;
    public bool IsPause { get; set; }

    private void Awake()
    {
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }
}
