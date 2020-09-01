using UnityEngine;

public class Clock
{
    private const float SPEED = 1;
    
    public float StartTime { get; private set; }
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

        StartTime += Time.deltaTime * SPEED;

        Hour = (int) Mathf.Floor((StartTime % 216000) / 3600);
        Minute = (int) Mathf.Floor((StartTime % 3600) / 60);
        Second = (int) Mathf.Floor(StartTime % 60);
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
        StartTime = 0;
        Second = 0;
        Minute = 0;
        Hour = 0;
    }
}
