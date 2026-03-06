using UnityEngine;

public class fanAndGen : BlowBehaviour
{
    [Header("What rotates")]
    [SerializeField] Transform fanPart;         // the fan blades / hub
    [SerializeField] Transform generatorPart;   // the cylinder part

    [Header("Fan settings")]
    [SerializeField] float fanAmplify = 120f;   // how much blowing adds
    [SerializeField] float fanFalloff = 0.9f;   // how fast it slows down

    [Header("Generator settings")]
    [SerializeField] float generatorMultiplier = 2f; // generator spins based on fan speed

    float fanSpinSpeed = 0f;

    protected override void OnBlown(Vector3 blowDirection)
    {
        // ignore tiny blows
        if (blowDirection.sqrMagnitude < 0.01f)
            return;

        // optional: only react while actual breath button is pressed
        if (BreathInput.Value <= 0.1f)
            return;

        // add spin based on blow strength
        fanSpinSpeed += blowDirection.magnitude * fanAmplify;
    }

    void Update()
    {
        if (fanPart != null)
        {
            // spin fan around local Y
            fanPart.Rotate(0f, fanSpinSpeed * Time.deltaTime, 0f, Space.Self);
        }

        if (generatorPart != null)
        {
            // spin generator around local X
            generatorPart.Rotate(fanSpinSpeed * generatorMultiplier * Time.deltaTime, 0f, 0f, Space.Self);
        }

        // slow down over time
        fanSpinSpeed *= Mathf.Pow(fanFalloff, Time.deltaTime);
    }
}