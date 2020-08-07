using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _elements = new List<GameObject>();
    
    private int width => GameManager.Instance.FieldSize;
    private int height => GameManager.Instance.FieldSize;

    private List<Cell> _cells = new List<Cell>();
    private List<int> _id = new List<int>();

    private int count;
    public bool IsChangeElement => count == _cells.Count - 3;

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
                _cells.Add(cell);
                id++;
            }
        }
        
        Generation();
    }

    private void Generation()
    {
        if (_elements.Count > (width * height))
        {
            var index = _elements.IndexOf(_elements.First(x=>x.GetComponent<Element>().ID > (width * height) - 1));
            if (index > -1)
            {
                _elements.RemoveRange(index, _elements.Count - index);
            }
        }

        if (_elements.Count != 0)
        {
            foreach (Cell c in _cells)
            {
                if (_elements.Count != 1)
                {
                    c.Free = false;
                    int rnd = Random.Range(0, _elements.Count - 1);
                    GameObject el = _elements[rnd];
                    Instantiate(el, c.Position, Quaternion.identity, transform);
                    c.Element = el.GetComponent<Element>();
                    _elements.RemoveAt(rnd);
                }
            }
        }
        
        CellCheck();
        Change();
    }

    public Cell GetCell(Vector2 v2)
    {
        foreach (Cell c in _cells)
        {
            if (c.Position == v2) return c;
        }
        return null;
    }
    
    public Cell GetCell(int id)
    {
        foreach (Cell c in _cells)
        {
            if (c.ID == id) return c;
        }
        return null;
    }

    private void Change()
    {
        count = 0;

        foreach (Cell c in _cells)
        {
            if (c.Check) count++;
        }

        if (count == (width * height) - 1)
        {
            if (_elements.Count != 0)
            {
                Dispatcher.Send(Event.ON_WIN);
                Instantiate(_elements[0], new Vector2(_cells[_cells.Count - 1].Position.x, _cells[_cells.Count - 1].Position.y), Quaternion.identity, transform);
                _elements.RemoveAt(0);
                _cells[_cells.Count - 1].Free = false;
            }
        }
        
        if (count == _cells.Count - 3)
        {
            if (_id.Count != 0) _id.Clear();

            foreach (Cell c in _cells)
            {
                if (!c.Check && c.Element != null)
                {
                    _id.Add(c.ID);
                }
            }
        }
        
        Dispatcher.Send(Event.ON_CHANGE_FIELD);
    }

    public void ReversElements()
    {
        if (_id.Count != 2) return;

        Vector2 pos1 = new Vector2 (GetCell(_id[0]).Position.x, GetCell(_id[0]).Position.y);
        Vector2 pos2 = new Vector2 (GetCell(_id[1]).Position.x, GetCell(_id[1]).Position.y);

        int id = 0;
        
        foreach (Cell c in _cells)
        {
            if (c.Element == null)
            {
                id = c.ID;
                MoveToCell(GetCell(_id[0]).Element, c.Position);
                break;
            }
        }

        MoveToCell(GetCell(_id[1]).Element, pos1);
        MoveToCell(GetCell(id).Element, pos2);

        foreach (Cell c in _cells)
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
        foreach (Cell c in _cells)
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