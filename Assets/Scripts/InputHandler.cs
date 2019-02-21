using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
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

    public void Quit()
    {
        audioManager.Play("ButtonClick");
        Application.Quit();
    }
}
