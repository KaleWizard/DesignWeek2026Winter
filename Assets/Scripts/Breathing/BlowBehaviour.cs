using UnityEngine;
using UnityEngine.Events;

public abstract class BlowBehaviour : MonoBehaviour
{
    protected Rigidbody rb;


    //public UnityEvent<Vector3> OnBlownEvent;
    protected virtual void OnBlown(Vector3 blowVector, Transform other) { }
    protected virtual void OnBlown(Vector3 blowVector) { }

    public void Blow(Vector3 blowVector, Transform other)
    {
        if (rb != null)
        {
            rb.AddForce(blowVector, ForceMode.VelocityChange);
        }

        OnBlown(blowVector, other);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
