using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SessionWindowsUI
{
    [SerializeField]
    private Text labelProgressCounter = null;
    [SerializeField]
    private Text labelTime = null;
    
    [SerializeField]
    private Button btnRevers = null;

    public void SetProgress(int progress)
    {
        labelProgressCounter.text = progress.ToString();
    }

    public void SetTime(string time)
    {
        labelTime.text = time;
    }

    public void SetButtonReversInteractable(bool isInteractable)
    {
        btnRevers.interactable = isInteractable;
    }
}
