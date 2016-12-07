using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashingText : MonoBehaviour
{
	bool isBlinking = false;
	GameObject multiplier;
    Text multiplierText;
	// Use this for initialization
	void Awake ()
	{
		multiplier = GameObject.Find("Multiplier");
        multiplierText = multiplier.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void BlinkSequence(Color color, Color revertColor)
	{
		isBlinking = true;
		StartCoroutine(BlinkText(color, revertColor));
		StartCoroutine(StopBlinking(revertColor));
	}

	//function to blink the text 
	IEnumerator BlinkText(Color color, Color revertColor)
	{
		//blink it forever. You can set a terminating condition depending upon your requirement. Here you can just set the isBlinking flag to false whenever you want the blinking to be stopped.
		while (isBlinking)
		{
            //set the Text's text to blank
            multiplierText.color = revertColor;
			//display blank text for 0.5 seconds
			yield return new WaitForSeconds(.05f);
            //display “I AM FLASHING TEXT” for the next 0.5 seconds
            multiplierText.color = color;
			yield return new WaitForSeconds(.05f);
		}
        multiplierText.color = revertColor;
	}
	//your logic here. I have set the isBlinking flag to false after 5 seconds
	IEnumerator StopBlinking(Color revertColor)
	{
		//wait for 5 seconds
		yield return new WaitForSeconds(0.2f);
		//stop the blinking
		isBlinking = false;
        multiplierText.color = revertColor;
		//set a different text just for sake of clarity
		//flashingText.text = staticText;
	}
}
