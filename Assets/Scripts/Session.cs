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

    private int randomMusicIndex;

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
        AudioManager.instance.StopMusic($"Music_{randomMusicIndex}");
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
        RandomBackground();
        
        ui.Aplay();

        field.Create();

        clock.Clear();
        clock.Start();
        StartCoroutine(TimerUpdate());

        RandomMusic();
    }

    private void RandomBackground()
    {
        var rnd = Random.Range(0, backgrounds.Count);
        background.GetComponent<SpriteRenderer>().sprite = backgrounds[rnd];
    }
    
    private void RandomMusic()
    {
        randomMusicIndex = Random.Range(1, 5); //TODO: hard-code {max: 5}
        AudioManager.instance.PlayMusic($"Music_{randomMusicIndex}");
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
        AudioManager.instance.StopMusic($"Music_{randomMusicIndex}");
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
