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

    public event Action OnPauseClick; 
    public event Action OnMenuClick; 
    public event Action OnRestartClick;
    public event Action OnReversClick;

    #endregion
    
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

    public void AddListeners()
    {
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
        AudioManager.Instance.Play("ButtonClick");
        btnPause.gameObject.GetComponent<Image>().sprite = GameData.Instance.IsPause ? spritesButtonPause[0] : spritesButtonPause[1];
        btnPause.targetGraphic.LayoutComplete();
        pauseLine.SetActive(GameData.Instance.IsPause);

        if (GameData.Instance.IsPause)
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
        AudioManager.Instance.Play("ButtonClick");
    }
    
    private void RestartEvent()
    {
        OnRestartClick?.Invoke();
        
        // Animation button and other ui
        AudioManager.Instance.Play("ButtonClick");
    }
    
    private void ReversEvent()
    {
        OnReversClick?.Invoke();
        
        // Animation button and other ui
        AudioManager.Instance.Play("ButtonClick");
        btnRevers.interactable = false;
    }
}
