using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SessionWindowsUI
{
    [SerializeField]
    private Text labelProgressCounter = null;
    [SerializeField]
    private Text labelTime = null;

    public void SetProgress(int progress)
    {
        labelProgressCounter.text = progress.ToString();
    }

    public void SetTime(string time)
    {
        labelTime.text = time;
    }
}
