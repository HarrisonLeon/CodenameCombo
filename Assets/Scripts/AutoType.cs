using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

using InControl;

public class AutoType : MonoBehaviour
{
    public float letterPause = 0.0001f;
    public AudioClip sound;

    public PageManager imagePageManager;
    public PageManager textPageManager;

    GameObject nextPrompt;
    Text text;
    string message;
    bool isFinished;

    // Use this for initialization
    void Awake()
    {
        nextPrompt = GameObject.Find("NextPrompt");
        nextPrompt.SetActive(false);
        text = GetComponent<Text>();
        message = text.text;
        text.text = "";
        isFinished = false;
    }

    void Update()
    {
		// Input.GetKeyDown(KeyCode.Space)
		if (InputManager.ActiveDevice.Command.WasPressed)
		{
			if (SceneManager.GetActiveScene().buildIndex != 2)
			{
				SceneManager.LoadSceneAsync(2);
			}
		}
		else if (InputManager.ActiveDevice.AnyButtonWasPressed)
        {
            StopAllCoroutines();
            GetComponent<AudioSource>().Stop();
            if (isFinished)
            {
                textPageManager.NextPage();
                imagePageManager.NextPage();
            }
            else
            {
                text.text = message;
                isFinished = true;
                nextPrompt.SetActive(true);
            }
        }
    }

    public void OpenPage()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            text.text += letter;
            if (sound)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(sound);
            }
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        GetComponent<AudioSource>().Stop();
        isFinished = true;
        nextPrompt.SetActive(true);
    }
}