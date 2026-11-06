using System;
using System.Data;
using Unity.VectorGraphics;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private Units unit;
    public  event Action<int,int> OnPlayerTakenDmage;
    public  event Action OnDamage;
    public static event Action OnPlayerDead;
    public static  event Action OnPlayerHit;
    public event Action OnZombieHit;
    public event Action OnZombieDeath;

    private float DeathDelay = 1.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health{get; set; }
    private int CurrentHealth;
    void Awake()
    {
        health = unit.Unit_Hp;
        CurrentHealth = health;
    }
     public void TakeDamage(int damage,bool isPlayer)
     {
         CurrentHealth -= damage;
         
             OnDamage?.Invoke();
             if (isPlayer)
             {
                 OnPlayerHit?.Invoke();


                 OnPlayerTakenDmage?.Invoke(health, CurrentHealth);
             }

             if (!isPlayer)
             {
                 OnZombieHit?.Invoke();
             }

             if (CurrentHealth <= 0)
         {
             Die();
             
         }
     }

     public void Die()
     {
         if (gameObject.tag == "Player")
         {
              Time.timeScale = 0;
              MonoBehaviour[] scripts =
                  FindObjectsByType<MonoBehaviour>(
                      FindObjectsSortMode.None
                  );

              foreach (MonoBehaviour script in scripts)
              {
                  if (script != this && script.tag == "Player")
                  {
                      script.enabled = false;
                      
                  }
              }
              OnPlayerDead?.Invoke();
              
              Debug.Log("Destroyed");
         }
         else
         {
             OnZombieDeath?.Invoke();
             OnDamage?.Invoke();
             
             Invoke(nameof(destroy),DeathDelay);
             
         }
     }

     public void destroy()
     {
         Destroy(gameObject);
     }
}
