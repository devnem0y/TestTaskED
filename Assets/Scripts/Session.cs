using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    private Field field;
    
    private int progressCounter;
    private readonly Clock clock = new Clock();

    [SerializeField]
    private SessionWindowsUI ui = null;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private List<Sprite> backgrounds;

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
        AudioManager.instance.StopMusic("Music_1");
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
        int rnd = Random.Range(0, backgrounds.Count);
        background.GetComponent<SpriteRenderer>().sprite = backgrounds[rnd];

        ui.Aplay();

        field.Create();

        clock.Clear();
        clock.Start();
        StartCoroutine(TimerUpdate());

        AudioManager.instance.PlayMusic("Music_1");
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
        AudioManager.instance.StopMusic("Music_1");
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
