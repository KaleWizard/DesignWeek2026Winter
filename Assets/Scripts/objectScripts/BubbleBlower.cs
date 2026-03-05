using UnityEngine;
using System.Collections;

public class BubbleBlower : BlowBehaviour
{
    [SerializeField] ParticleSystem bubbles;
    [SerializeField] float bubbleDuration = 1f;

    // Your soft blows are ~0.02 to ~0.053 from the logs
    [SerializeField] float minSoft = 0.015f;
    [SerializeField] float maxSoft = 0.12f;

    bool isPlaying;

    void Start()
    {
        if (bubbles != null)
            bubbles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    protected override void OnBlown(Vector3 blowDirection)
    {

        // If your system sends zero-vector when idle (your log shows mag=0)
        float strength = blowDirection.magnitude;
        if (strength == 0f) return;

        // soft-only window
        if (strength < minSoft) return;
        if (strength > maxSoft) return;

        Debug.Log(strength);

        if (isPlaying) return;
        StartCoroutine(PlayBubbles());
    }

    IEnumerator PlayBubbles()
    {
        isPlaying = true;

        if (bubbles != null)
            bubbles.Play();

        yield return new WaitForSeconds(bubbleDuration);

        if (bubbles != null)
            bubbles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        isPlaying = false;
    }
}