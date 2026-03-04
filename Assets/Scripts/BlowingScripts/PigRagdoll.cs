using UnityEngine;

public class PigRagdoll : BlowBehaviour

   {
    public BoxCollider PigCollider;
    public GameObject PigRig;
    protected override void OnBlown(Vector3 blowDirection)
    {
        if (blowDirection.magnitude > 1)
        {
            PigCollider.enabled = false;
            PigRig.SetActive(true);
        }

    }
}

