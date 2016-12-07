using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;

using InControl;

public class LevelCompleteMenu : MonoBehaviour
{
    bool isEnabled;

    bool calledOnce = false;

    ScoreManager scoreManager;

    private string scoreString;
    private string comboCountString;

    private GameObject scoreText;
    private GameObject comboCountText;

    RectTransform missionCompleteText;
    RectTransform continueButton;
    RectTransform quitButton;

    //hard coded, taken from original positions
    Vector2 missionTextPos = new Vector2(0f, 30.6f);
    Vector2 scoreTextPos = new Vector2(0f, 10.1f);
    Vector2 comboTextPos = new Vector2(0f, -1f);
    Vector2 continuePos = new Vector2(0f, -23.15f);
    Vector2 quitPos = new Vector2(0f, -36.39f);

    GameObject levelCompleteMenu;

    // Use this for initialization
    void Awake()
    {
        isEnabled = false;

        levelCompleteMenu = GameObject.Find("UI").transform.FindChild("LevelCompleteMenu").gameObject;

		// scoreText = GameObject.Find("ScoreText");
		// comboCountText = GameObject.Find("ComboText");

		scoreText = levelCompleteMenu.transform.Find("ScoreText").gameObject;
		comboCountText = levelCompleteMenu.transform.Find("ComboText").gameObject;

        missionCompleteText = levelCompleteMenu.transform.FindChild("MissionCompleteText").gameObject.GetComponent<RectTransform>();
        continueButton = levelCompleteMenu.transform.FindChild("Continue").gameObject.GetComponent<RectTransform>();
        quitButton = levelCompleteMenu.transform.FindChild("Quit").gameObject.GetComponent<RectTransform>();

        scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
           levelCompleteMenu.SetActive(true);
        }
        else
        {
            levelCompleteMenu.SetActive(false);
        }
    }

    public void EnableMenu()
    {
        StartCoroutine(MenuEnable());
    }

    public void NextLevel()
    {
		int sceneNo = SceneManager.GetActiveScene().buildIndex;
		sceneNo = (sceneNo == 4) ? 0 : sceneNo + 1;
		//Application.LoadLevel(name);
		SceneManager.LoadScene(sceneNo);
    }

    public void Quit()
    {
        //Application.LoadLevel("MainMenu");
		SceneManager.LoadScene("MainMenu");
    }

    IEnumerator MenuEnable()
    {
		//yield return new WaitForSeconds(2.0f);

		isEnabled = true;

        scoreText.GetComponent<Text>().text = "Your Score: " + scoreManager.GetScore().ToString();
        comboCountText.GetComponent<Text>().text = "Longest Streak: " + scoreManager.GetComboCounter().ToString();

        StartCoroutine(LerpText());

        yield return new WaitForSeconds(2.0f);

        EventSystem.current.SetSelectedGameObject(continueButton.gameObject, null);

        //continueButton.gameObject.GetComponent<Button>().Select();

    }

    IEnumerator LerpText()
    {
        missionCompleteText.anchoredPosition = Vector2.Lerp(missionCompleteText.anchoredPosition, missionTextPos, Time.deltaTime * 5);

        yield return new WaitForSeconds(0.5f);

        scoreText.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(scoreText.GetComponent<RectTransform>().anchoredPosition, scoreTextPos, Time.deltaTime * 5);

        yield return new WaitForSeconds(0.5f);

        comboCountText.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(comboCountText.GetComponent<RectTransform>().anchoredPosition, comboTextPos, Time.deltaTime * 5);

        yield return new WaitForSeconds(0.5f);

        continueButton.anchoredPosition = Vector2.Lerp(continueButton.anchoredPosition, continuePos, Time.deltaTime * 5);
        quitButton.anchoredPosition = Vector2.Lerp(quitButton.anchoredPosition, quitPos, Time.deltaTime * 5);

    }

}
