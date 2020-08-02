using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField]
    private int id = 0;

    public int ID => id;
    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void OnMouseUp()
    {
        Dispatcher.Send(Event.ON_CHANGE_ELEMENT,this);
    }
}