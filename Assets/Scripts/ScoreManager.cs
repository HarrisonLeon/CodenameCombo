using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public GameObject ultInstructions;
	public GameObject ultMeter;
	public GameObject specialMeter;

	public ParticleSystem[] elementSequence;

    private int scoreVal;
    private string scoreString;

    private int multiplierVal;
    private string multiplierString;

    private int comboCounterVal = 0;
    private int comboCounterMax = 0;
    private string comboCountString;

	private float specialPercent = 0;
    private int ultPercent = 0;

    private GameObject scoreText;
    private GameObject multiplierText;
    private GameObject comboCountText;

    private FlashingText ft;

	private float inc;

	private bool UltNotification = false;

    // Use this for initialization
    void Start()
    {
		scoreVal = 0;
        multiplierVal = 1;
        scoreText = GameObject.Find("Score");
        multiplierText = GameObject.Find("Multiplier");
        comboCountText = GameObject.Find("ComboCounter");
        ft = GetComponent<FlashingText>();

		// Turn off fire hands
		elementSequence[0].Stop();
		elementSequence[1].Stop();
		elementSequence[2].Stop();
		elementSequence[3].Stop();
		elementSequence[4].Stop();
		elementSequence[5].Stop();
		elementSequence[6].Stop();
		elementSequence[7].Stop();

		UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
		// FIXME: could probably use some cleanup
		if (specialPercent < 100)
		{
			specialPercent += (1.0f / 5.0f * Time.deltaTime);
			specialMeter.GetComponent<Image>().fillAmount = specialPercent;
		}
	}

    void UpdateScore()
    {
        scoreString = "Score: " + scoreVal.ToString("D6");
        multiplierString = multiplierVal.ToString() + "X";
        comboCountString = "Streak: " + comboCounterVal.ToString();
        scoreText.GetComponent<Text>().text = scoreString;
        multiplierText.GetComponent<Text>().text = multiplierString;
        comboCountText.GetComponent<Text>().text = comboCountString;

		float sizeMod = multiplierVal * 0.0025f;

		multiplierText.GetComponent<RectTransform>().localScale = new Vector3(0.5f + sizeMod, 0.5f + sizeMod, 0.5f + sizeMod);

		// FIXME: consider having a GameObject variable for this for efficiency
        ultMeter.GetComponent<ProgressBar>().Value = ultPercent;
	}

    public void IncreaseMultiplier(int value)
    {
        ft.BlinkSequence((multiplierVal > 200) ? new Color(0f, 1f, 0f, 0.40f) : Color.green, (multiplierVal > 200) ? new Color(0f, 0f, 0f, 0.40f) : Color.black);
        if (multiplierVal <= 1000)
        {
            multiplierVal += value;

            UpdateScore();
        }

		if (multiplierVal >= 10 && multiplierVal < 25)
		{
			elementSequence[0].Play();
			elementSequence[4].Play();
		}
		if (multiplierVal >= 25 && multiplierVal < 50)
		{
			elementSequence[0].Stop();
			elementSequence[4].Stop();
			elementSequence[1].Play();
			elementSequence[5].Play();
		}
		if (multiplierVal >= 50 && multiplierVal < 100)
		{
			elementSequence[1].Stop();
			elementSequence[5].Stop();
			elementSequence[2].Play();
			elementSequence[6].Play();
		}
		if (multiplierVal >= 100)
		{
			elementSequence[2].Stop();
			elementSequence[6].Stop();
			elementSequence[3].Play();
			elementSequence[7].Play();
		}
	}

    public void DecreaseMultiplier()
    {
		ft.BlinkSequence((multiplierVal > 200) ? new Color(1f, 0f, 0f, 0.40f) : Color.red, (multiplierVal > 200) ? new Color(0f, 0f, 0f, 0.40f) : Color.black);
		if (multiplierVal != 1)
        {
            multiplierVal /= 2;

            UpdateScore();
        }

		if (multiplierVal < 10)
		{
			elementSequence[0].Stop();
			elementSequence[4].Stop();
		}
		if (multiplierVal < 25)
		{
			elementSequence[1].Stop();
			elementSequence[5].Stop();
		}
		if (multiplierVal < 50)
		{
			elementSequence[2].Stop();
			elementSequence[6].Stop();
		}
		if (multiplierVal < 100)
		{
			elementSequence[3].Stop();
			elementSequence[7].Stop();
		}
	}

    public void AddScore(int score)
    {
        scoreVal += score * multiplierVal;
        UpdateScore();
    }

    public void IncrementComboCount()
    {
        comboCounterVal++;
        if (comboCounterVal > comboCounterMax)
        {
            comboCounterMax = comboCounterVal;
        }
        UpdateScore();
    }

    public void ResetComboCount()
    {
        comboCounterVal = 0;
        UpdateScore();
    }

    public void ChargeUlt(int amount)
    {
        if (ultPercent < 100)
        {
            ultPercent += amount;
            UpdateScore();
        }
        else if (!UltNotification)
        {
			StartCoroutine(ShowUlt());
        }
    }

	public void ResetSpecial()
	{
		specialPercent = 0f;
	}

    public void ResetUlt()
    {
        ultPercent = 0;
        UpdateScore();
        ultInstructions.SetActive(false);
		UltNotification = false;
    }

	public bool SpecialIsCharged()
	{
		return (specialPercent >= 1.0f);
	}

    public bool UltIsCharged()
    {
        return (ultPercent > 99);
    }
    

    public int GetScore()
    {
        return scoreVal;
    }

    public int GetComboCounter()
    {
        return comboCounterMax;
    }

	IEnumerator ShowUlt()
	{
		UltNotification = true;
		ultInstructions.SetActive(true);
		yield return new WaitForSeconds(3f);
		ultInstructions.SetActive(false);
	}
}
