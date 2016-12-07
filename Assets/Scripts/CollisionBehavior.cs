using UnityEngine;
using System.Collections;

public class CollisionBehavior : MonoBehaviour
{
	LevelManager levelManager;
	public Transform deadTransform;

	ScoreManager scoreManager;
	SoundManager soundManager;

    float shakeTimer = 0f;
    float shakeAmount = 0.1f;

    Color originalColor;

	public int Health = 10;

    SpawnText multiplierPopup;

    StateMachine_EnemyPrototype enemyPrototype;

	Vector3 homePos;
	int homeCol;

	// Use this for initialization
	void Start ()
	{
		scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();
        multiplierPopup = GameObject.Find("OnScreenText").GetComponent<SpawnText>();

		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
		GameObject sm = GameObject.FindGameObjectWithTag("SoundManager");
		if (!sm)
		{
			sm = levelManager.SpawnSound();
		}
		soundManager = sm.GetComponent<SoundManager>();
		scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();

        if (GetComponent<StateMachine_EnemyPrototype>())
        {
            enemyPrototype = GetComponent<StateMachine_EnemyPrototype>();
        }

		homePos = transform.position;
		homeCol = Mathf.RoundToInt(homePos.x);
		homeCol = (homeCol + 15) / 3;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (shakeTimer > 0)
		{
			Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

			transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

			shakeTimer -= Time.deltaTime;
        }
		else
		{
			shakeTimer = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Spy" && enemyPrototype.GetAttackingStatus())
        {
            scoreManager.DecreaseMultiplier();
            scoreManager.ResetComboCount();
            soundManager.PlayPlayerHurt();
			multiplierPopup.SpawnPrefab(transform.position, -1);
		}
    }

    public void enablePhysics()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(DisablePhysics());
    }

    public void disablePhysics()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void TakeDamage(int modifier)
    {
		shakeTimer = 0.2f;

		switch (modifier)
		{
			case 0: // normal hit
				Health -= 1;
				scoreManager.AddScore(50);
				scoreManager.IncreaseMultiplier(1);
				multiplierPopup.SpawnPrefab(transform.position, 1);
				//soundManager.PlayExplosion();
				break;
			case 1: // correct ender
                shakeTimer = 0.3f;
				Health -= 7;
				scoreManager.AddScore(500);
				scoreManager.IncreaseMultiplier(2);
				scoreManager.ChargeUlt(5);
               multiplierPopup.SpawnPrefab(transform.position, 2);
				break;

			case 2: // incorrect ender
				Health -= 2;
				scoreManager.AddScore(500);
                scoreManager.ChargeUlt(3);
                // multiplierPopup.SpawnPrefab(transform.position);
                break;

		}

        if (Health <= 0)
        {
			Die();
        }
    }

	public void Die()
	{
		GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnManager>().RemoveFromSpawnColumn(homeCol);
		// spawn dead prefab
		Instantiate(deadTransform, transform.position, transform.rotation);
        scoreManager.IncrementComboCount();
		Destroy(gameObject);
		scoreManager.IncreaseMultiplier(3);
		scoreManager.ChargeUlt(3);
		//multiplierPopup.SpawnPrefab(transform.position, 3);
	}

    public void DieEndGame()
    {
        // spawn dead prefab
        Instantiate(deadTransform, transform.position, transform.rotation);
        Destroy(gameObject);
    }



    IEnumerator DisablePhysics()
    {
            yield return new WaitForSeconds(.5f);
            GetComponent<CollisionBehavior>().disablePhysics();


    }
}
