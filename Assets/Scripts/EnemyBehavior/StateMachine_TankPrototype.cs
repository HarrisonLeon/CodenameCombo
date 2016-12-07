using UnityEngine;
using System.Collections;

using MonsterLove.StateMachine;

public enum TankStates
{
	Passive,
	Attack
}

public class StateMachine_TankPrototype : MonoBehaviour
{
	StateMachine<EnemyStates> enemyFSM;
	Transform player;
	public float PatrolTime;
	public float MinDist;
	public float MoveSpeed;

	Vector3 moveDirection;

	bool isPatrolling = true;
	bool isDiving = false;
	bool canAttack = true;

	ScoreManager scoreManager;
	public SoundManager soundManager;

	void Awake()
	{
		enemyFSM = StateMachine<EnemyStates>.Initialize(this, EnemyStates.Passive);

		player = GameObject.FindWithTag("Player").transform;

		scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();
		soundManager = GameObject.FindWithTag("Sound").GetComponent<SoundManager>();
	}

	void Start()
	{ }

	void Update()
	{
		transform.position += moveDirection * Time.deltaTime * MoveSpeed;

		if (isPatrolling == true)
		{
			float rotationAmount = 180f;
			transform.Rotate(rotationAmount * Time.deltaTime, 0f, 0f);
		}
	}

	IEnumerator Passive_Enter()
	{
		StartCoroutine(Patrol());
		yield return new WaitForSeconds(PatrolTime);
		isPatrolling = false;
		enemyFSM.ChangeState(EnemyStates.Attack);
	}

	void Passive_Update()
	{
		
	}

	void Attack_Enter()
	{ }

	void Attack_Update()
	{
		if (Vector3.Distance(transform.position, player.position) >= MinDist)
		{
			moveDirection = (player.position - transform.position).normalized;
		}
		else if (canAttack)
		{
			scoreManager.DecreaseMultiplier();
            scoreManager.ResetComboCount();
			soundManager.PlayPlayerHurt();
			canAttack = false;
			StartCoroutine(CanAttack());
		}
	}

	IEnumerator CanAttack()
	{
		// attack speed
		yield return new WaitForSeconds(.4f);
		canAttack = true;
	}

	IEnumerator Patrol()
	{
		Debug.Log("Start patrol");
		while (isPatrolling)
		{
			Debug.Log("Begin loop");
			moveDirection = Vector3.left;
			yield return new WaitForSeconds(1f);
			moveDirection = Vector3.right;
			yield return new WaitForSeconds(1f);
		}
		transform.Rotate(0f, 0f, 0f);
	}
}

