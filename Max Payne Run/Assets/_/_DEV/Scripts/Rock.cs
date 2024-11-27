using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSysteme;
    [SerializeField] AudioSource boulderSmashAudio; 
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisionCooldown = 1f;

    CinemachineImpulseSource cinemachineImpulseSource;

    float collisionTimer = 1f;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (collisionTimer < collisionCooldown) return;
        
        FireImpulse();
        CollisionFX(other);
        collisionTimer = 0;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);

        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSysteme.transform.position = contactPoint.point;
        collisionParticleSysteme.Play();
        boulderSmashAudio.Play();
    }
}