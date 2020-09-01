using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MenuWindowsUI
{
    public event Action<int> OnStartSession;
    public event Action OnQuit; 
    public event Action OnOpenURL;
    public event Action OnSettingsSoundClick;
    public event Action OnSettingsMusicClick;

    [SerializeField]
    private Button btnSound = null;
    [SerializeField]
    private Button btnMusic = null;
    [SerializeField]
    private Button btnQuit = null;
    [SerializeField]
    private Button btn3x3 = null;
    [SerializeField]
    private Button btn4x4 = null;
    [SerializeField]
    private Button btn5x5 = null;
    
    [SerializeField]
    private List<Sprite> spritesButtonSound;
    [SerializeField]
    private List<Sprite> spritesButtonMusic;
    
    [SerializeField]
    private Text version = null;
    [SerializeField]
    private Button btnGoToGooglePlay = null;
    [SerializeField] 
    private string urlAppGooglePlay = "";
    public string UrlAppGooglePlay => urlAppGooglePlay;

    public void AddListeners()
    {
        btnSound.onClick.AddListener(SettingsSoundEvent);
        btnMusic.onClick.AddListener(SettingsMusicEvent);
        btnQuit.onClick.AddListener(QuitEvent);
        btn3x3.onClick.AddListener(Session3x3Event);
        btn4x4.onClick.AddListener(Session4x4Event);
        btn5x5.onClick.AddListener(Session5x5Event);
        btnGoToGooglePlay.onClick.AddListener(GoToGooglePlay);
    }

    public void Aplay()
    {
        version.text = $"v {Application.version}";
        
        #if PLATFORM_WEBGL
        btnQuit.gameObject.SetActive(false);
        btnGoToGooglePlay.gameObject.SetActive(Application.version == "1.2");
        #endif
        
        btnSound.gameObject.GetComponent<Image>().sprite = GameData.instance.IsSound ? spritesButtonSound[0] : spritesButtonSound[1];
        btnMusic.gameObject.GetComponent<Image>().sprite = GameData.instance.IsMusic ? spritesButtonMusic[0] : spritesButtonMusic[1];
    }

    private void QuitEvent()
    {
        OnQuit?.Invoke();
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
    }
    private void GoToGooglePlay()
    {
        OnOpenURL?.Invoke();
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
    }

    private void Session3x3Event()
    {
        OnStartSession?.Invoke(3);
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
    }

    private void Session4x4Event()
    {
        OnStartSession?.Invoke(4);
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
    }

    private void Session5x5Event()
    {
        OnStartSession?.Invoke(5);
        
        // Animation button and other ui
        AudioManager.instance.PlaySound("ButtonClick");
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
}
