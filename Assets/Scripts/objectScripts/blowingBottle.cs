using UnityEngine;

public class BlowInstrument : BlowBehaviour
{
    public enum Type : byte { Horn, Bottle }

    [SerializeField] AudioSource source;
    [SerializeField] Type type = Type.Bottle;

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        if (col == null)
        {
            print($"{gameObject.name} expected collider but none was found!");
        }
    }

    protected override void OnBlown(Vector3 blowDirection, Transform other)
    {
        // ignore tiny blows
        if (blowDirection.sqrMagnitude < 0.01f)
            return;

        // if already playing, don't restart
        if (source.isPlaying)
            return;

        Ray ray = new()
        {
            direction = other.forward,
            origin = other.position,
        };
        if (type == Type.Horn && (!col.Raycast(ray, out var _, 6f) || Vector3.Dot(transform.forward.normalized, other.forward.normalized) < 0.5f))
            return;

        if (type == Type.Bottle && (!col.Raycast(ray, out var _, 3f) || Mathf.Abs(Vector3.Dot(transform.up.normalized, other.forward.normalized)) > 0.5f))
            return;

        // play the whole sound once
        source.Play();
    }
}