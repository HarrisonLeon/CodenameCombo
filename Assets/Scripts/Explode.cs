using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	float radius = 1f;
	float power = 800f;

	// Use this for initialization
	void Start ()
	{
		Vector3 explosionPos = transform.position;
		Collider[] colls = Physics.OverlapSphere(explosionPos, radius);

		foreach (Collider collider in colls)
		{
			if (collider.GetComponent<Rigidbody>())
			{
				collider.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, Random.Range(-1.0f, 1.0f));
			}
				
		}

	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
