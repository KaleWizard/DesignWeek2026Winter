using UnityEngine;

public abstract class BlowBehaviour : MonoBehaviour
{
    private Rigidbody rb;

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
