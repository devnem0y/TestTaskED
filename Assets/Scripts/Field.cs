using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField, Range(3, 5)]
    private int width = 3;
    [SerializeField, Range(3, 5)]
    private int height = 3;
    [SerializeField]
    private List<GameObject> Elements = new List<GameObject>();
    
    private List<Cell> cells = new List<Cell>();
    private List<int> elementId = new List<int>();

    private int count;
    public bool IsChangeElement => count == cells.Count - 3;

    private float MinX { get; set; }
    private float MaxY { get; set; }

    private void Awake()
    {
        Dispatcher.OnChangeElement += ChangeElement;
    }

    private void OnDestroy()
    {
        Dispatcher.OnChangeElement -= ChangeElement;
    }

    public void Create()
    {
        MinX = ((width - 1) / 2f) * -1;
        MaxY = ((height - 1) / 2f);

        int id = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell = new Cell(id, new Vector2(MinX + x, MaxY - y), transform);
                cells.Add(cell);
                id++;
            }
        }
        
        Generation();
    }

    private void Generation()
    {
        if (Elements.Count != 0)
        {
            foreach (Cell c in cells)
            {
                if (Elements.Count != 1)
                {
                    c.Free = false;
                    int rnd = Random.Range(0, Elements.Count - 1);
                    GameObject el = Elements[rnd];
                    Instantiate(el, c.Position, Quaternion.identity, transform);
                    c.Element = el.GetComponent<Element>();
                    Elements.RemoveAt(rnd);
                }
            }
        }
        
        CellCheck();
        Change();
    }

    public Cell GetCell(Vector2 v2)
    {
        foreach (Cell c in cells)
        {
            if (c.Position == v2) return c;
        }
        return null;
    }
    
    public Cell GetCell(int id)
    {
        foreach (Cell c in cells)
        {
            if (c.ID == id) return c;
        }
        return null;
    }

    private void Change()
    {
        count = 0;

        foreach (Cell c in cells)
        {
            if (c.Check) count++;
        }

        if (count == (width * height) - 1)
        {
            if (Elements.Count != 0)
            {
                Dispatcher.Send(Event.ON_WIN);
                Instantiate(Elements[0], new Vector2(cells[cells.Count - 1].Position.x, cells[cells.Count - 1].Position.y), Quaternion.identity, transform);
                Elements.RemoveAt(0);
                cells[cells.Count - 1].Free = false;
            }
        }
        
        if (count == cells.Count - 3)
        {
            if (elementId.Count != 0) elementId.Clear();

            foreach (Cell c in cells)
            {
                if (!c.Check && c.Element != null)
                {
                    elementId.Add(c.ID);
                }
            }
        }
        
        Dispatcher.Send(Event.ON_CHANGE_FIELD);
    }

    public void ReversElements()
    {
        if (elementId.Count != 2) return;

        Vector2 pos1 = new Vector2 (GetCell(elementId[0]).Position.x, GetCell(elementId[0]).Position.y);
        Vector2 pos2 = new Vector2 (GetCell(elementId[1]).Position.x, GetCell(elementId[1]).Position.y);

        int id = 0;
        
        foreach (Cell c in cells)
        {
            if (c.Element == null)
            {
                id = c.ID;
                MoveToCell(GetCell(elementId[0]).Element, c.Position);
                break;
            }
        }

        MoveToCell(GetCell(elementId[1]).Element, pos1);
        MoveToCell(GetCell(id).Element, pos2);

        foreach (Cell c in cells)
        {
            if (c.Element == null)
            {
                c.Check = false;
                c.Free = true;
                break;
            }
        }
        
        Change();
    }

    private void CellCheck()
    {
        foreach (Cell c in cells)
        {
            if (c.ID == c.Element?.ID)
            {
                c.Check = true;
            }
        }
    }

    private void ChangeElement(Element element)
    {
        List<Vector2> vectors = new List<Vector2>();
        FindingBorder(element.Position, vectors);

        foreach (Vector2 v in vectors)
        {
            if (GetCell(v).Free)
            {
                MoveToCell(element, v, true);
                break;
            }
        }
        
        Change();
    }
    private void FindingBorder(Vector2 elementPosition, List<Vector2> v2)
    {
        float x = elementPosition.x;
        float y = elementPosition.y;

        if (y + 1 <= MaxY) v2.Add(new Vector2(x, y + 1));
        if (x + 1 <= MinX * -1) v2.Add(new Vector2(x + 1, y));
        if (y - 1 >= MaxY * -1) v2.Add(new Vector2(x, y - 1));
        if (x - 1 >= MinX) v2.Add(new Vector2(x - 1, y));
    }
    
    private void MoveToCell(Element element, Vector2 cellPosition, bool isClick = false)
    {
        GetCell(element.Position).Free = isClick;
        GetCell(element.Position).Element = null;
        GetCell(element.Position).Check = false;
        element.Position = cellPosition;
        GetCell(cellPosition).Free = false;
        GetCell(cellPosition).Element = element;
        GetCell(element.ID).Check = GetCell(cellPosition).ID == element.ID;
        
        Dispatcher.Send(Event.ON_CLICK_ELEMENT);
    }
}