using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenu;
    [Tooltip("Specify pause button")][SerializeField] InputActionReference pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pauseButton.action.started += ctx =>
        {
              if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        };
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void MainMenu(){
        SceneManager.LoadSceneAsync(0);

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
