using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    private AudioManager audioManager;
    private Field field;

    [SerializeField]
    private int id;

    public int GetId() { return id; }

    private void Awake()
    {
        field = FindObjectOfType<Field>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnMouseDown()
    {
        List<Vector2> vectors = new List<Vector2>();
        FindingBorder(vectors);

        foreach (Vector2 v in vectors)
        {
            if (field.GetCell(v).Free)
            {
                field.GetCell(transform.position).Free = true;
                field.GetCell(transform.position).Check = false;
                transform.position = v;
                field.GetCell(v).Free = false;
                if (field.GetCell(v).ID == id) field.GetCell(v).Check = true;
                audioManager.Play("ElementClick");
                break;
            }
        }
    }

    private void OnMouseUp()
    {
        field.Change();
    }

    private void FindingBorder(List<Vector2> v2)
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (y + 1 <= field.MaxY) v2.Add(new Vector2(x, y + 1));
        if (x + 1 <= field.MinX * -1) v2.Add(new Vector2(x + 1, y));
        if (y - 1 >= field.MaxY * -1) v2.Add(new Vector2(x, y - 1));
        if (x - 1 >= field.MinX) v2.Add(new Vector2(x - 1, y));
    }
}