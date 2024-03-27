using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;

    [SerializeField][Range(0,1)] private float shootingVolume = 1;

    [Header("Damage")]
    [SerializeField] private AudioClip damageClip;

    [SerializeField][Range(0,1)] private float damageVolume = 1;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if(instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip() => PlayClip(shootingClip,shootingVolume);

    public void PlayDamageClip() => PlayClip(damageClip,damageVolume);

    private void PlayClip(AudioClip clip,float volume)
    {
        if(clip is not null)
        {
            AudioSource.PlayClipAtPoint(
                clip,
                Camera.main.transform.position,
                volume);
        }
    }
}