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
        // Get Camera Position
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine("ScreenShake");
    }

    IEnumerator ScreenShake()
    {
        float timePassed = 0;

        // Shake the camera for as long as ShakeDuration
        while (timePassed < ShakeDuration)
        {
            // Change x and y position using ShakeMagnitude, update timePassed and wait for the end of the frame
            transform.position = initialPosition + (Vector3) Random.insideUnitCircle * ShakeMagnitude;
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Return Camera position to its original position
        transform.position = initialPosition;
    }
}
