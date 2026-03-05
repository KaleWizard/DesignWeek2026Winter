using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindmillRotate : BlowBehaviour
{
    [SerializeField] float falloff = 0.9f;
    [SerializeField] float amplify = 4f;

    float angularSpeed = 0;

    public AudioSource windmillAudio;
    public AudioClip windmillClip;
    protected override void OnBlown(Vector3 blowDirection)
    {
        bool positiveRotate = Vector3.Dot(blowDirection, transform.right) > 0;

        angularSpeed += (positiveRotate ? 1 : -1) * blowDirection.sqrMagnitude * amplify;
        
        if (Mathf.Abs(angularSpeed) > 200f && !windmillAudio.isPlaying)
        {
            windmillAudio.clip = windmillClip;
            windmillAudio.Play();
        } else if (Mathf.Abs(angularSpeed) < 200f)
        {
            windmillAudio.Stop();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.angularDamping = 0.25f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rot = transform.localEulerAngles;
        bool reverseDirection = rot.y > 90;
        rot.x += (reverseDirection ? -1 : 1) * angularSpeed * Time.deltaTime;
        transform.localEulerAngles = rot;

        angularSpeed *= Mathf.Pow(falloff, Time.deltaTime);
    }
}
