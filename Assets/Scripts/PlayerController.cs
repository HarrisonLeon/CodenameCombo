using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour
{
	//PlayerActions playerActions;
	public float MaxSpeed;
    float speed;

	LevelManager levelManager;

    bool movementEnable = false;
    
    bool facingRight = true;

    bool introFall = true;

    bool outroFall = false;

    bool kickMove = false;

    Vector3 startPos;
    public GameObject player;

    float leftBorder;
    float rightBorder;
    float topBorder;
    float bottomBorder;

    SpawnManager spawnManager;

    // Use this for initialization
    void Start()
    {
		speed = MaxSpeed;
		float dist = (transform.position - Camera.main.transform.position).z;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + GetComponent<Renderer>().bounds.size.x / 2;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - GetComponent<Renderer>().bounds.size.x / 2;
        topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + GetComponent<Renderer>().bounds.size.y / 2;
        bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - GetComponent<Renderer>().bounds.size.y / 2;

        StartCoroutine(Intro());
        startPos = new Vector3(0, 1, 0);

		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
		GameObject sm = GameObject.FindGameObjectWithTag("Spawner");
		if (!sm)
		{
			sm = levelManager.SpawnSpawner();
		}
		spawnManager = sm.GetComponent<SpawnManager>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {

		float h = InputManager.ActiveDevice.LeftStickX.RawValue;
		float x = InputManager.ActiveDevice.LeftStickX.RawValue * Time.deltaTime * speed;
		float y = InputManager.ActiveDevice.LeftStickY.RawValue * Time.deltaTime * speed;

        //regular movement
        if (movementEnable)
        {
            transform.Translate(x, y, 0f, Space.World);
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);
        }

        if (!Mathf.Approximately(h, 0.0f) && movementEnable)
        {
            // changes facing direction to left/right if any horitzontal input happens
            transform.forward = Vector3.Normalize(new Vector3(0f, 0f, h));
            if (transform.forward.z < 0)
            {
                FaceLeft();
            }
            else
            {
                FaceRight();
            }
        }

        if (kickMove)
        {
            if (facingRight)
            {
                player.transform.position = new Vector3(player.transform.position.x + Time.deltaTime * speed, player.transform.position.y, player.transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(player.transform.position.x - Time.deltaTime * speed, player.transform.position.y, player.transform.position.z);
            }
            
        }

        //intro fall
        if (player.transform.position.y > startPos.y && introFall == true)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - Time.deltaTime * speed / 4, player.transform.position.z);
        }
        else
        {
            introFall = false;
        }

        //called when level ends
        if (player.transform.position.y > startPos.y - 13 && outroFall == true)
        {
            movementEnable = false;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - Time.deltaTime * speed / 4, player.transform.position.z);
            spawnManager.DisableSpawn();
        }

    }

	public void FaceLeft()
	{
        facingRight = false;
		player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, -90f, player.transform.eulerAngles.z);
	}

	public void FaceRight()
	{
        facingRight = true;
		player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90f, player.transform.eulerAngles.z);
	}

    IEnumerator Intro()
	{
        Debug.Log("FROZEN");
        
        yield return new WaitForSeconds(3f);
        movementEnable = true;
        introFall = true;
        Debug.Log("Now you can move");
    }

     public bool getFacingRight()
    {
        return facingRight;
    }

    public void disablePlayer()
    {
        outroFall = true;
    }

    public bool GetMovementStatus()
    {
        return movementEnable;
    }

    public bool GetIntroStatus()
    {
        return introFall;
    }

	void OnDestroy()
	{
		//playerActions.Destroy();
	}

    public void EnableMovement()
    {
		speed = MaxSpeed;
    }

    public void DisableMovement()
    {
		speed = MaxSpeed * 0.6f;
    }

    public void EnableDashKick()
    {
        kickMove = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void DisableDashKick()
    {
        kickMove = false;
        GetComponent<BoxCollider>().enabled = true;
    }

}
