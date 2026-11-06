using UnityEngine;

public class AnimationZombieController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioZombie_2;
    [SerializeField] private UnitMovement un_Movement;
    [SerializeField] AudioSource ZombieSound;
    [SerializeField] AudioClip ZombieSoundClip_Attack;
    [SerializeField] AudioClip ZombieSoundClip_Idle;
    [SerializeField] AudioClip ZombieSoundClip_Walk_Moan;
    [SerializeField] private AudioClip ZombieSoundClip_Walking;
    [SerializeField] private AudioClip ZombieSoundClip_Dead;
    [SerializeField] private AudioClip ZombieSoundClip_OnDamage;
    [SerializeField] private Health health;
    [SerializeField]private AudioSource Dead;
    private static readonly int IsWalking =
        Animator.StringToHash("IsWalking");

    private static readonly int IsAttacking =
        Animator.StringToHash("IsAttacking");
    bool Idel = true;
    
    private void Start()
    {
        health.OnZombieHit += ZombieDamage;
        health.OnZombieDeath += OnZombieDeath;
        // Subscribe to movement events
        un_Movement.OnIdeal += PlayIdle;
        un_Movement.OnMovement += PlayWalking;
        un_Movement.OnAttacked += PlayAttack;
        

        // Initial animation
        PlayIdle();
    }

    private void OnDestroy()
    {
        health.OnZombieHit -= ZombieDamage;
        health.OnZombieDeath -= ZombieDamage;
        // Unsubscribe from events
        if (un_Movement == null) return;

        un_Movement.OnIdeal -= PlayIdle;
        un_Movement.OnMovement -= PlayWalking;
        un_Movement.OnAttacked -= PlayAttack;
    }

    private void PlayIdle()
    {
        Debug.Log("IDLE EVENT");
        animator.SetBool(IsWalking, false);
        Idel = true;
        ZombieSound.PlayOneShot(ZombieSoundClip_Idle);
        
    }

    private void PlayWalking(bool  isWalking)
    {
        Debug.Log("WALKING EVENT");
        if (isWalking)
        {
            Idel = false;
            ZombieSound.clip = ZombieSoundClip_Walk_Moan;
            ZombieSound.Play();
            audioZombie_2.clip = ZombieSoundClip_Walking;
            audioZombie_2.Play();
            animator.SetBool(IsWalking, true);
        }
        else
        {
           if(!Idel) 
            ZombieSound.Stop();
            audioZombie_2.Pause();
            
            
            
           
        }
    }

    private void PlayAttack()
    {
        Debug.Log("ATTACK EVENT");
        ZombieSound.PlayOneShot(ZombieSoundClip_Attack);
        animator.SetBool(IsWalking, false);
        animator.SetTrigger(IsAttacking);
    }

    private void ZombieDamage()
    {
        ZombieSound.PlayOneShot(ZombieSoundClip_OnDamage);
    }

    private void OnZombieDeath()
    {
        
        
        
        Dead.PlayOneShot(ZombieSoundClip_Dead);
    }
}