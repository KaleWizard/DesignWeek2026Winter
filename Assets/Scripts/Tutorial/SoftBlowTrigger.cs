using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoftBlowTrigger : BlowBehaviour
{
    [SerializeField] float speed;

    new Collider collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider>();
        if (!collider)
        {
            Debug.LogWarning($"{gameObject.name} needs a collider!");
            Destroy(this); 
            return;
        }
    }

    protected override void OnBlown(Vector3 blowVector, Transform other)
    {
        print("Trying to show UI");
        Ray ray = new()
        {
            direction = other.forward,
            origin = other.position,
        };
        if (collider.Raycast(ray, out var _, 6f)) {
            print("Resetting");
            SoftBlowUI.Instance.Increase();
        }
    }
}
