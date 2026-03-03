using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class Blow : MonoBehaviour
{
    [SerializeField] float inhaleStrength = 10f;
    [SerializeField] float exhaleStrength = 2f;

    [SerializeField] float playerMoveStrength = 10f;

    [SerializeField] FirstPersonController playerController;

    List<Rigidbody> blowBodies = new();

    // Update is called once per frame
    void FixedUpdate()
    {
        float blowVal = BreathInput.AccumulatedDelta;

        foreach (var b in blowBodies)
        {
            BlowBody(b, blowVal);
        }

        BlowPlayer(blowVal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rb))
        {
            blowBodies.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out var rb))
        {
            blowBodies.Remove(rb);
        }
    }

    void BlowBody(Rigidbody rb, float blowValue)
    {
        if (!ShouldBlow(rb)) return;

        Vector3 direction =
            transform.forward * 2
            + (rb.transform.position - transform.position);

        float mod = blowValue * (blowValue > 0 ? inhaleStrength : exhaleStrength);
        Vector3 force = mod * direction.normalized;

        rb.AddForce(force, ForceMode.VelocityChange);
    }

    bool ShouldBlow(Rigidbody rb)
    {
        return true;
    }

    void BlowPlayer(float blowValue)
    {
        if (blowValue < 0) return;
        float mod = playerMoveStrength * blowValue;
        playerController.AddVelocity(mod * -transform.forward);
    }
}
