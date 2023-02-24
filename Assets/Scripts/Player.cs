using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingBottom;
    [SerializeField] float paddingTop;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Animator myAnimator;
    bool isMoving;
    Shooter shooter;
    public bool isDead;

    void Awake() 
    {
        shooter = GetComponent<Shooter>();
    }
    void Start() 
    {
        initBounds();
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!isDead)
        {
            Move();
        }   
    }

    void initBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2 (1,1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingRight, maxBounds.x - paddingLeft);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
        isMoving = (rawInput.x != 0) || (rawInput.y != 0);
        myAnimator.SetBool("IsWalking", isMoving);

    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null && !isDead)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}

