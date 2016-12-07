using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    int currentPage = 0;

	LevelManager levelManager;
	SoundManager soundManager;

	Camera mainCamera;

	void Awake()
	{
		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        pages[0].SetActive(true);
        if (pages[0].GetComponent<AutoType>())
        {
            pages[0].GetComponent<AutoType>().OpenPage();
        }
	}

    public void NextPage()
    {
        pages[currentPage].SetActive(false);
        currentPage++;
		if (currentPage == 2)
		{
			mainCamera.GetComponent<AudioSource>().enabled = false;
			soundManager.PlaySiren();
		}
		if (currentPage == 3)
		{
			soundManager.PlayExplosion();
		}
        if (currentPage == pages.Length && gameObject.name.Equals("TextPages"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                SceneManager.LoadSceneAsync(2);
            }
        }
        else if (currentPage < pages.Length)
        {
            pages[currentPage].SetActive(true);
            if (pages[currentPage].GetComponent<AutoType>())
            {
                pages[currentPage].GetComponent<AutoType>().OpenPage();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
