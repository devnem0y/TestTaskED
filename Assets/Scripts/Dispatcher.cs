using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class Dispatcher
{
    public static event Action<Element> OnChangeElement;
    public static event Action OnClickElement;
    public static event Action OnWin;
    public static event Action OnChangeField;

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

    public static void Send(Event e)
    {
        Invoker(GetEvent(e));
    }

    private static void Invoker(Action action)
    {
        action?.Invoke();
    }

    private static Action GetEvent(Event e)
    {
        switch (e)
        {
            case Event.ON_WIN: return OnWin;
            case Event.ON_CHANGE_FIELD: return OnChangeField;
            case Event.ON_CLICK_ELEMENT: return OnClickElement;
            
            default: throw new ArgumentOutOfRangeException(nameof(e), e, null);
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
