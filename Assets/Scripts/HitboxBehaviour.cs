using UnityEngine;
using System.Collections;

public class HitboxBehaviour : MonoBehaviour
{
	LevelManager levelManager;
    SoundManager soundManager;
    FSMComboSystem comboSystem;
    float lightPushForce = .1f;
    float heavyPushForce = 20.0f;

	// Use this for initialization
	void Start ()
	{
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
		GameObject sm = GameObject.Find("SoundManager");
		if (!sm)
		{
			sm = levelManager.SpawnSound();
		}
		soundManager = sm.GetComponent<SoundManager>();

		comboSystem = GameObject.Find("Spy").GetComponent<FSMComboSystem>();
        Debug.Log("INITILIZAED");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("HIT");
        if (col.gameObject.tag == "Enemy")
        {
            if (comboSystem.GetLaunch() == true)
            {
                soundManager.PlayEnderHit();
                Camera.main.GetComponent<ScreenShake>().ShakeCamera(.25f, .2f);

                col.gameObject.GetComponent<CollisionBehavior>().enablePhysics();
                Vector3 launchVector = col.gameObject.transform.position - this.transform.position;
                launchVector.Normalize();
                col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(launchVector.x, launchVector.y, 0) * heavyPushForce, ForceMode.Impulse);
                col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(2);

                StartCoroutine(DisablePhysics(col));
                //StartCoroutine(HitStop());

                comboSystem.ResetLaunch();
            }
            if (comboSystem.GetUpperCut() == true)
            {
                soundManager.PlayEnderHit();

                Camera.main.GetComponent<ScreenShake>().ShakeCamera(.35f, .2f);

                col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(1);

                //StartCoroutine(HitStop());

                comboSystem.ResetUppercut();
            }
            else
            {
				Camera.main.GetComponent<ScreenShake>().ShakeCamera(.05f, .2f);
				soundManager.PlayLightHit();
                col.gameObject.GetComponent<CollisionBehavior>().enablePhysics();
                col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(0);
                Vector3 launchVector = col.gameObject.transform.position - this.transform.position;
                launchVector.Normalize();
                col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(launchVector.x, launchVector.y, 0) * lightPushForce, ForceMode.Impulse);
                StartCoroutine(DisablePhysics(col));
            }
        }
    }


    public static IEnumerator DisablePhysics(Collider col)
    {

        if (col.gameObject.GetComponent<CollisionBehavior>())
        {
            yield return new WaitForSeconds(.5f);
			if (col.gameObject.GetComponent<CollisionBehavior>())
			{
				col.gameObject.GetComponent<CollisionBehavior>().disablePhysics();
			}
        }
    }

    IEnumerator HitStop()
    {
        Time.timeScale = 0.0f;
        float pauseEndTime = Time.realtimeSinceStartup + 0.03f;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1.0f;
    }
}
