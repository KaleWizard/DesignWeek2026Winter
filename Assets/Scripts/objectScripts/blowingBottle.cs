using UnityEngine;

public class BlowInstrument : BlowBehaviour
{
    [SerializeField] AudioSource source;

    protected override void OnBlown(Vector3 blowDirection)
    {
        // ignore tiny blows
        if (blowDirection.sqrMagnitude < 0.01f)
            return;

        // if already playing, don't restart
        if (source.isPlaying)
            return;

        // play the whole sound once
        source.Play();
    }
}