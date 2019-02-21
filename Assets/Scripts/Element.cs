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

        if (transform.position.y + 1 <= field.MaxY)
        {
            float newY = transform.position.y + 1;
            v2.Add(new Vector2(x, newY));
        }
        if (transform.position.x + 1 <= field.MinX * -1)
        {
            float newX = transform.position.x + 1;
            v2.Add(new Vector2(newX, y));
        }
        if (transform.position.y - 1 >= field.MaxY * -1)
        {
            float newY = transform.position.y - 1;
            v2.Add(new Vector2(x, newY));
        }
        if (transform.position.x - 1 >= field.MinX)
        {
            float newX = transform.position.x - 1;
            v2.Add(new Vector2(newX, y));
        }
    }
}