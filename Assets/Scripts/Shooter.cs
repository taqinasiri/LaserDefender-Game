using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private float projectileLifeTime = 5;
    [SerializeField] private float baseFirningRate = 0.2f;

    [Header("AI")]
    [SerializeField] private bool useAI = false;

    [SerializeField] private float firningRateVariance = 0;
    [SerializeField] private float minFirningRate = 1;

    [HideInInspector]
    public bool isFiring;

    private Coroutine firingCoroutine;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if(isFiring && firingCoroutine is null)
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        else if(!isFiring && firingCoroutine is not null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuosly()
    {
        while(true)
        {
            GameObject instance = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb is not null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance,projectileLifeTime);

            float timeToNextProjectile = Random.Range(baseFirningRate - firningRateVariance
            ,baseFirningRate + firningRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile,minFirningRate,float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}