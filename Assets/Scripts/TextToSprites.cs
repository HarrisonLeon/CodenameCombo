using UnityEngine;
using System.Collections;

public class TextToSprites : MonoBehaviour
{
	public SpriteRenderer[] buttonSprites;

	TextMesh textMesh;

	// Use this for initialization
	void Start ()
	{
		textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		string comboText = textMesh.text;
		if (comboText.Equals(string.Empty))
		{
			buttonSprites[0].sprite = null;
			buttonSprites[1].sprite = null;
			buttonSprites[2].sprite = null;
		}
		int i = 0;
		foreach (char c in comboText)
		{
			if (i <= 2)
			{
				switch (c)
				{
					case 'A':
						buttonSprites[i].sprite = Resources.Load<Sprite>("Sprites/TouchButton_A");
						break;
					case 'X':
						buttonSprites[i].sprite = Resources.Load<Sprite>("Sprites/TouchButton_X");
						break;
					default:
						break;
				}

				i++;
			}
		}
		textMesh.text = "";
	}
}
