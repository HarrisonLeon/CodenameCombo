using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{

    public float shakeTimer;
    public float shakeAmount;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (shakeTimer > 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(originalPosition.x + shakePos.x, originalPosition.y + shakePos.y, originalPosition.z);

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
            transform.position = originalPosition;
        }

    }

    public void ShakeCamera(float shakeStrength, float shakeDuration)
    {
        shakeAmount = shakeStrength;
        shakeTimer = shakeDuration;
    }

}