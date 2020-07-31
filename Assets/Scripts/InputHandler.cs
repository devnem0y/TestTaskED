using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    private AudioManager audioManager;
    private Field field;

    [SerializeField]
    private Button reversBtn = null;

    private void Awake()
    {
        field = FindObjectOfType<Field>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void RestartLevel()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartScene(string name)
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene(name);
    }

    public void ReversElements()
    {
        audioManager.Play("ButtonClick");
        field.ReversElements();
        reversBtn.interactable = false;
    }

    public void Quit()
    {
        audioManager.Play("ButtonClick");
        Application.Quit();
    }
}
