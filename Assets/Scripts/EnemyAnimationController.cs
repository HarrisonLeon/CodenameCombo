using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour
{
	Animator animator;

	StateMachine_EnemyPrototype enemySM;

	static int idleState = Animator.StringToHash("Base Layer.moving");
	static int chargeState = Animator.StringToHash("Base Layer.charging");
	static int attackState = Animator.StringToHash("Base Layer.attack");

	float chargeTime = 0f;

	// Use this for initialization
	void Start ()
	{
		enemySM = GetComponent<StateMachine_EnemyPrototype>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == chargeState)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.9f)
			{
				StartCoroutine(AnimateAttack());
			}
		}
	}

	public void AnimateMovement()
	{
		animator.Play(idleState);
	}

	public void AnimateCharge()
	{
		animator.Play(chargeState);
	}

	IEnumerator AnimateAttack()
	{
		enemySM.SetAttackingStatus(true);
		yield return new WaitForSeconds(.25f);
		animator.Play(attackState);
		yield return new WaitForSeconds(0.3f);
		enemySM.Retreat();
	}
}
