using UnityEngine;
using System.Collections.Generic;

public class movingPlatform : MonoBehaviour
{
    [SerializeField] string riderTag = "NPC";
    [SerializeField] MerryGoRoundSpin spinner;

    [SerializeField] float ejectSpinSpeed = 1500f;
    [SerializeField] float ejectVelocity = 12f;     // use a sane velocity, not spinSpeed-scaled
    [SerializeField] float ejectUpBoost = 1.5f;     // optional: little hop so they clear the collider
    [SerializeField] float ejectCooldown = 0.5f;    // seconds before they can be ejected again

    private Dictionary<Rigidbody, float> lastEjectTime = new Dictionary<Rigidbody, float>();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(riderTag))
            collision.transform.SetParent(transform, true);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(riderTag))
            collision.transform.SetParent(null, true);
    }

    void OnCollisionStay(Collision collision)
    {
        if (!collision.collider.CompareTag(riderTag))
            return;

        if (spinner == null)
            return;

        // IMPORTANT: use attachedRigidbody (works even if collider is on a child)
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb == null)
            return;

        // only eject if spinning fast enough
        if (Mathf.Abs(spinner.spinSpeed) < ejectSpinSpeed)
            return;

        // cooldown check so we don't apply 60 ejects/sec
        if (lastEjectTime.TryGetValue(rb, out float t) && Time.time < t + ejectCooldown)
            return;

        lastEjectTime[rb] = Time.time;

        // detach
        collision.transform.SetParent(null, true);

        // fling direction away from center + a bit up
        Vector3 flingDir = (rb.position - transform.position).normalized;
        Vector3 velocity = flingDir * ejectVelocity + Vector3.up * ejectUpBoost;

        // set velocity directly (clean + predictable)
        rb.linearVelocity = velocity;
    }
}