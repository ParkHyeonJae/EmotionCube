using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0f;

    public float tempDuration = 0f;

    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void OnEnable()
    {
        tempDuration = shakeDuration;
		Time.timeScale = 0.2f;
    }


    void Update()
    {
        if (tempDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            tempDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            tempDuration = 0f;
            transform.localPosition = originalPos;
			Time.timeScale = 1f;
            this.enabled = false;
        }
    }
}
