using UnityEngine;

public class Cell
{
    public int ID { get; set; }
    public Vector2 Position { get; set; }

    public bool Check { get; set; }
    public bool Free { get; set; }

    public Element Element { get; set; }

    public Cell(int id, Vector2 position, Transform parent)
    {
        ID = id;
        Position = position;
        Free = true;
        Check = false;
    }
}