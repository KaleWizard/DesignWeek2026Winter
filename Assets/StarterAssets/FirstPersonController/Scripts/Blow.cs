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


    [SerializeField] float refreshTime = 0.5f;

    [Header("General")]
    [SerializeField] FirstPersonController playerController;

    [SerializeField] ParticleSystem particleSpawner;

    [SerializeField] float timeBetweenWinds = 0.25f;

    [Header("FOV")]
    [SerializeField] float minFOV = 50f;
    [SerializeField] float maxFOV = 70f;
    [SerializeField] CinemachineVirtualCamera cam;

    List<BlowBehaviour> blowBodies = new();

    float particleTimer = 0;

    // Blow Cooldown
    float refreshRate = 0;
    public float secondLastBreathValue = 1;
    public float lastBreathValue = 1;

    private void Start()
    {
        refreshRate = 1 / refreshTime;
    }

    private void Update()
    {
        particleTimer += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float blowVal = -GetBlowStrength();

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

    float GetBlowStrength()
    {
        secondLastBreathValue = lastBreathValue;
        lastBreathValue = Mathf.Min(1 - BreathInput.Value, lastBreathValue + refreshRate * Time.deltaTime);
        return lastBreathValue - secondLastBreathValue;
    }

    void BlowBody(BlowBehaviour bb, float blowValue)
    {
        if (!ShouldBlow(bb)) return;

        Vector3 direction =
            transform.forward * 2
            + (bb.transform.position - transform.position);

        float mod = blowValue * (blowValue > 0 ? exhaleStrength : inhaleStrength);

        Vector3 force = mod * direction.normalized;

        bb.Blow(force, transform);
    }

    bool ShouldBlow(BlowBehaviour bb)
    {
        return true;
    }

    void BlowPlayer(float blowValue)
    {
        AlterFOV(blowValue);
        if (blowValue < 0) return;
        float mod = playerMoveStrength * blowValue;
        playerController.AddVelocity(mod * -transform.forward);

        if (blowValue > 0.1f && particleTimer > timeBetweenWinds)
        {
            SpawnGust(blowValue);
        }
    }

    void SpawnGust(float blowValue)
    {
        var shape = particleSpawner.shape;
        shape.rotation = transform.eulerAngles;

        var main = particleSpawner.main;
        var particleSpeed = main.startSpeed;
        particleSpeed.constantMin = blowValue * 16;
        particleSpeed.constantMax = blowValue * 24;
        particleSpeed.mode = ParticleSystemCurveMode.TwoConstants;

        ParticleSystem newSpawner = Instantiate(particleSpawner);
        newSpawner.transform.SetParent(null, true);
        newSpawner.transform.position = transform.position;
        newSpawner.Play();
        Destroy(newSpawner.gameObject, 10);

        particleTimer = 0;
    }

    void AlterFOV(float blowValue)
    {
        cam.m_Lens.FieldOfView = math.remap(0f, 1f, minFOV, maxFOV, 1 - BreathInput.Value);
    }
}
