using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HitboxTypes
{
	RightHand,
	LeftHand,
	DoubleHand,
	RightFoot,
	LeftFoot,
	DoubleFoot
}

public class HitboxManager : MonoBehaviour
{
	// Set these in the editor
	public GameObject LeftHandHitbox;
	public GameObject RightHandHitbox;
	public GameObject LeftFootHitbox;
    public GameObject RightFootHitbox;

	void Start()
	{
		DisableAllHitboxes();
	}

	public void DisableAllHitboxes()
	{
		LeftHandHitbox.SetActive(false);
		RightHandHitbox.SetActive(false);
		LeftFootHitbox.SetActive(false);
		RightFootHitbox.SetActive(false);
	}

	public void EnableHitbox(HitboxTypes hitboxType)
	{
		DisableAllHitboxes();

		switch (hitboxType)
		{
			case HitboxTypes.RightHand:
				RightHandHitbox.SetActive(true);
				break;

			case HitboxTypes.LeftHand:
				LeftHandHitbox.SetActive(true);
				break;

			case HitboxTypes.DoubleHand:
				RightHandHitbox.SetActive(true);
				LeftHandHitbox.SetActive(true);
				break;

			case HitboxTypes.RightFoot:
				RightFootHitbox.SetActive(true);
				break;

			case HitboxTypes.LeftFoot:
				LeftFootHitbox.SetActive(true);
				break;

			case HitboxTypes.DoubleFoot:
				RightFootHitbox.SetActive(true);
				LeftFootHitbox.SetActive(true);
				break;

			default:
				DisableAllHitboxes();
				break;
		}
	}

	public void DisableHitbox(HitboxTypes hitboxType)
	{ }

	//public List<BoxCollider> getColliders()
	//{
	//	return colliders;
	//}
}