using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float sakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0;
        while(elapsedTime < sakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}