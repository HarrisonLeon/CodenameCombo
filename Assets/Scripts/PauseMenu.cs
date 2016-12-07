using UnityEngine;
using InControl;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    bool isPaused;

    GameObject pauseMenu;

	// Use this for initialization
	void Start () {
        isPaused = false;
        pauseMenu = GameObject.Find("UI").transform.FindChild("PauseMenu").gameObject;
        pauseMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (InputManager.ActiveDevice.Command.WasPressed)
        {
            Debug.Log("ESC PRESSED");
            isPaused = !isPaused;

            if (isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
	}

    public void Unpause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel("MainMenu");
    }
}
