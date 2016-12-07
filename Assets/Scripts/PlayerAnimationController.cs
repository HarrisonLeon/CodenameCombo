using UnityEngine;
using System.Collections;
using InControl;

public class PlayerAnimationController : MonoBehaviour
{
	Animator anim;

	static int idleState = Animator.StringToHash("Combat Layer.Idle");
	static int punchState1 = Animator.StringToHash("Combat Layer.RightPunch");
	static int punchState2 = Animator.StringToHash("Combat Layer.LeftPunch");
	static int punchState3 = Animator.StringToHash("Combat Layer.Uppercut");
	static int punchState4 = Animator.StringToHash("Combat Layer.DoublePunch");
	static int kickState1 = Animator.StringToHash("Combat Layer.KickOne");
	static int kickState2 = Animator.StringToHash("Combat Layer.KickTwo");
	static int kickState3 = Animator.StringToHash("Combat Layer.KickThree");

	void Awake ()
    {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {}

	public void ResetAnimation()
	{
        anim.Play(idleState, 1);
	}

	public void AnimatePunch()
	{
		if (anim.GetCurrentAnimatorStateInfo(1).fullPathHash == punchState1)
		{
            anim.Play(punchState2, 1);
        }
		else
		{
            anim.Play(punchState1, 1);
        }

		// Legacy Animation Stuff
		//if (anim.IsPlaying("LeftPunch"))
		//{
		//	anim.Play("RightPunch", PlayMode.StopAll);
		//	anim.PlayQueued("Idle", QueueMode.CompleteOthers);
		//}
		//else
		//{
		//	anim.Play("LeftPunch", PlayMode.StopAll);
		//	anim.PlayQueued("Idle", QueueMode.CompleteOthers);
		//}
	}

	public void AnimateUppercut()
	{
		anim.Play(punchState3, 1);
	}

	public void AnimateDoublePunch()
	{
        anim.Play(punchState4, 1);
        //anim.Play("DoublePunch", PlayMode.StopAll);
        //anim.PlayQueued("Idle", QueueMode.CompleteOthers);
    }

	public void AnimateKick()
	{
		if (anim.GetCurrentAnimatorStateInfo(1).fullPathHash == kickState1)
		{
			anim.Play(kickState2, 1);
		}
		else
		{
			anim.Play(kickState1, 1);
		}
	}

	public void AnimateLargeKick()
	{
		anim.Play(kickState3, 1);
	}

	public void AnimateUltimate()
	{
        anim.Play(punchState4, 1);
        //anim.Play("DoublePunch", PlayMode.StopAll);
        //anim.PlayQueued("Idle", QueueMode.CompleteOthers);
    }
}
