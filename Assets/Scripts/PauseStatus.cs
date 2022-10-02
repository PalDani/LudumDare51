using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseStatus : MonoBehaviour
{

    public GameObject pauseMenu;

    private bool isPaused = false;
    public bool IsPaused
    {
        get => isPaused;
        set {
            isPaused = value;
            SetMouseState(isPaused);
        }
    }



    public static PauseStatus Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMouseState(bool state)
    {
        if(state)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseMenuStatus();
        }
    }

    private void ChangePauseMenuStatus()
    {
        IsPaused = !IsPaused;
        pauseMenu.SetActive(IsPaused);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ContinueGame()
    {
        ChangePauseMenuStatus();
    }
}
