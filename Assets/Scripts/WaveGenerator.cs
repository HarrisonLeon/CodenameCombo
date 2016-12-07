using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour
{
    public GameObject WaveContainer;
    public Transform[] SpawnType;
    public float SpawnTime;

    bool[] validSpawns = new bool[8];
    int[] spawnCount = new int[8];

    bool isSpawning = true;

    // Use this for initialization
    void Awake()
    {
        StartCoroutine(Spawn());

        for (int i = 0; i < 8; i++)
        {
            validSpawns[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    { }

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
            SpawnWave(1);
            yield return new WaitForSeconds(SpawnTime);
        }

    }

    void SpawnWave(int waveIndex)
    {
        int spawnIndex = -1;
        bool valid = false;
        while (!valid)
        {
            spawnIndex = Random.Range(0, 7);
            if (validSpawns[spawnIndex])
            {
                valid = true;
            }
        }

        switch (waveIndex)
        {
            case 1: // column wave
                int column = Random.Range(1, 6);
                for (int i = 0; i < 6; i++)
                {
                    Vector3 pos = new Vector3(column * 3, 3 * (i - 2), 0);
                    Object obj = Instantiate(SpawnType[spawnIndex], pos, Quaternion.identity, WaveContainer.transform);
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
}
