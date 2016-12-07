using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	public GameObject MetricManager;
	public GameObject SoundManager;
	public GameObject SpawnManager;

	// Use this for initialization
	void Awake ()
	{ }
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public GameObject SpawnMetric()
	{
		return Instantiate(MetricManager) as GameObject;
	}

	public GameObject SpawnSpawner()
	{
		return Instantiate(SpawnManager, this.transform) as GameObject;
	}

	public GameObject SpawnSound()
	{
		return Instantiate(SoundManager, this.transform) as GameObject;
	}
}
