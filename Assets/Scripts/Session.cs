using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour
{
    private Field field;
    
    private int progressCounter;
    private readonly Clock clock = new Clock();

    [SerializeField]
    private SessionWindowsUI ui = null;

    private void Awake()
    {
        field = FindObjectOfType<Field>();
        
        Dispatcher.OnClickElement += ClickElement;
        Dispatcher.OnWin += Win;
        Dispatcher.OnChangeField += ChangeField;

        ui.AddListeners();
        ui.OnSettingsSoundClick += SettingsSound;
        ui.OnSettingsMusicClick += SettingsMusic;
        ui.OnPauseClick += Pause;
        ui.OnReversClick += Revers;
        ui.OnMenuClick += Menu;
        ui.OnRestartClick += Restart;
    }

    private void Pause()
    {
        GameData.instance.IsPause = !GameData.instance.IsPause;

        if (GameData.instance.IsPause)
        {
            clock.Stop();
        }
        else
        {
            clock.Start();
        }
    }

    private void SettingsSound()
    {
        GameData.instance.IsSound = !GameData.instance.IsSound;
    }

    private void SettingsMusic()
    {
        GameData.instance.IsMusic = !GameData.instance.IsMusic;
    }

    private void Revers()
    {
        field.ReversElements();
    }
    
    private void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void OnDestroy()
    {
        Dispatcher.OnClickElement -= ClickElement;
        Dispatcher.OnWin -= Win;
        Dispatcher.OnChangeField -= ChangeField;

        ui.OnPauseClick -= Pause;
        ui.OnReversClick -= Revers;
        ui.OnMenuClick -= Menu;
        ui.OnRestartClick -= Restart;
        ui.OnSettingsSoundClick -= SettingsSound;
        ui.OnSettingsMusicClick -= SettingsMusic;
    }

    private void Start()
    {
        ui.Aplay();

        field.Create();

        clock.Clear();
        clock.Start();
        StartCoroutine(TimerUpdate());
    }

    private void ClickElement()
    {
        progressCounter++;
        ui.SetProgress(progressCounter);
        AudioManager.instance.PlaySound("ElementClick");
    }

    private void ChangeField()
    {
        ui.SetButtonReversInteractable(field.IsChangeElement);
    }
    
    private void Win()
    {
        clock.Stop();
        ui.SetButtonPauseInteractable(false);
        AudioManager.instance.PlaySound("Win");
    }
    
    private IEnumerator TimerUpdate()
    {
        if (!field.IsStart) yield return null;
        
        while (clock.Hour < 24)
        {
            clock.Update();
            ui.SetTime(clock.Display);
            yield return null;
        }

        clock.Stop();
    }
}
