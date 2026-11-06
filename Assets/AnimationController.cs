using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private AudioSource _audioSource2;
    private bool Isideal = true;
    [SerializeField] AudioSource PlayerAudio;
    [SerializeField] private AudioClip PlayerDead;
    [SerializeField] AudioClip PlayerHit;
    [SerializeField] AudioClip PlayerRun;
    [SerializeField] AudioClip PlayerGunShot;
    bool IsHitting = false;
    private bool IsPlayed = false;

    private void Start()
    {
        //
    }

    void Awake()
    {
        Health.OnPlayerHit += OnPlayerHit;
        Health.OnPlayerDead += OnPlayerDead;
        playerAttack.AttackStarted += HandelGunShot;
        playerMovement.MovementStateChanged += HandelMovementAnim;
    }
    void OnDestroy()
    {
        Health.OnPlayerHit -= OnPlayerHit;
        Health.OnPlayerDead -= OnPlayerDead;
        playerAttack.AttackStarted -= HandelGunShot;
        playerMovement.MovementStateChanged -= HandelMovementAnim;
    }

    private void HandelMovementAnim(bool moving)
    {
        if (moving == true)
        {
            playerAnimator.ResetTrigger("Isattack");
            playerAnimator.ResetTrigger("Isideal");
            playerAnimator.SetTrigger("IsRunning");

            if (PlayerRun != null && !PlayerAudio.isPlaying)
            {
                PlayerAudio.clip = PlayerRun;
                PlayerAudio.loop = true;
                PlayerAudio.Play();
            }

            Isideal = false;
        }
        else
        {
            playerAnimator.ResetTrigger("IsRunning");
            PlayerAudio.clip = null;
            PlayerAudio.Stop();

            playerAnimator.SetTrigger("Isideal");
            Isideal = true;
        }
    }

    private void HandelGunShot()
    {
        if (PlayerGunShot != null)
        {
            _audioSource2.PlayOneShot(PlayerGunShot);
        }

        IsHitting = true;

        if (Isideal == true)
        {
            playerAnimator.ResetTrigger("Isideal");
            playerAnimator.SetTrigger("Isattack");
        }
    }

    public void AttackFinished()
    {
        IsHitting = false;
    }

    public void OnPlayerDead()
    {
        PlayerAudio.PlayOneShot(PlayerDead);
    }

    public void OnPlayerHit()
    {
        _audioSource2.PlayOneShot(PlayerHit);
    }
}