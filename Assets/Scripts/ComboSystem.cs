//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Collections;
//using InControl;

//public class ComboSystem : MonoBehaviour {

//    HitboxManager hitboxManager;
//    BoxCollider activeHitbox;
//    float pushForce = 50.0f;
//    bool launch;

//    public SoundManager soundManager;
//	ScoreManager scoreManager;

//	public TextMesh ComboUI;

//	PlayerAnimationController pAnimationController;

//    enum attackStages {
//        attack1,
//        attack2,
//        attack3,
//        attack4,
//        attack5
//    }

//    attackStages currentState;

//    float timeToChain = 0.3f;   //you have this long to press the next attack
//    // float attackCooldown = 0.1f;  // let the attack play out a little before you can do the next one
//    // float chainCooldown = 0f;   // cooldown once you do a full combo
       

//	// Use this for initialization
//	void Start ()
//	{
//		pAnimationController = GetComponentInChildren<PlayerAnimationController>();

//        launch = false;

//        hitboxManager = this.gameObject.GetComponent<HitboxManager>();
//		scoreManager = GameObject.FindWithTag("Score").GetComponent<ScoreManager>();

//		//start off with attack1 flag
//		currentState = attackStages.attack1;

//        resetChainTimer();
//	}
	
//	// Update is called once per frame
//	void Update () {
//        if (timeToChain > 0f)
//        {
//            timeToChain -= Time.deltaTime;
//            //Debug.Log(timeToChain);
//        }
//        else
//        {
//            currentState = attackStages.attack1;
//            activeHitbox = null;
//			launch = false;
//			ComboUI.text = "";
//            ComboUI.color = Color.black;

//            ComboUI.transform.forward = Vector3.Normalize(new Vector3(0f, 0f, 1f));

//            //if (!gameObject.GetComponent<PlayerController>().getFacingRight())
//            //{
//            //    Debug.Log(gameObject.GetComponent<PlayerController>().getFacingRight());
//            //    ComboUI.transform.forward = Vector3.Normalize(new Vector3(0f, 0f, 1f)); 
//            //}
//            //else
//            //{
//            //    Debug.Log(gameObject.GetComponent<PlayerController>().getFacingRight());
//            //    ComboUI.transform.forward = Vector3.Normalize(new Vector3(0f, 0f, 1f));
//            //}

//   //         hitboxManager.getColliders()[0].enabled = false;
//   //         hitboxManager.getColliders()[1].enabled = false;
//   //         hitboxManager.getColliders()[2].enabled = false;
//   //         hitboxManager.getColliders()[3].enabled = false;
//   //         hitboxManager.getColliders()[4].enabled = false;

//   //         hitboxManager.getColliders()[0].gameObject.SetActive(false);
//			//hitboxManager.getColliders()[1].gameObject.SetActive(false);
//			//hitboxManager.getColliders()[2].gameObject.SetActive(false);
//   //         hitboxManager.getColliders()[3].gameObject.SetActive(false);
//   //         hitboxManager.getColliders()[4].gameObject.SetActive(false);
//        }
//		// Y or TRIANGLE - Ultimate Attack
//		if (InputManager.ActiveDevice.Action4.WasPressed)
//		{
//			attack(-1);
//		}
//		// A or X - Basic Punch
//        if (InputManager.ActiveDevice.Action1.WasPressed)
//        {
//            attack(0);
//		}
//		// B or CIRCLE - Red Punch
//		else if (InputManager.ActiveDevice.Action2.WasPressed)
//		{
//            attack(1);
//        }
//		// X or SQUARE - Blue Punch
//        else if (InputManager.ActiveDevice.Action3.WasPressed)
//        {
//            attack(2);
//        }
//        else if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        }
      
//	}

//    void resetChainTimer()
//    {
//        timeToChain = 0.3f;
//    }

//	void attack(int attackType)
//	{
//		switch (attackType)
//		{
//			case -1:
//				if (scoreManager.UltIsCharged())
//				{
//					pAnimationController.AnimateUltimate();
//					UltimateAttack();
//				}
//				break;

//			case 0:
//				if (currentState == attackStages.attack1)
//				{
//					pAnimationController.AnimatePunch();
//					ComboUI.color = Color.black;
//					ComboUI.text = "A";
//					//Debug.Log("FIRST");

//					StartCoroutine(StartUpDelay(1));
//				}

//				else if (currentState == attackStages.attack2)
//				{
//					pAnimationController.AnimatePunch();
//					ComboUI.text = "AA";
//					//Debug.Log("SECOND");
//					StartCoroutine(StartUpDelay(2));

//				}
//				else if (currentState == attackStages.attack3)
//				{
//					pAnimationController.AnimatePunch();
//					ComboUI.text = "AAA";
//					//Debug.Log("THIRD");
//					StartCoroutine(StartUpDelay(3));
//				}
//				break;

//			case 1:
//				if (currentState == attackStages.attack4)
//				{
//					pAnimationController.AnimatePunch();
//					ComboUI.color = Color.red;
//					ComboUI.text = "AAAB";
//					//Debug.Log("FOURTH");
//					StartCoroutine(StartUpDelay(4));
//				}
//				break;

//			case 2:
//				if (currentState == attackStages.attack4)
//				{
//					pAnimationController.AnimatePunch();
//					ComboUI.color = Color.blue;
//					ComboUI.text = "AAAX";
//					//Debug.Log("FIFTH");
//					StartCoroutine(StartUpDelay(5));

//					return;
//				}
//				break;
//		}
//	}

//    void OnTriggerEnter(Collider col)
//    {
//        if (col.gameObject.tag == "Enemy")
//        {

//            // Debug.Log("HIT ENEMY");
//            //if (col.gameObject.name == "EnemyRed(Clone)")
//            //{
//            //    col.gameObject.GetComponent<CollisionBehavior>();
//            //}
//            //if (col.gameObject.name == "EnemyBlue(Clone)")
//            //{
                
//            //}

//            //add slight pushback on hit
//           col.gameObject.GetComponent<CollisionBehavior>().enablePhysics();
//            Vector3 launchVector = col.gameObject.transform.position - this.transform.position;
//            launchVector.Normalize();
//            col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(launchVector.x, launchVector.y, 0) * pushForce/55.0f, ForceMode.Impulse);
//            if (col.gameObject != null)
//            {
//                StartCoroutine(DisablePhysics(col));  //this is set to the same time as the shaking duration
//            }

//            //if (currentState == attackStages.attack1 || currentState == attackStages.attack2 || currentState == attackStages.attack3){
//            //    soundManager.PlayLightHit();
//            //    return;
//            //}

//            //if (currentState == attackStages.attack4)
//            //{
//            //    soundManager.PlayMediumHit();
//            //    return;
//            //}


//            if (launch == true)
//            {
//                Camera.main.GetComponent<ScreenShake>().ShakeCamera(.15f, .5f);
//                col.gameObject.GetComponent<CollisionBehavior>().enablePhysics();
//                //col.gameObject.GetComponent<AudioSource>().Play();
//                //if (hitboxManager.getColliders()[3].enabled == true)
//                //{
//                //    soundManager.PlayEnderHit();
//                //    return;
//                //}
//                //if (hitboxManager.getColliders()[4].enabled == true)
//                //{
//                //    soundManager.PlayEnderHit2();
//                //    return;
//                //}


//                // launchVector = col.gameObject.transform.position - this.transform.position;
//                //launchVector.Normalize();
//                //add a lot of pushback on hit
//                col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(launchVector.x, launchVector.y, 0) * pushForce, ForceMode.Impulse);
//                StartCoroutine(DisablePhysics(col));
//            }

//            //sound, super jank
//            if (activeHitbox == hitboxManager.getColliders()[0] || activeHitbox == hitboxManager.getColliders()[1])
//            {
//                soundManager.PlayLightHit();
//                col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(0);
//            }
//            if (activeHitbox == hitboxManager.getColliders()[2])
//            {
//                soundManager.PlayMediumHit();
//                col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(0);
//            }
//            if (activeHitbox == hitboxManager.getColliders()[3])
//            {
//                soundManager.PlayEnderHit();
//                if (col.gameObject.name == "EnemyRed(Clone)")
//                {
//                    col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(1); // matching ender
//                }
//                else
//                {
//                    col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(2); // non-matching ender
//				}
//            }
//            if (activeHitbox == hitboxManager.getColliders()[4])
//            {
//                soundManager.PlayEnderHit2();
//                if (col.gameObject.name == "EnemyBlue(Clone)")
//                {
//                    col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(1); // matching ender
//                }
//                else
//                {
//                    col.gameObject.GetComponent<CollisionBehavior>().TakeDamage(2); // non-matching ender
//                }

//            }
//        }
//    }

//	void UltimateAttack()
//	{
//		soundManager.PlayExplosion();
//		scoreManager.ResetUlt();
//		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//		foreach (GameObject e in enemies)
//		{
//			scoreManager.AddScore(200);
//			e.GetComponent<CollisionBehavior>().Die();
//		}
//	}

//    IEnumerator StartUpDelay(int i)
//    {
//        yield return new WaitForSeconds(0f);  // wait this long before the attack comes out

//        if (i == 1)
//        {

//            hitboxManager.getColliders()[3].enabled = false;
//            hitboxManager.getColliders()[3].gameObject.SetActive(false);
//            hitboxManager.getColliders()[4].enabled = false;
//            hitboxManager.getColliders()[4].gameObject.SetActive(false);
//            hitboxManager.getColliders()[0].enabled = true;
//            hitboxManager.getColliders()[0].gameObject.SetActive(true);
//            activeHitbox = hitboxManager.getColliders()[0];
//            currentState = attackStages.attack2;
//			//ComboUI.text += "J";
//			resetChainTimer();
//        }

//        if (i == 2)
//        {
//            hitboxManager.getColliders()[0].enabled = false;
//            hitboxManager.getColliders()[0].gameObject.SetActive(false);
//            hitboxManager.getColliders()[1].enabled = true;
//            hitboxManager.getColliders()[1].gameObject.SetActive(true);
//            activeHitbox = hitboxManager.getColliders()[1];
//            currentState = attackStages.attack3;
//			//ComboUI.text += "J";
//			resetChainTimer();
//        }

//        if (i == 3)
//        {
//            hitboxManager.getColliders()[1].enabled = false;
//            hitboxManager.getColliders()[1].gameObject.SetActive(false);
//            hitboxManager.getColliders()[2].enabled = true;
//            hitboxManager.getColliders()[2].gameObject.SetActive(true);
//            activeHitbox = hitboxManager.getColliders()[2];
//            currentState = attackStages.attack4;
//			//ComboUI.text += "J";
//			resetChainTimer();
//        }

//        if (i == 4)
//        {
//            hitboxManager.getColliders()[2].enabled = false;
//            hitboxManager.getColliders()[2].gameObject.SetActive(false);
//            hitboxManager.getColliders()[3].enabled = true;
//            hitboxManager.getColliders()[3].gameObject.SetActive(true);
//            activeHitbox = hitboxManager.getColliders()[3];
//            currentState = attackStages.attack1;
//            launch = true;
//			//ComboUI.text += "K";
//			resetChainTimer();

            
//        }
//        if (i == 5)
//        {
//            hitboxManager.getColliders()[2].enabled = false;
//            hitboxManager.getColliders()[2].gameObject.SetActive(false);
//            hitboxManager.getColliders()[4].enabled = true;
//            hitboxManager.getColliders()[4].gameObject.SetActive(true);
//            activeHitbox = hitboxManager.getColliders()[4];
//            currentState = attackStages.attack1;
//            launch = true;
//            //Debug.Log("SPECIAL");
//			//ComboUI.text += "L";
//			resetChainTimer();

//        }
//        // Debug.Log("3 FRAMES");
        
//    }

//    IEnumerator CooldownBetweenAttacks()
//    {
//        Debug.Log("8 FRAMES");
//        yield return new WaitForSeconds(1.33333f);
//    }


//    IEnumerator CooldownAfterEnder()
//    {
//        yield return new WaitForSeconds(.75f);
//    }

//    IEnumerator DisablePhysics(Collider col)
//    {
        
//        if (col.gameObject.GetComponent<CollisionBehavior>())
//        {
//            yield return new WaitForSeconds(.5f);
//            //col.gameObject.GetComponent<CollisionBehavior>().disablePhysics();
//        }
        
//    }

//	IEnumerator ClearComboText()
//	{
//		yield return new WaitForSeconds(1f - timeToChain);
//		ComboUI.text = "";
//		ComboUI.color = Color.black;
//	}
//}
