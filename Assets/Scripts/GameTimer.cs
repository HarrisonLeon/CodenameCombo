using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{
	LevelManager levelManager;

	public float timeLeft;

	Text text;

    public Text introText;

	ScoreManager scoreManager;
	public GameObject Instructions;

    GameObject player;

    GameObject soundManager;

    GameObject spawnManager;

    Camera mainCamera;

    bool playedOnce = false;

    // Use this for initialization
    void Start ()
	{
		StartCoroutine(DisableIntroText());
		text = GetComponent<Text>();
		text.text = ((int)(timeLeft)).ToString();

		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

		GameObject sndm = GameObject.FindGameObjectWithTag("SoundManager");
		GameObject spnm = GameObject.FindGameObjectWithTag("Spawner");
		if (!sndm)
		{
			sndm = levelManager.SpawnSound();
		}
		if (!spnm)
		{
			spnm = levelManager.SpawnSpawner();
		}
		soundManager = sndm;
		spawnManager = spnm;
        
		scoreManager = GameObject.FindWithTag("Score").GetComponent<ScoreManager>();
        player = GameObject.Find("Spy");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update ()
	{
		timeLeft -= Time.deltaTime;
		text.text = ((int)(timeLeft)).ToString();
		//if (timeLeft <  15f && timeLeft > 14.9f)
		//{
		//	UltDemo();
		//}

		if (timeLeft <= 0)
		{
            timeLeft = 0.0f;
			GameOver();
            if (playedOnce == false)
            {
                soundManager.GetComponent<SoundManager>().PlayEndMusic();
                playedOnce = true;
            }
		}

		if (timeLeft <= 10)
		{
			text.color = new Color(0.49f, 0f, 0f, 1f);
		}
		else
		{
			text.color = new Color(0f, 0.4f, 0f, 1f);
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
        }
	}

	void GameOver()
	{
		//Time.timeScale = 0f;
		//Instructions.SetActive(true);
		//Instructions.GetComponent<Text>().fontSize = 100;
		//Instructions.GetComponent<Text>().text = "THANKS FOR PLAYING!!!";
        //player.GetComponent<PlayerController>().disablePlayer();
        GameObject ui = GameObject.Find("UI");
        ui.GetComponent<LevelCompleteMenu>().EnableMenu();

        player.GetComponent<FSMComboSystem>().UltimateEndGame();

        mainCamera.GetComponent<AudioSource>().enabled = false;

        //soundManager.SetActive(false);
        spawnManager.SetActive(false);



    }

	void UltDemo()
	{
		Instructions.SetActive(true);
		scoreManager.ChargeUlt(100);
	}

    IEnumerator DisableIntroText()
    {
        yield return new WaitForSeconds(5f);
    }
}
