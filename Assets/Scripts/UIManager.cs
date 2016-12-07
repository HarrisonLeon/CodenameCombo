using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour {

    bool showControls = false;

    GameObject mainMenu;
    GameObject controlsMenu;

	// Use this for initialization
	void Start () {
        mainMenu = GameObject.Find("MainScreen");
        controlsMenu = GameObject.Find("ControlsScreen");

        showControls = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (showControls)
        {
            controlsMenu.SetActive(true);
        }
        else
        {
            controlsMenu.SetActive(false);
        }
    }

	public void startGame(string level){
		Application.LoadLevel(level);
	}

    public void quitToMainMenu()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel("MainMenu");
    }

    public void ShowControls()
    {
        showControls = true;

        controlsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Back"), null);
    }

    public void HideControls()
    {
        showControls = false;

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Start"), null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
