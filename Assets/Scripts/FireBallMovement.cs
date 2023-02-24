using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField] float FBSpeed = 3f;
    bool isMoving = true;
    public bool tookDamage;
    Vector2 initialPosition;
    Vector2 minBounds;
    Vector2 maxBounds;

    void Start()
    {
        initBounds();
        StartCoroutine("RespawnFireball");

    }

    void initBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2 (1,0.5f));
    }
    
    void Update()
    {
        if (isMoving)
        {
            transform.localPosition = new Vector2(transform.localPosition.x - FBSpeed * Time.deltaTime, initialPosition.y);
            if (transform.localPosition.x < minBounds.x - 4 || tookDamage)
            {
                StartCoroutine("RespawnFireball");
            }
        }  
    }

    IEnumerator RespawnFireball()
    {
        isMoving = false;
        tookDamage = false;
        initialPosition = new Vector2 (maxBounds.x + 2, Random.Range(minBounds.y, maxBounds.y));
        transform.localPosition = initialPosition; 
        yield return new WaitForSeconds(Random.Range(5,10));
        isMoving = true;
    }
}
