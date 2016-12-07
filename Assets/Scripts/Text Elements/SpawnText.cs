using UnityEngine;
using System.Collections;

public class SpawnText : MonoBehaviour {

    public GameObject multiplierUpText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnPrefab(Vector3 position, int multiNum)
    {
        GameObject obj = Instantiate(multiplierUpText, position, Quaternion.identity) as GameObject;
		TextMesh text = obj.GetComponent<TextMesh>();
		if (multiNum == -1)
		{
			text.color = new Color(0.5f, 0f, 0f);
			text.fontSize = text.fontSize * 2;
			text.text = "x1/2";
		}
		else
		{
			text.color = new Color(0f, 1f, 0f);
			text.text = "+" + multiNum.ToString();
		}
    }

	public void SpawnPrefab(Vector3 position, string str)
	{
		GameObject obj = Instantiate(multiplierUpText, position, Quaternion.identity) as GameObject;
		TextMesh text = obj.GetComponent<TextMesh>();
		text.text = str;
		text.color = new Color(1f, 0.843f, 0f);
	}
}
