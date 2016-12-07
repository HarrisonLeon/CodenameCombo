using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEngine.EventSystems;
using System.Collections;

public class EndMenu : MonoBehaviour
{

    bool isEnabled;

    ScoreManager scoreManager;

    private string scoreString;
    private string comboCountString;

    private GameObject scoreText;
    private GameObject comboCountText;

    GameObject endMenu;

    // Use this for initialization
    void Start()
    {
        isEnabled = false;

        scoreText = GameObject.Find("ScoreText");
        comboCountText = GameObject.Find("ComboText");

        endMenu = GameObject.Find("LevelCompleteMenu");
        scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
           endMenu.SetActive(true);
        }
        else
        {
            endMenu.SetActive(false);
        }
    }

    public void EnableMenu()
    {
        StartCoroutine(MenuEnable());
    }

    public void NextLevel(string name)
    {
        Application.LoadLevel(name);
    }

    public void Quit()
    {
        Application.LoadLevel("MainMenu");
    }

    IEnumerator MenuEnable()
    {
        //yield return new WaitForSeconds(2.0f);

        isEnabled = true;

        scoreText.GetComponent<Text>().text = "Your Score: " + scoreManager.GetScore().ToString();
        comboCountText.GetComponent<Text>().text = "Longest Streak: " + scoreManager.GetComboCounter().ToString();

        yield return new WaitForSeconds(1.0f);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Continue"), null);
        
    }

}
