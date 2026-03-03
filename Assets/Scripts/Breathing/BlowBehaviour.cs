using UnityEngine;
using UnityEngine.Events;

public abstract class BlowBehaviour : MonoBehaviour
{
    protected Rigidbody rb;


    //public UnityEvent<Vector3> OnBlownEvent;
    protected abstract void OnBlown(Vector3 blowVector);

    public void Blow(Vector3 blowVector)
    {
        if (rb != null)
        {
            rb.AddForce(blowVector, ForceMode.VelocityChange);
        }

        OnBlown(blowVector);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
