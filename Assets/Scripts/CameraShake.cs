using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float ShakeDuration = 1f;
    [SerializeField] float ShakeMagnitude = 0.5f;
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine("ScreenShake");
    }

    IEnumerator ScreenShake()
    {
        float timePassed = 0;
        while (timePassed < ShakeDuration)
        {
            transform.position = initialPosition + (Vector3) Random.insideUnitCircle * ShakeMagnitude;
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
