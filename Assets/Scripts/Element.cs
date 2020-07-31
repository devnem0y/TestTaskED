using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    private AudioManager audioManager;
    private Field field;

    [SerializeField]
    private int id;

    public int ID => id;
    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    private void Awake()
    {
        field = FindObjectOfType<Field>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void OnClickEl()
    {
        List<Vector2> vectors = new List<Vector2>();
        FindingBorder(vectors);

        foreach (Vector2 v in vectors)
        {
            if (field.GetCell(v).Free)
            {
                MoveToCell(v, true);
                audioManager.Play("ElementClick");
                break;
            }
        }
    }

    public void MoveToCell(Vector2 cellPosition, bool isClick = false)
    {
        field.GetCell(Position).Free = isClick;
        field.GetCell(Position).Element = null;
        field.GetCell(Position).Check = false;
        Position = cellPosition;
        field.GetCell(cellPosition).Free = false;
        field.GetCell(cellPosition).Element = this;
        field.GetCell(ID).Check = field.GetCell(cellPosition).ID == ID;
    }
    
    private void OnMouseUp()
    {
        OnClickEl();
        field.Change();
    }

    private void FindingBorder(List<Vector2> v2)
    {
        float x = Position.x;
        float y = Position.y;

        if (y + 1 <= field.MaxY) v2.Add(new Vector2(x, y + 1));
        if (x + 1 <= field.MinX * -1) v2.Add(new Vector2(x + 1, y));
        if (y - 1 >= field.MaxY * -1) v2.Add(new Vector2(x, y - 1));
        if (x - 1 >= field.MinX) v2.Add(new Vector2(x - 1, y));
    }
}