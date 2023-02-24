using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f,1f)] float shootingVolume = 1f;
    [Header("Player")]
    [SerializeField] AudioClip playerSwordClip;
    [SerializeField][Range(0f,1f)] float playerSwordVolume = 1f;
    [SerializeField]AudioClip playerDamageClip;
    [SerializeField][Range(0f,1f)] float playerDamageVolume = 1f;
    [Header("Damage")]
    [SerializeField] AudioClip enemyDamageClip;
    [SerializeField][Range(0f,1f)] float enemyDamageVolume = 1f;
    [Header("Death")]
    [SerializeField] AudioClip enemyDeathClip;
    [SerializeField] [Range(0f,1f)] float enemyDeathVolume = 1f;
     [SerializeField] AudioClip bigEnemyDeathClip;
    [SerializeField][Range(0f,1f)] float bigEnemyDeathVolume = 1f;

    static AudioPlayer instance;

    void Awake() 
    {
        ManageSingleton();    
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip, 
                                        Camera.main.transform.position, 
                                        shootingVolume);
        }
    }

    public void PlaySwordClip()
    {
        if (playerSwordClip != null)
        {
            AudioSource.PlayClipAtPoint(playerSwordClip,
                                        Camera.main.transform.position,
                                        playerSwordVolume);
        }
    }
    public void PlayPlayerDamageClip()
    {
        if (playerDamageClip != null)
        {
            AudioSource.PlayClipAtPoint(playerDamageClip,
                                        Camera.main.transform.position,
                                        playerDamageVolume);
        }
    }

    public void PlaySmallEnemyDamageClip()
    {
        if (enemyDamageClip != null)
        {
            AudioSource.PlayClipAtPoint(enemyDamageClip,
                                        Camera.main.transform.position,
                                        enemyDamageVolume);
        }
    }

    public void PlaySmallEnemyDeathClip()
    {
        AudioSource.PlayClipAtPoint(enemyDeathClip,
                                    Camera.main.transform.position,
                                    enemyDeathVolume);
    }

    public void PlayBigEnemyDeathClip()
    {
        if (bigEnemyDeathClip != null)
        {
            AudioSource.PlayClipAtPoint(bigEnemyDeathClip,
                                        Camera.main.transform.position,
                                        bigEnemyDeathVolume);
        }
    }
}
