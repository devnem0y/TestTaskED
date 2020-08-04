using System.Collections;
using UnityEngine;

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
        ui.OnTestClick += Test;
    }

    private void Test()
    {
        Debug.Log("TestEvent");
    }
    
    private void OnDestroy()
    {
        Dispatcher.OnClickElement -= ClickElement;
        Dispatcher.OnWin -= Win;
        Dispatcher.OnChangeField -= ChangeField;

        ui.OnTestClick -= Test;
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
        AudioManager.Instance.Play("ElementClick");
    }

    private void ChangeField()
    {
        ui.SetButtonReversInteractable(field.IsChangeElement);
    }
    
    private void Win()
    {
        clock.Stop();
        AudioManager.Instance.Play("Win");
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
