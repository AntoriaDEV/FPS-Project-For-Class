using System.Collections;
using UnityEngine;

public class CameraShakeOnGunFire : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform cameraTransform;

    [Header("Shake")]
    [SerializeField] float duration = 0.08f;
    [SerializeField] float amplitude = 0.12f;
    [SerializeField] float frequency = 35f;
    [SerializeField] AnimationCurve strengthOverTime = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

    Vector3 baseLocalPosition;
    Coroutine shakeRoutine;
    float seed;

    void Awake()
    {
        if (cameraTransform == null)
        {
            var player = FindObjectOfType<FPSController>();
            if (player != null && player.Cam != null)
            {
                cameraTransform = player.Cam.transform;
            }
            else
            {
                cameraTransform = transform;
            }
        }
    }

    void OnEnable()
    {
        if (cameraTransform != null)
        {
            baseLocalPosition = cameraTransform.localPosition;
        }

        Gun.OnGunFired += HandleGunFired;
    }

    void OnDisable()
    {
        Gun.OnGunFired -= HandleGunFired;

        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            shakeRoutine = null;
        }

        if (cameraTransform != null)
        {
            cameraTransform.localPosition = baseLocalPosition;
        }
    }

    void HandleGunFired()
    {
        if (!isActiveAndEnabled || cameraTransform == null)
            return;

        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }

        shakeRoutine = StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        baseLocalPosition = cameraTransform.localPosition;
        seed = Random.value * 1000f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = duration <= 0f ? 1f : (elapsed / duration);
            float strength = amplitude * strengthOverTime.Evaluate(t);

            float time = Time.time * frequency;
            float nX = (Mathf.PerlinNoise(seed, time) - 0.5f) * 2f;
            float nY = (Mathf.PerlinNoise(seed + 1f, time) - 0.5f) * 2f;

            cameraTransform.localPosition = baseLocalPosition + new Vector3(nX, nY, 0f) * strength;

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = baseLocalPosition;
        shakeRoutine = null;
    }
}
