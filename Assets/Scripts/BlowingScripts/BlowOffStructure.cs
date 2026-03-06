using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BlowOffStructure : BlowBehaviour
{
    [SerializeField] float threshold = 3;
    [SerializeField] List<Collider> collidersToDisable = new();
    [SerializeField] List<GameObject> objsToReparent = new();

    public bool dontDetach = false;
    [SerializeField] bool ignorePlayer = false;

    int playerLayerHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        playerLayerHash = LayerMask.NameToLayer("Player");
    }

    protected override void OnBlown(Vector3 blowVector)
    {
        TryDetach(blowVector);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == playerLayerHash) return;
        if (TryDetach(collision.impulse))
        {
            if (collision.rigidbody != null)
            {
                //collision.rigidbody.AddForce(-collision.impulse, ForceMode.VelocityChange);
            }
        }
    }

    bool TryDetach(Vector3 force)
    {
        if (dontDetach || force.magnitude < threshold || !rb.isKinematic) return false;

        foreach (var c in collidersToDisable)
        {
            if (c != null) 
                c.enabled = false;
        }

        foreach (var obj in objsToReparent)
            obj.transform.SetParent(null, true);

        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.VelocityChange);
        rb.AddTorque(Random.insideUnitSphere * 10);
        return true;
    }
}
