using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
	public GameObject WaveContainer;
	public Transform[] SpawnType;
	public float SpawnTime;

    //spawn the walls
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;

	int[] spawnColumns = new int[11];

	float leftBorder;
    float rightBorder;
    float topBorder;
    float bottomBorder;

    bool isSpawning = true;
	bool spawnAvailable = true;

    // Use this for initialization
    void Awake ()
	{
        float dist = (transform.position - Camera.main.transform.position).z;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + GetComponent<Renderer>().bounds.size.x / 2;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - GetComponent<Renderer>().bounds.size.x / 2;
        topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + GetComponent<Renderer>().bounds.size.y / 2;
        bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - GetComponent<Renderer>().bounds.size.y / 2;

        SpawnWalls();

        StartCoroutine(Spawn());

		// Dummy column
		spawnColumns[5] = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    void SpawnWalls()
    {
        // Debug.Log("WALLS WERE SET");
        leftWall.transform.position = new Vector3(leftBorder - 1.2f, 0, 0);
        rightWall.transform.position = new Vector3(rightBorder + 1.2f, 0, 0);
        topWall.transform.position = new Vector3(0, topBorder - 1.2f, 0);
        bottomWall.transform.position = new Vector3(0, bottomBorder + 1.2f, 0);
    }

	IEnumerator Spawn()
	{
        yield return new WaitForSeconds(SpawnTime);
		while (isSpawning)
		{
			//int probability = Random.Range(0, 100);

			//// 50% chance
			//if (probability < 50)
			//{
			//	SpawnWave(1);
			//}
			//// 35% chance
			//else if (probability < 85)
			//{
			//	SpawnWave(2);
			//}
			//// 15% chance
			//else
			//{
			//	SpawnWave(3);
			//}
			spawnAvailable = false;
			for (int i = 0; i < 11; i++)
			{
				if (spawnColumns[i] == 0)
				{
					// only spawn wave if there is space available
					spawnAvailable = true;
				}
			}

			if (spawnAvailable)
			{
				SpawnWave(1);
			}
			
			yield return new WaitForSeconds(SpawnTime);
		}

	}

	void SpawnWave(int waveIndex)
	{
		int spawnIdx = Random.Range(0, 2);
		switch (waveIndex)
		{
			case 1: // column wave
				bool valid = false;
				int column = 0;
				while (!valid)
				{
					int posColumn = Random.Range(0, 2);
					if (posColumn == 1) { column = Random.Range(1, 6); }
					if (posColumn == 0) { column = Random.Range(-5, 0); }
					if (spawnColumns[column + 5] == 0)
					{
						valid = true;
					}
				}
				
				if (SpawnType[spawnIdx].name == "EnemyPrototype")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Vector3 pos = new Vector3(column * 3, 3 * (i - 2), 0);
                        Instantiate(SpawnType[spawnIdx], pos, Quaternion.identity, WaveContainer.transform);
					}
                }
                if (SpawnType[spawnIdx].name == "TankPrototype")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3 pos = new Vector3(column * 3, 3 * (i - 1), 0);
                        Instantiate(SpawnType[spawnIdx], pos, Quaternion.identity, WaveContainer.transform);
					}
                }
				break;
			case 2: // tank wave in corners
				Instantiate(SpawnType[1], new Vector3(-10f, 9f, 0f), Quaternion.identity, WaveContainer.transform);
				Instantiate(SpawnType[1], new Vector3(10f, 9f, 0f), Quaternion.identity, WaveContainer.transform);
				Instantiate(SpawnType[1], new Vector3(10f, -7f, 0f), Quaternion.identity, WaveContainer.transform);
				Instantiate(SpawnType[1], new Vector3(-10f, -7f, 0f), Quaternion.identity, WaveContainer.transform);
				break;

			case 3: // ellipse wave
				int numberOfObjects = 10;
				float radiusX = 15f;
				float radiusY = 8f;
				for (int i = 0; i < 10; i++)
				{
					float angle = i * Mathf.PI * 2 / numberOfObjects;
					Vector3 pos = new Vector3(Mathf.Cos(angle) * radiusX, Mathf.Sin(angle) * radiusY, 0f);
					Instantiate(SpawnType[0], pos, Quaternion.identity);
				}
				break;

			default:
				break;
		}
	}

    public void DisableSpawn()
    {
        isSpawning = false;
    }

	public void AddToSpawnColumn(int col)
	{
		spawnColumns[col]++;
	}

	public void RemoveFromSpawnColumn(int col)
	{
		spawnColumns[col]--;
	}

}



//public GameObject prefab;
//public int numberOfObjects = 10;
//public float radius = 5f;

//void Start()
//{
//	for (int i = 0; i < numberOfObjects; i++)
//	{
//		float angle = i * Mathf.PI * 2 / numberOfObjects;
//		Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
//		Instantiate(prefab, pos, Quaternion.identity);
//	}
//}

