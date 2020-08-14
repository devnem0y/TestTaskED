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
        AudioManager.instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartScene(int fieldSize)
    {
        GameData.instance.FieldSize = fieldSize;
        AudioManager.instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(fieldSize != 0 ? "Game" : "Menu");
    }

    public void ReversElements()
    {
        AudioManager.instance.PlaySound("ButtonClick");
        field.ReversElements();
        reversBtn.interactable = false;
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound("ButtonClick");
        Application.Quit();
    }
}
