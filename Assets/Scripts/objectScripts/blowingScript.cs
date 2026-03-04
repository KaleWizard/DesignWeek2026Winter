using UnityEngine;

public class blowingScript : BlowBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioSource source;
    [SerializeField] float minPitch = 0.85f;
    [SerializeField] float maxPitch = 1.25f;

    [Header("Blow detection")]
    [SerializeField] float blowStartThreshold = 0.05f; // how strong blow must be to count
    [SerializeField] float blowStopDelay = 0.12f;      // keeps sound from stuttering

    float lastBlowTime;

    void Awake()
    {
        if (source == null) source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = true;
    }

    protected override void OnBlown(Vector3 blowDirection)
    {
        float strength = blowDirection.magnitude;

        // ignore tiny noise / idle values
        if (strength < blowStartThreshold)
            return;

        lastBlowTime = Time.time;

        // start sound if not already playing
        if (!source.isPlaying)
            source.Play();

        // stretch goal: map strength to pitch
        // if your blow system sends huge values, this clamp keeps it sane
        float t = Mathf.Clamp01(strength);
        source.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }

    void Update()
    {
        // if we haven't been blown on recently, stop the sound
        if (source.isPlaying && Time.time > lastBlowTime + blowStopDelay)
        {
            source.Stop();
        }
    }
}