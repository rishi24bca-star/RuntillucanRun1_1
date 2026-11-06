using UnityEngine;

public class ParticlesUpdater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] ParticleSystem particles;
    [SerializeField] private Health health;
    [SerializeField] private bool IsUnit = true;
    void Start()
    {
        if (IsUnit)
        {
            health.OnDamage += Update_Particles;
        }
        else if(!IsUnit)
        {
            PlayerAttack.OnGunShotParticles += OnGunShotParticles;
        }
    }
    
    void Update_Particles()
    {
        
        particles.Play();
         
        
    }

    void OnGunShotParticles()
    {
        particles.Play();
    }
    
}
