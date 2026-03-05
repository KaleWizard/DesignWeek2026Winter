using System.Collections.Generic;
using UnityEngine;

public class PigRagdoll : BlowBehaviour
   {
    public List<Rigidbody> rigBodies = new();
    public Animator anim;

    protected override void OnBlown(Vector3 blowDirection)
    {
        if (blowDirection.magnitude > 1)
        {
            var anim = GetComponentInParent<Animator>();
            if (anim != null)
            {
                anim.enabled = false;
            }

            foreach (Rigidbody rb in rigBodies)
            {
                rb.isKinematic = false;
            }
        }

    }
}

