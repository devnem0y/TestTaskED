using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class SessionWindowsUI
{
    #region ButtonEvents

    public event Action OnSettingsSoundClick; 
    public event Action OnSettingsMusicClick; 
    public event Action OnPauseClick; 
    public event Action OnMenuClick; 
    public event Action OnRestartClick;
    public event Action OnReversClick;

    #endregion

    [Header("Game Screen")]
    [SerializeField]
    private Text labelProgressCounter = null;
    [SerializeField]
    private Text labelTime = null;
    [SerializeField]
    private GameObject pauseLine = null;
    [SerializeField] 
    private GameObject top = null;
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

    [Header("Windows Win")]
    [SerializeField]
    private GameObject windowsWin = null;
    [SerializeField]
    private Text title = null;
    [SerializeField]
    private Text score = null;
    [SerializeField]
    private GameObject star1 = null;
    [SerializeField]
    private GameObject star2 = null;
    [SerializeField]
    private GameObject star3 = null;
    [SerializeField]
    private Button btnMenuWin = null;
    [SerializeField]
    private Button btnRestartWin = null;

    public void AddListeners()
    {
        btnSound.onClick.AddListener(SettingsSoundEvent);
        btnMusic.onClick.AddListener(SettingsMusicEvent);
        btnPause.onClick.AddListener(PauseEvent);
        btnMenu.onClick.AddListener(MenuEvent);
        btnRestart.onClick.AddListener(RestartEvent);
        btnRevers.onClick.AddListener(ReversEvent);
        btnMenuWin.onClick.AddListener(MenuEvent);
        btnRestartWin.onClick.AddListener(RestartEvent);
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

    public void ShowWindowsWin(int totalSeconds, int progress)
    {
        top.SetActive(false);
        btnRevers.gameObject.SetActive(false);
        windowsWin.SetActive(true);

        int variation = GameData.instance.FieldSize * 5;

        #region Ужасный switch

        switch (GameData.instance.FieldSize)
        {
            case 3:
                if (totalSeconds > 45 + variation || progress > 35 + variation)
                {
                    // 1 star
                    star1.SetActive(true);
                    title.text = "ЕЩЁ РАЗ?";
                    score.text = Random.Range(100, 300).ToString();
                }
                else if (totalSeconds < 45 - variation || progress < 35)
                {
                    // 3 star
                    star3.SetActive(true);
                    title.text = "ПОБЕДА!";
                    score.text = Random.Range(1500, 2200).ToString();
                }
                else if ((totalSeconds > 45 - variation && totalSeconds < 45 + variation) || (progress > 35 - variation && progress < 35 + variation))
                {
                    // 2 star
                    star2.SetActive(true);
                    title.text = "ХОРОШО";
                    score.text = Random.Range(310, 1200).ToString();
                }
                break;
            case 4:
                if (totalSeconds > 120 + variation || progress > 100 + variation)
                {
                    // 1 star
                    star1.SetActive(true);
                    title.text = "ЕЩЁ РАЗ?";
                    score.text = Random.Range(400, 850).ToString();
                }
                else if ((totalSeconds > 120 - variation && totalSeconds < 120 + variation) || (progress > 100 - variation && progress < 100 + variation))
                {
                    // 2 star
                    star2.SetActive(true);
                    title.text = "ХОРОШО";
                    score.text = Random.Range(1100, 2000).ToString();
                }
                else if (totalSeconds < 120 - variation || progress < 100)
                {
                    // 3 star
                    star3.SetActive(true);
                    title.text = "ПОБЕДА!";
                    score.text = Random.Range(2200, 3500).ToString();
                }
                break;
            case 5:
                if (totalSeconds > 300 + variation || progress > 335 + variation)
                {
                    // 1 star
                    star1.SetActive(true);
                    title.text = "ЕЩЁ РАЗ?";
                    score.text = Random.Range(650, 1000).ToString();
                }
                else if ((totalSeconds > 300 - variation && totalSeconds < 300 + variation) || (progress > 335 - variation && progress < 335 + variation))
                {
                    // 2 star
                    star2.SetActive(true);
                    title.text = "ХОРОШО";
                    score.text = Random.Range(1500, 3200).ToString();
                }
                else if (totalSeconds < 300 - variation || progress < 335)
                {
                    // 3 star
                    star3.SetActive(true);
                    title.text = "ПОБЕДА!";
                    score.text = Random.Range(3350, 6700).ToString();
                }
                break;
        }

        #endregion
    }
}
