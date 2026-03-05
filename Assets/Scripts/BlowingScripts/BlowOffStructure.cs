using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BlowOffStructure : BlowBehaviour
{
    [SerializeField] float threshold = 3;
    [SerializeField] List<Collider> collidersToDisable = new();
    [SerializeField] List<GameObject> objsToReparent = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    protected override void OnBlown(Vector3 blowVector)
    {
        TryDetach(blowVector);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        if (force.magnitude < threshold || !rb.isKinematic) return false;

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
