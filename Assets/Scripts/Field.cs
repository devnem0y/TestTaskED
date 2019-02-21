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
    private Text frame;

    private int[,] grid;
    public float MinX { get; set; }
    public float MaxY { get; set; }
    private List<Cell> cells = new List<Cell>();

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        Create();
        Generation();
    }

    private void Create()
    {
        grid = new int[width, height];
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

        Change();
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
                    Instantiate(Elements[rnd], c.Position, Quaternion.identity, transform);
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
                Instantiate(Elements[0], new Vector2(cells[cells.Count - 1].Position.x, cells[cells.Count - 1].Position.y), Quaternion.identity, transform);
                Elements.RemoveAt(0);
                cells[cells.Count - 1].Free = false;
                frame.text = "ПОБЕДА!";
                audioManager.Play("Win");
            }
        }
    }
}