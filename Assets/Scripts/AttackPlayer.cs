using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour
{
    Transform _player;
	public float MinDist;
    public float MoveSpeed;
	public float DiveSpeed;

    bool isDiving = false;
	bool canAttack = true;

	ScoreManager scoreManager;
	public SoundManager soundManager;

	void Awake()
	{
		StartCoroutine(Dive());

		_player = GameObject.FindWithTag("Player").transform;

		scoreManager = GameObject.FindWithTag("Score").GetComponent<ScoreManager>();
		soundManager = GameObject.FindWithTag("Sound").GetComponent<SoundManager>();
	}

    void Start()
    {}

    void Update()
    {
		if (Vector3.Distance(transform.position, _player.position) >= MinDist)
		{
			//Vector3 towards = (_player.position - transform.position).normalized;

			//transform.position += towards * Time.deltaTime * (isDiving ? DiveSpeed : MoveSpeed);
		}
		else if (canAttack)
		{
			scoreManager.DecreaseMultiplier();
			soundManager.PlayPlayerHurt();
			canAttack = false;
			StartCoroutine(CanAttack());
		}
	}

	IEnumerator Dive()
    {
        yield return new WaitForSeconds(2);
        isDiving = true;
        StartCoroutine(Halt());
    }

    IEnumerator Halt()
    {
        yield return new WaitForSeconds(0.25f);
        isDiving = false;
        StartCoroutine(Dive());
    }

	IEnumerator CanAttack()
	{
		yield return new WaitForSeconds(.4f);
		canAttack = true;
	}
}

