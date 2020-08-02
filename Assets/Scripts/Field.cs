using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField, Range(3, 5)]
    private int width = 3;
    [SerializeField, Range(3, 5)]
    private int height = 3;
    [SerializeField]
    private List<GameObject> Elements = new List<GameObject>();

    [Space, SerializeField]
    private Text frame = null;
    
    [SerializeField]
    private Button reversBtn = null;
    
    public float MinX { get; set; }
    public float MaxY { get; set; }
    private List<Cell> cells = new List<Cell>();

    private List<int> el = new List<int>();

    public Text timer = null;
    public Text progress = null;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        Create();
        Generation();
        CellCheck();
        Change();
        SessionData.Clock.Clear();
        SessionData.Clock.Start();
        SessionData.progressCounter = 0;
        StartCoroutine(TimerUpdate());
    }

    private void Create()
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

    public void Change()
    {
        int count = 0;

        foreach (Cell c in cells)
        {
            if (c.Check) count++;
        }

        if (count == (width * height) - 1)
        {
            if (Elements.Count != 0)
            {
                SessionData.Clock.Stop();
                Instantiate(Elements[0], new Vector2(cells[cells.Count - 1].Position.x, cells[cells.Count - 1].Position.y), Quaternion.identity, transform);
                Elements.RemoveAt(0);
                cells[cells.Count - 1].Free = false;
                frame.text = "ПОБЕДА!";
                audioManager.Play("Win");
            }
        }
        
        if (count == cells.Count - 3)
        {
            if (el.Count != 0) el.Clear();

            foreach (Cell c in cells)
            {
                if (!c.Check && c.Element != null)
                {
                    el.Add(c.ID);
                }
            }
        }
            
        if (reversBtn == null) return;

        reversBtn.interactable = count == cells.Count - 3;
    }

    public void ReversElements()
    {
        if (el.Count != 2) return;

        Vector2 pos1 = new Vector2 (GetCell(el[0]).Position.x, GetCell(el[0]).Position.y);
        Vector2 pos2 = new Vector2 (GetCell(el[1]).Position.x, GetCell(el[1]).Position.y);

        int id = 0;
        
        foreach (Cell c in cells)
        {
            if (c.Element == null)
            {
                id = c.ID;
                GetCell(el[0]).Element.MoveToCell(c.Position);
                break;
            }
        }

        GetCell(el[1]).Element.MoveToCell(pos1);
        GetCell(id).Element.MoveToCell(pos2);

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

    private IEnumerator TimerUpdate()
    {
        while (SessionData.Clock.Hour < 24)
        {
            SessionData.Clock.Update();
            timer.text = SessionData.Clock.Display;
            yield return null;
        }

        SessionData.Clock.Stop();
    }
}