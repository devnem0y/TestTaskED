using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private MenuWindowsUI ui = null;

    private void OnDestroy()
    {
        ui.OnQuit -= Quit;
        ui.OnSettingsSoundClick -= SettingsSound;
        ui.OnSettingsMusicClick -= SettingsMusic;
        ui.OnStartSession -= StartSession;
    }

    private void Start()
    {
        ui.Aplay();

        ui.AddListeners();
        ui.OnQuit += Quit;
        ui.OnSettingsSoundClick += SettingsSound;
        ui.OnSettingsMusicClick += SettingsMusic;
        ui.OnStartSession += StartSession;
        
        AudioManager.instance.PlayMusic("Music_0");
    }
    
    public void StartSession(int fieldSize)
    {
        GameData.instance.FieldSize = fieldSize;
        AudioManager.instance.StopMusic("Music_0");
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void SettingsSound()
    {
        GameData.instance.IsSound = !GameData.instance.IsSound;
    }

    private void SettingsMusic()
    {
        GameData.instance.IsMusic = !GameData.instance.IsMusic;
    }
}
