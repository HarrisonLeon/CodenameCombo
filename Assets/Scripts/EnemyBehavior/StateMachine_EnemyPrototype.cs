using UnityEngine;
using System.Collections;

using MonsterLove.StateMachine;

public enum EnemyStates
{
	Passive,
	Follow,
	Attack,
	Retreat
}

public class StateMachine_EnemyPrototype : MonoBehaviour
{
	LevelManager levelManager;

	StateMachine<EnemyStates> enemyFSM;
	EnemyAnimationController animController;
	Transform player;

	public float MinPatrolTime;
	public float MaxPatrolTime;
	public float MinDist;
	public float MoveSpeed;

	bool ready;
    bool attacking;

	Vector3 homePos;
	int homeCol;
	// public float DiveSpeed;

	Vector3 moveDirection;

	bool isPatrolling = true;

	ScoreManager scoreManager;
	SoundManager soundManager;

	Coroutine followTimer;

	void Awake()
	{
		enemyFSM = StateMachine<EnemyStates>.Initialize(this, EnemyStates.Passive);
		animController = GetComponentInChildren<EnemyAnimationController>();
		player = GameObject.FindWithTag("Player").transform;

		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
		GameObject sm = GameObject.FindGameObjectWithTag("SoundManager");
		if (!sm)
		{
			sm = levelManager.SpawnSound();
		}
		soundManager = sm.GetComponent<SoundManager>();
		scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();

		ready = false;
        attacking = false;
	}

	void Start()
	{
		homePos = transform.position;
		homeCol = Mathf.RoundToInt(homePos.x);
		homeCol = (homeCol + 15) / 3;

		GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnManager>().AddToSpawnColumn(homeCol);
	}

	void Update()
	{
		if (isPatrolling)
		{
			float rotationAmount = 360f;
			transform.Rotate(rotationAmount * Time.deltaTime, 0f, 0f);
		}
	}

	IEnumerator Passive_Enter()
	{
		ready = false;
		float patrolTime = Random.Range(MinPatrolTime, MaxPatrolTime);
		yield return new WaitForSeconds(patrolTime);
		isPatrolling = false;
		enemyFSM.ChangeState(EnemyStates.Follow);
	}

	void Follow_Enter()
	{
		transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
	}

	void Follow_Update()
	{
		StartCoroutine("FollowTimer");
		if (Vector3.Distance(transform.position, player.position) >= MinDist && !ready)
		{
			moveDirection = (player.position - transform.position).normalized;
			transform.position += moveDirection * Time.deltaTime * MoveSpeed;
		}
		else
		{
			enemyFSM.ChangeState(EnemyStates.Attack);
		}
	}

	void Attack_Enter()
	{
		animController.AnimateCharge();
	}

	void Retreat_Update()
	{
		if (!(Vector3.Distance(transform.position, homePos) < 0.1f))
		{
			moveDirection = (homePos - transform.position).normalized;
			transform.position += moveDirection * Time.deltaTime * MoveSpeed;
		}
		else
		{
			enemyFSM.ChangeState(EnemyStates.Passive);
		}
	}

	//IEnumerator Dive()
	//{
	//	// time spent between dives
	//	yield return new WaitForSeconds(2);
	//	isDiving = true;
	//	StartCoroutine(Halt());
	//}

	//IEnumerator Halt()
	//{
	//	// time spent diving
	//	yield return new WaitForSeconds(0.25f);
	//	isDiving = false;
	//	StartCoroutine(Dive());
	//}

	//IEnumerator CanAttack()
	//{
	//	// attack speed
	//	yield return new WaitForSeconds(.4f);
	//	canAttack = true;
	//}

    //need to make the sphere collider scale on attack
    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(0.25f);
	}

	IEnumerator Patrol()
	{
		Debug.Log("Start patrol");
		while (isPatrolling)
		{
			Debug.Log("Begin loop");
			moveDirection = Vector3.left;
			yield return new WaitForSeconds(.3f);
			moveDirection = Vector3.right;
			yield return new WaitForSeconds(.3f);
		}
	}

	IEnumerator FollowTimer()
	{
		yield return new WaitForSeconds(5f);
		ready = true;
	}

	public void Retreat()
	{
        attacking = false;
		enemyFSM.ChangeState(EnemyStates.Retreat);
	}

    public bool GetAttackingStatus()
    {
        return attacking;
    }

	public void SetAttackingStatus(bool status)
	{
		attacking = status;
	}
}

