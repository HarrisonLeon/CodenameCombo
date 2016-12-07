using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		StartCoroutine(Death());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Death()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
