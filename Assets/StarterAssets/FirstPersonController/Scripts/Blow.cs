using Cinemachine;
using StarterAssets;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blow : MonoBehaviour
{
    [SerializeField] float inhaleStrength = 10f;
    [SerializeField] float exhaleStrength = 2f;

    [SerializeField] float playerMoveStrength = 10f;

    [SerializeField] FirstPersonController playerController;

    [SerializeField] ParticleSystem particleSpawner;

    [SerializeField] float timeBetweenWinds = 0.25f;

    [Header("FOV")]
    [SerializeField] float minFOV = 50f;
    [SerializeField] float maxFOV = 70f;
    [SerializeField] CinemachineVirtualCamera cam;

    List<BlowBehaviour> blowBodies = new();

    float timer = 0;
    AnimationCurve ease;



    private void Start()
    {
        ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float blowVal = BreathInput.FixedDelta;

        foreach (var b in blowBodies)
        {
            BlowBody(b, blowVal);
        }

        BlowPlayer(blowVal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BlowBehaviour>(out var bb))
        {
            blowBodies.Add(bb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BlowBehaviour>(out var bb))
        {
            blowBodies.Remove(bb);
        }
    }

    void BlowBody(BlowBehaviour bb, float blowValue)
    {
        if (!ShouldBlow(bb)) return;

        Vector3 direction =
            transform.forward * 2
            + (bb.transform.position - transform.position);

        float mod = blowValue * (blowValue > 0 ? inhaleStrength : exhaleStrength);

        Vector3 force = mod * direction.normalized;

        bb.Blow(force, transform);
    }

    bool ShouldBlow(BlowBehaviour bb)
    {
        return true;
    }

    void BlowPlayer(float blowValue)
    {
        if (blowValue < 0) return;
        float mod = playerMoveStrength * blowValue;
        playerController.AddVelocity(mod * -transform.forward);

        if (blowValue > 0.1f && timer > timeBetweenWinds)
        {
            SpawnGust(blowValue);
        }

        AlterFOV(blowValue);
    }

    void SpawnGust(float blowValue)
    {
        var shape = particleSpawner.shape;
        shape.rotation = transform.eulerAngles;

        var main = particleSpawner.main;
        main.startSpeed = blowValue * 20;

        ParticleSystem newSpawner = Instantiate(particleSpawner);
        newSpawner.transform.SetParent(null, true);
        newSpawner.transform.position = transform.position;
        newSpawner.Play();
        Destroy(newSpawner.gameObject, 10);

        timer = 0;
    }

    void AlterFOV(float blowValue)
    {
        cam.m_Lens.FieldOfView = math.remap(-1f, 1f, minFOV, maxFOV, BreathInput.Value);
    }
}
