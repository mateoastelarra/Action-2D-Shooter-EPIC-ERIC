using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject proyectile;
    [SerializeField] float proyectileSpeed = 10f;
    [SerializeField] float proyectileLifetime = 5f;
    [Header("Firing")]
    [SerializeField] float baseFiringRate = 2f;
    [SerializeField] bool useIA;
    [SerializeField] float firingRateVariance = 0;
    [SerializeField] float minimumFiringRate = 0.2f;
    [Header("Firing Animation")]
    [SerializeField] bool hasFiringAnimation;
    [SerializeField] string triggerForFiringName;
    [HideInInspector] public bool isFiring;
    [HideInInspector] public bool hasFired;
    Coroutine FiringCoroutine;
    Animator myAnimator;
    AudioPlayer audioPlayer;
    Vector2 minBounds;
    Vector2 maxBounds;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    void Start()
    {
        if (useIA)
        {
            isFiring = true;
        }
        if (hasFiringAnimation)
        {
            myAnimator = gameObject.GetComponent<Animator>();
        }
        initBounds();
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && CheckForPositionBounds(gameObject))
        {
            if (!hasFired)
            {
                FiringCoroutine = StartCoroutine(FireContinue());
            }    
        }
        else
        {
            if (FiringCoroutine != null)
            {
                StopCoroutine(FiringCoroutine);
                hasFired = false;
            }    
        }
        
    }

    IEnumerator FireContinue()
    {
        while(true)
        {
            GameObject proyectileInstance = Instantiate(proyectile, 
                                                    transform.position,
                                                    Quaternion.identity);
            Rigidbody2D rb = proyectileInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (useIA)
                {
                    rb.velocity = - transform.up * proyectileSpeed;
                }
                else
                {
                    rb.velocity = transform.up * proyectileSpeed;
                    proyectileInstance.transform.localRotation = Quaternion.Euler(0, 0, 180);
                    audioPlayer.PlaySwordClip();    
                }    
            }
            if (hasFiringAnimation)
            {
                myAnimator.SetTrigger(triggerForFiringName);
            }
            audioPlayer.PlayShootingClip();
            Destroy(proyectileInstance, proyectileLifetime);
            hasFired = true;
            yield return new WaitForSeconds(GetRandomFiringRate());
            hasFired = false;
        }   
    }

    float GetRandomFiringRate()
    {
        float randomFiringRate = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
        return Mathf.Clamp(randomFiringRate, minimumFiringRate, float.MaxValue);

    }

    void initBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2 (1,1));
    }

    public bool CheckForPositionBounds(GameObject gameObject)
    {
        if (transform.position != null)
        {
            if (transform.position.x > maxBounds.x ||
                transform.position.x < minBounds.x ||
                transform.position.y > maxBounds.y ||
                transform.position.y < minBounds.y)
            {
                return false;
            }
        }
        return true;
    }
}
