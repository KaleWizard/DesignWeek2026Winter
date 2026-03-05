using UnityEngine;

// This script makes the merry-go-round spin when it is blown on.
// It inherits from BlowBehaviour so it can receive the OnBlown() event.

public class MerryGoRoundSpin : BlowBehaviour
{

    [SerializeField] float falloff = 0.9f;

    // How powerful the spin added from a blow is.
    // Bigger number = stronger spins when the player blows.
    [SerializeField] float spinPower = 250f;

    // How quickly the merry-go-round slows down.
    // Bigger number = stops faster
    // Smaller number = spins longer
    [SerializeField] float slowdown = 50f;

    // Current spinning speed of the merry-go-round.
    // This value increases when blown and decreases over time.
    public float spinSpeed = 0f;


    // This function is automatically called by the blow system
    // whenever the player's blow hits this object.
    protected override void OnBlown(Vector3 blowDirection)
    {
        // Sometimes the blow system sends a zero vector when idle.
        // This ignores those so it doesn't add fake spin.
        if (blowDirection.sqrMagnitude < 0.001f)
            return;

        // Get how strong the blow was
        float strength = blowDirection.magnitude;

        // Add spin based on the blow strength and spinPower multiplier
        spinSpeed += strength * spinPower;
    }


    // Runs every frame
    void Update()
    {
        // Rotates the object around its UP axis (vertical axis)
        // spinSpeed determines how fast it rotates
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime, Space.Self);

        // Gradually reduce the spin speed so it slows down over time
        spinSpeed *= Mathf.Pow(falloff, Time.deltaTime);
    }
}