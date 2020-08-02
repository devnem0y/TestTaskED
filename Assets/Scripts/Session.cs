using System.Collections;
using UnityEngine;

public class Session : MonoBehaviour
{
    private AudioManager audioManager;
    private Field field;
    
    private int progressCounter;
    private readonly Clock clock = new Clock();

    [SerializeField]
    private SessionWindowsUI ui = null;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        field = FindObjectOfType<Field>();
        
        Dispatcher.OnClickElement += ClickElement;
        Dispatcher.OnWin += Win;
        Dispatcher.OnChangeField += ChangeField;
    }
    
    private void OnDestroy()
    {
        Dispatcher.OnClickElement -= ClickElement;
        Dispatcher.OnWin -= ClickElement;
        Dispatcher.OnChangeField -= ChangeField;
    }

    private void Start()
    {
        field.Create();

        clock.Clear();
        clock.Start();
        StartCoroutine(TimerUpdate());
    }

    private void ClickElement()
    {
        progressCounter++;
        ui.SetProgress(progressCounter);
        audioManager.Play("ElementClick");
    }

    private void ChangeField()
    {
        ui.SetButtonReversInteractable(field.IsChangeElement);
    }
    
    private void Win()
    {
        clock.Stop();
        audioManager.Play("Win");
    }
    
    private IEnumerator TimerUpdate()
    {
        while (clock.Hour < 24)
        {
            clock.Update();
            ui.SetTime(clock.Display);
            yield return null;
        }

        clock.Stop();
    }
}
