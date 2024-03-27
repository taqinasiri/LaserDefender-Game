using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private int health = 5;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private int score = 1;
    [SerializeField] private bool applyCameraShake = false;

    private CameraShake CameraShake;
    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;
    private LevelManager levelManager;

    private void Awake()
    {
        CameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if(damageDealer is not null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.AddScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if(hitEffect is not null)
        {
            ParticleSystem instance = Instantiate(
                hitEffect,
                transform.position,
                Quaternion.identity);
            Destroy(instance.gameObject,instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    private void ShakeCamera()
    {
        if(CameraShake is not null && applyCameraShake)
        {
            CameraShake.Play();
        }
    }

    public int GetHealth() => health;
}