using System;
using UnityEngine;

public class Dispatcher
{
    public static event Action<Element> OnChangeElement;
    
    public delegate void ClickDelegate();
    public static event ClickDelegate OnClickElement;
    
    public delegate void WinDelegate();
    public static event WinDelegate OnWin;
    
    public delegate void FieldChangeDelegate();
    public static event FieldChangeDelegate OnChangeField;

    public static void Send(Event e)
    {
        switch (e)
        {
            case Event.ON_CLICK_ELEMENT:
                OnClickElement?.Invoke();
                break;
            case Event.ON_WIN:
                OnWin?.Invoke();
                break;
            case Event.ON_CHANGE_FIELD:
                OnChangeField?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }
    
    public static void Send(Event e, Element element)
    {
        switch (e)
        {
            case Event.ON_CHANGE_ELEMENT:
                OnChangeElement?.Invoke(element);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }
}

public enum Event
{
    ON_CLICK_ELEMENT,
    ON_CHANGE_ELEMENT,
    ON_CHANGE_FIELD,
    ON_WIN,
}
