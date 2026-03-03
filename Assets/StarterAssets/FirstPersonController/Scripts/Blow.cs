using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blow : MonoBehaviour
{
    [SerializeField] float inhaleStrength = 10f;
    [SerializeField] float exhaleStrength = 2f;

    [SerializeField] float playerMoveStrength = 10f;

    [SerializeField] FirstPersonController playerController;

    List<BlowBehaviour> blowBodies = new();

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
        if (other.TryGetComponent<BlowBehaviour>(out var bb))
        {
            blowBodies.Add(bb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BlowBehaviour>(out var bb))
        {
            blowBodies.Remove(bb);
        }
    }

    void BlowBody(BlowBehaviour bb, float blowValue)
    {
        if (!ShouldBlow(bb)) return;

        Vector3 direction =
            transform.forward * 2
            + (bb.transform.position - transform.position);

        float mod = blowValue * (blowValue > 0 ? inhaleStrength : exhaleStrength);

        Vector3 force = mod * direction.normalized;

        bb.Blow(force);
    }

    bool ShouldBlow(BlowBehaviour bb)
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
