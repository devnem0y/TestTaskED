using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    private Field field;

    [SerializeField]
    private Button reversBtn = null;

    private void Awake()
    {
        field = FindObjectOfType<Field>();
    }

    public void RestartLevel()
    {
        AudioManager.Instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartScene(int fieldSize)
    {
        GameManager.Instance.FieldSize = fieldSize;
        AudioManager.Instance.Play("ButtonClick");
        SceneManager.LoadScene(fieldSize != 0 ? "Game" : "Menu");
    }

    public void ReversElements()
    {
        AudioManager.Instance.Play("ButtonClick");
        field.ReversElements();
        reversBtn.interactable = false;
    }

    public void Quit()
    {
        AudioManager.Instance.Play("ButtonClick");
        Application.Quit();
    }
}
