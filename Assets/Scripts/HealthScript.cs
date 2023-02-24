using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem damageEffect;
    [SerializeField] int scoreForKillingThisEnemy = 10;
    Animator myAnimator;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    Shooter shooter;
    UIDisplay uiDisplay;
    LevelManager levelManager;

    private void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();   
        scoreKeeper = FindObjectOfType<ScoreKeeper>();  
        uiDisplay = FindObjectOfType<UIDisplay>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        shooter = GetComponent<Shooter>();
    }

    public int GetHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (shooter.CheckForPositionBounds(gameObject))
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                TakeDamage(damageDealer.GetDamage());
                PlayDamageSoundClip();
                PlayHitEffect();
                damageDealer.Hit();
            }   
        }
         
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (gameObject.transform.tag == "Player")
        {
            uiDisplay.UpdateHealthSlider(health);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void PlayHitEffect()
    {
        if (damageEffect != null)
        {
            ParticleSystem instance = Instantiate(damageEffect, transform.position, Quaternion.identity);
            Destroy(instance, instance.main.duration);
        }
        if (gameObject.transform.tag == "Player")
        {
            FindObjectOfType<CameraShake>().Play();
        }
    }

    
    IEnumerator PlayEnemyDeathAnimation()
    {
        myAnimator.SetTrigger("Dead");
        float duration = myAnimator.GetCurrentAnimatorClipInfo(0).Length;
        gameObject.GetComponent<PathFinder>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Shooter>().isFiring = false;
        yield return  new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    

    void PlayerDeath()
    {
        myAnimator.SetTrigger("Dead");
        Player player = FindObjectOfType<Player>();
        player.isDead = true;
        player.GetComponent<Collider2D>().enabled = false;
        levelManager.LoadGameOverScreen();
    }

    void PlayDamageSoundClip()
    {
        string tag = gameObject.transform.tag;
        switch(tag)
        {
            case "SmallEnemy":
            case "BigEnemy":
            audioPlayer.PlaySmallEnemyDamageClip();
            break;
            case "Player":
            audioPlayer.PlayPlayerDamageClip();
            break;
        }
    }

    void PlayDeathSoundClip()
    {
        string tag = gameObject.transform.tag;
        switch(tag)
        {
            case "SmallEnemy":
            audioPlayer.PlaySmallEnemyDeathClip();
            break;
            case "BigEnemy":
            audioPlayer.PlayBigEnemyDeathClip();
            break;
        }
    }

    void Die()
    {
        if (gameObject.transform.tag == "Player")
        {
            PlayerDeath();
            return;
        }
        else
        {
            scoreKeeper.UpdateScore(scoreForKillingThisEnemy);
            StartCoroutine("PlayEnemyDeathAnimation");
            PlayDeathSoundClip();
        }
    }
}