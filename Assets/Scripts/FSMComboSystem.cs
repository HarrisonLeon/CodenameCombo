using UnityEngine;
using System.Collections;

using InControl;
using MonsterLove.StateMachine;

public enum AttackStates
{
	Start,
	Finish,
	One,				// A or X
	Two,				// B or CIRCLE
	Three,				// X or SQUARE
	Four,               // Y or TRIANGLE

	Uppercut,		// COMBO: Uppercut
	DoublePunch,	// COMBO: Double Punch
	DashKick		// COMBO: Large Kick
}

public class FSMComboSystem : MonoBehaviour
{
	LevelManager levelManager;
	MetricManager metricManager;

    HitboxManager hitboxManager;
    StateMachine<AttackStates> attackFSM;
    PlayerAnimationController pAnimationController;
    TextMesh comboText;

    PlayerController playerController;

    SoundManager soundManager;
    ScoreManager scoreManager;

	public SpawnText successPopup;

    //the jank stuff, need to incorporate state machine
    bool launch;

    bool uppercut;

    const float timeToChain = 0.3f;

	float shortComboCooldown = 0.5f;
	float longComboCooldown = 0.3f;

    int attackNum;
    string attackString;
    float chainTimer;

    // Use this for initialization
    void Awake()
    {
		// Level Management
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

		GameObject mm = GameObject.Find("MetricManager");
		GameObject sm = GameObject.Find("SoundManager");
		if (!mm)
		{
			mm = levelManager.SpawnMetric();
		}
		if (!sm)
		{
			sm = levelManager.SpawnSound();
		}
		metricManager = mm.GetComponent<MetricManager>();
		soundManager = sm.GetComponent<SoundManager>();
		scoreManager = GameObject.Find("UI").GetComponent<ScoreManager>();

		// Player Setup
		playerController = GameObject.Find("Spy").GetComponent<PlayerController>();

        hitboxManager = this.gameObject.GetComponent<HitboxManager>();
		pAnimationController = GetComponentInChildren<PlayerAnimationController>();
		attackFSM = StateMachine<AttackStates>.Initialize(this, AttackStates.Start);
        comboText = GameObject.FindWithTag("ButtonSequence").GetComponent<TextMesh>();

        launch = false;
        uppercut = false;

        attackNum = 0;
        attackString = System.String.Empty;
        chainTimer = timeToChain;

        // Debug.Log("PLATFORM: " + InputManager.ActiveDevice.Meta);
    }

	void Start()
	{
		
	}

    // Update is called once per frame
    void Update()
    {
		//if (InputManager.ActiveDevice.Action2.WasPressed)
		//{
		//	if (scoreManager.SpecialIsCharged())
		//	{
		//		attackFSM.ChangeState(AttackStates.Two);
		//	}
		//}

		if (InputManager.ActiveDevice.Action4.WasPressed)
		{
			if (scoreManager.UltIsCharged())
			{
				attackFSM.ChangeState(AttackStates.Four);
			}
		}

        if (chainTimer > 0f)
        {
            chainTimer -= Time.deltaTime;
        }
        else
        {
            attackFSM.ChangeState(AttackStates.Start);
        }

        comboText.text = attackString;
    }

    void Start_Enter()
    {
		attackNum = 0;
        attackString = System.String.Empty;
        hitboxManager.DisableAllHitboxes();
		pAnimationController.ResetAnimation();
        //everytime the character resets to neutral, it gets movement back
        if (playerController.GetIntroStatus() == false)
        {
            playerController.EnableMovement();
        }
    }

    void Start_Update()
    {
		// 1 - Kick
		if (InputManager.ActiveDevice.Action1.WasPressed)
		{
			attackFSM.ChangeState(AttackStates.One);
		}
        // 3 - Punch
        if (InputManager.ActiveDevice.Action3.WasPressed)
        {
            attackFSM.ChangeState(AttackStates.Three);
        }
    }

	// Punch Button
    void One_Enter()
    {
		if (attackNum == 0)
		{
			attackString = System.String.Empty;
		}
        chainTimer = timeToChain;
		if (attackString.Equals("A")) { StartCoroutine(StartUpLight(HitboxTypes.LeftFoot)); }
		else { StartCoroutine(StartUpLight(HitboxTypes.RightFoot)); }
        pAnimationController.AnimateKick();
        attackNum++;
        attackString += 'A';

        soundManager.PlayLowWhoosh();

        playerController.DisableMovement();

        // debug stuff
        Debug.Log("<b>Attack no:</b> " + attackNum + ", <i>Attack string: </i>" + attackString);

        if (attackNum == 3)
        {
			// TODO: lunging punch here
			metricManager.AddCombo(attackString);
			attackNum = 0;
        }
    }

    void One_Update()
    {
		if (InputManager.ActiveDevice.Action1.WasPressed)
		{
			One_Enter();
		}

		if (InputManager.ActiveDevice.Action3.WasPressed)
		{
			if (attackString.Equals("AA"))
			{
				attackFSM.ChangeState(AttackStates.Uppercut);
			}
			else
			{
				attackFSM.ChangeState(AttackStates.Three);
			}
		}
    }

	void Two_Enter()
	{
		//metricManager.AddSpecial(2);
		//SpecialAttack();
		//StartCoroutine(FinishCombo(0.3f));
	}

	// Punch button
    void Three_Enter()
    {
		if (attackNum == 0)
		{
			attackString = System.String.Empty;
		}
		chainTimer = timeToChain;
		if (attackString.Equals("X")) { StartCoroutine(StartUpLight(HitboxTypes.LeftHand)); }
		else { StartCoroutine(StartUpLight(HitboxTypes.RightHand)); }
		pAnimationController.AnimatePunch();
        attackNum++;
        attackString += 'X';

        soundManager.PlayLowWhoosh();

        playerController.DisableMovement();

        // debug stuff
        Debug.Log("<b>Attack no:</b> " + attackNum + ", <color=red>B</color> pressed");

        if (attackNum == 3)
        {
			metricManager.AddCombo(attackString);
			attackNum = 0;
        }
    }

    void Three_Update()
    {
		if (InputManager.ActiveDevice.Action1.WasPressed)
		{
			if (attackString.Equals("XX"))
			{
				attackFSM.ChangeState(AttackStates.DashKick);
			}
			else
			{
				attackFSM.ChangeState(AttackStates.One);
			}
		}

		if (InputManager.ActiveDevice.Action3.WasPressed)
		{
			Three_Enter();
		}
    }

	void Four_Enter()
	{
		metricManager.AddSpecial(4);
		StartCoroutine(UltimateAttack());
		StartCoroutine(FinishCombo(0.3f));
	}

	void Uppercut_Enter()
	{
		if (successPopup)
		{
			successPopup.SpawnPrefab(transform.position, "UPPERCUT");
		}
		chainTimer = timeToChain;
        StartCoroutine(StartUpHeavy(HitboxTypes.RightHand));
		pAnimationController.AnimateUppercut();
		attackNum++;

        playerController.DisableMovement();

        soundManager.PlayHighWhoosh();

		uppercut = true;
        
        // debug stuff
        Debug.Log("<b>Attack no:</b> " + attackNum + ", <i>Attack string: </i>" + attackString);

		attackString += 'X';
		metricManager.AddCombo("AAX");
		StartCoroutine(FinishCombo(0.3f));
	}

    void DoublePunch_Enter()
    {
        chainTimer = timeToChain;
        StartCoroutine(StartUpHeavy(HitboxTypes.DoubleHand));
        pAnimationController.AnimateDoublePunch();
        attackNum++;

        soundManager.PlayHighWhoosh();

        playerController.DisableMovement();

        //launch = true;

        // debug stuff
        Debug.Log("<b>Attack no:</b> " + attackNum + ", <i>Attack string: </i>" + attackString);

        StartCoroutine(FinishCombo(0.3f));
    }

	void DashKick_Enter()
	{
		if (successPopup)
		{
			successPopup.SpawnPrefab(transform.position, "DASH");
		}
		
		chainTimer = timeToChain;
        StartCoroutine(StartUpHeavy(HitboxTypes.RightFoot));
		pAnimationController.AnimateLargeKick();
		attackNum++;

		soundManager.PlayHighWhoosh();

        playerController.DisableMovement();
        StartCoroutine(DashKick());

        launch = true;

		// debug stuff
		Debug.Log("<b>Attack no:</b> " + attackNum + ", <i>Attack string: </i>" + attackString);

		attackString += 'A';
		metricManager.AddCombo("XXA");
		StartCoroutine(FinishCombo(0.3f));
	}

	void Finish_Enter()
    {
		
    }

    IEnumerator FinishCombo(float waitTime)
    {
        attackFSM.ChangeState(AttackStates.Finish, StateTransition.Overwrite);  // Prevent extra attacks

        yield return new WaitForSeconds(waitTime);

        launch = false;
        uppercut = false;
        pAnimationController.ResetAnimation();
        attackFSM.ChangeState(AttackStates.Start);  // Disable hitboxes and reset combo
    }

	void FinishQuick()
	{
		attackNum = 0;
		attackString = System.String.Empty;
	}

    public bool GetLaunch()
    {
        return launch;
    }

    public void ResetLaunch()
    {
        launch = false;
    }

    public bool GetUpperCut()
    {
        return uppercut;
    }

    public void ResetUppercut()
    {
        uppercut = false;
    }

    void SpecialAttack()
	{
		//pAnimationController.AnimateDoublePunch();
		//scoreManager.ResetSpecial();

		//float explosionRad = 6.0f;
		//float explosionPow = 25.0f;

		//Vector3 explosionPos = transform.position;
		//Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRad);
		//foreach (Collider hit in colliders)
		//{
		//	Rigidbody rb = hit.GetComponent<Rigidbody>();

		//	if (rb)
		//	{
		//		CollisionBehavior cb = hit.gameObject.GetComponent<CollisionBehavior>();
		//		if (cb) { cb.enablePhysics(); }
		//		rb.AddExplosionForce(explosionPow, explosionPos, explosionRad, 0f, ForceMode.Impulse);
  //              soundManager.PlayEarthQuake();
  //              StartCoroutine(HitboxBehaviour.DisablePhysics(hit));
		//	}
		//}
	}

    IEnumerator UltimateAttack()
    {
		soundManager.PlayEarthQuake();
		Camera.main.GetComponent<ScreenShake>().ShakeCamera(.5f, 1f);
		yield return new WaitForSeconds(1f);
		pAnimationController.AnimateDoublePunch();
        soundManager.PlayExplosion();
        scoreManager.ResetUlt();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            scoreManager.AddScore(200);
            scoreManager.IncrementComboCount();
            e.GetComponent<CollisionBehavior>().Die();
        }
		Camera.main.GetComponent<ScreenShake>().ShakeCamera(.4f, .5f);
	}

    public void UltimateEndGame()
    {
        pAnimationController.AnimateDoublePunch();
        //soundManager.PlayExplosion();
        scoreManager.ResetUlt();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            e.GetComponent<CollisionBehavior>().DieEndGame();
        }
    }

    //adding startup frames to the hits
    IEnumerator StartUpLight(HitboxTypes hitbox)
    {
        yield return new WaitForSeconds(0.06f);

        hitboxManager.EnableHitbox(hitbox);

    }

    IEnumerator StartUpHeavy(HitboxTypes hitbox)
    {
        yield return new WaitForSeconds(0.09f);

        hitboxManager.EnableHitbox(hitbox);
    }

    IEnumerator DashKick()
    {
        playerController.EnableDashKick();
        yield return new WaitForSeconds(0.4f);
        playerController.DisableDashKick();
        launch = false;
    }
}
