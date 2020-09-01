using UnityEditor;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private int fieldSize;
    public int FieldSize { get; set; }
    
    private bool isPause;
    public bool IsPause { get; set; }
    private bool isSound;
    public bool IsSound 
    { 
        get { return isSound; }
        set 
        {
            isSound = value;
            AudioManager.instance.OffVolumeSounds(value);
            PlayerPrefs.SetInt("SoundVolume", value ? 1 : 0);
        }
    }
    public bool SettingsVolumeSounds 
    { 
        get 
        {
            if (PlayerPrefs.HasKey("SoundVolume")) return PlayerPrefs.GetInt("SoundVolume") == 1;

            PlayerPrefs.SetInt("SoundVolume", 1);
            return true;
        } 
    }

    private bool isMusic;
    public bool IsMusic 
    { 
        get { return isMusic; }
        set 
        {
            isMusic = value;
            AudioManager.instance.OffVolumeMusic(value);
            PlayerPrefs.SetInt("MusicVolume", value ? 1 : 0);
        }
    }
    public bool SettingsVolumeMusic 
    { 
        get 
        {
            if (PlayerPrefs.HasKey("MusicVolume")) return PlayerPrefs.GetInt("MusicVolume") == 1;

            PlayerPrefs.SetInt("MusicVolume", 1);
            return true;
        } 
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        isSound = SettingsVolumeSounds;
        isMusic = SettingsVolumeMusic;
    }
}
