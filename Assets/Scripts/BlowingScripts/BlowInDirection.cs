using UnityEngine;

public class BlowInDirection : BlowBehaviour
{
    [SerializeField] float amplify = 0f;

    protected override void OnBlown(Vector3 blowDirection)
    {
        if (blowDirection.y < 0.1f) blowDirection.y = 0.1f;
        rb.AddForce(blowDirection * amplify, ForceMode.VelocityChange);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
