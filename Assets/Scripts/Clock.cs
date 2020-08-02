using UnityEngine;

public class Clock
{
    private const float SPEED = 1;
    
    private float startTime;

    public int Second { get; private set; }
    public int Minute { get; private set; }
    public int Hour { get; private set; }

    private bool isRun = true;

    public string Display => $"{Hour:00}:{Minute:00}:{Second:00}";

    public Clock()
    {
        
    }
    
    public void Update()
    {
        if (Input.GetKey(KeyCode.S)) Start();
        if (Input.GetKey(KeyCode.P)) Stop();
        if (Input.GetKey(KeyCode.C)) Clear();

        if (!isRun) return;

        startTime += Time.deltaTime * SPEED;

        Hour = (int) Mathf.Floor((startTime % 216000) / 3600);
        Minute = (int) Mathf.Floor((startTime % 3600) / 60);
        Second = (int) Mathf.Floor(startTime % 60);
    }
    
    public void Start()
    {
        isRun = true;
    }

    public void Stop()
    {
        isRun = false;
    }

    public void Clear()
    {
        startTime = 0;
        Second = 0;
        Minute = 0;
        Hour = 0;
    }
}
