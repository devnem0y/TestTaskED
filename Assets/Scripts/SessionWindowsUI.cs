using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SessionWindowsUI
{
    [SerializeField]
    private Text labelProgressCounter = null;
    [SerializeField]
    private Text labelTime = null;
    [SerializeField]
    private GameObject pauseLine = null;

    #region ButtonEvents

    public event Action OnSettingsSoundClick; 
    public event Action OnSettingsMusicClick; 
    public event Action OnPauseClick; 
    public event Action OnMenuClick; 
    public event Action OnRestartClick;
    public event Action OnReversClick;

    #endregion
    
    [SerializeField]
    private Button btnSound = null;
    [SerializeField]
    private Button btnMusic = null;
    [SerializeField]
    private Button btnRevers = null;
    [SerializeField]
    private Button btnMenu = null;
    [SerializeField]
    private Button btnRestart = null;
    [SerializeField]
    private Button btnPause = null;
    [SerializeField]
    private List<Sprite> spritesButtonPause;
    [SerializeField]
    private List<Sprite> spritesButtonSound;
    [SerializeField]
    private List<Sprite> spritesButtonMusic;

    public void AddListeners()
    {
        btnSound.onClick.AddListener(SettingsSoundEvent);
        btnMusic.onClick.AddListener(SettingsMusicEvent);
        btnPause.onClick.AddListener(PauseEvent);
        btnMenu.onClick.AddListener(MenuEvent);
        btnRestart.onClick.AddListener(RestartEvent);
        btnRevers.onClick.AddListener(ReversEvent);
    }

    public void SetProgress(int progress)
    {
        labelProgressCounter.text = progress.ToString();
    }

    public void SetTime(string time)
    {
        labelTime.text = time;
    }

    public void SetButtonReversInteractable(bool isInteractable)
    {
        btnRevers.interactable = isInteractable;
    }
    
    public void SetButtonPauseInteractable(bool isInteractable)
    {
        btnPause.interactable = isInteractable;
    }
    
    private void PauseEvent()
    {
        OnPauseClick?.Invoke();
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
        btnPause.gameObject.GetComponent<Image>().sprite = GameData.instance.IsPause ? spritesButtonPause[0] : spritesButtonPause[1];
        btnPause.targetGraphic.LayoutComplete();
        pauseLine.SetActive(GameData.instance.IsPause);

        if (GameData.instance.IsPause)
        {
            btnMenu.interactable = false;
            btnRestart.interactable = false;
            btnRevers.interactable = false;
        }
        else
        {
            btnMenu.interactable = true;
            btnRestart.interactable = true;
            Dispatcher.Send(Event.ON_CHANGE_FIELD);
        }
    }
    
    private void MenuEvent()
    {
        OnMenuClick?.Invoke();
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
    }
    
    private void RestartEvent()
    {
        OnRestartClick?.Invoke();
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
    }
    
    private void ReversEvent()
    {
        OnReversClick?.Invoke();
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
        btnRevers.interactable = false;
    }

    private void SettingsSoundEvent()
    {
        OnSettingsSoundClick?.Invoke();
        
        // Animation button and other ui
        btnSound.gameObject.GetComponent<Image>().sprite = GameData.instance.IsSound ? spritesButtonSound[0] : spritesButtonSound[1];
        AudioManager.instance.PlaySound("ButtonClick");
    }

    private void SettingsMusicEvent()
    {
        OnSettingsMusicClick?.Invoke();
        
        // Animation button and other ui
        btnMusic.gameObject.GetComponent<Image>().sprite = GameData.instance.IsMusic ? spritesButtonMusic[0] : spritesButtonMusic[1];
        AudioManager.instance.PlaySound("ButtonClick");
    }

    public void Aplay()
    {
        btnSound.gameObject.GetComponent<Image>().sprite = GameData.instance.IsSound ? spritesButtonSound[0] : spritesButtonSound[1];
        btnMusic.gameObject.GetComponent<Image>().sprite = GameData.instance.IsMusic ? spritesButtonMusic[0] : spritesButtonMusic[1];
    }
}
