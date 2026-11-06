using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System;

public class  PlayerAttack : MonoBehaviour
{
    // player attack should use gun   (guns will contain  ammo   gun name ,gun type , reload time , gun prefab,damage etc)    
    [SerializeField] private Guns guns;
    private int reload_time;
    [SerializeField] private Transform GunPoint;
    [SerializeField] private Transform Rotate;
    private int Damage;
    [SerializeField] private PlayerMovement playerMovement;
    private Ray ray;
    public event Action AttackStarted;
    public bool IsAttacking { get; set; }
    private bool IsTimeOut = false;
    private float TimeOut_GunTime;
    [Range(6, 32)] private int Bullet_Count;
    int CurrentBullet_Count;
    private bool Started = false;
    public static event Action OnGunShotParticles;
    public static event Action<int,int> OnPlayerBulletCountChanged;
   
    private Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
      
       IsAttacking = false;
       reload_time = guns.Reload_time;
       TimeOut_GunTime = guns.Time_;
       Bullet_Count = guns.bullet_count;
       Damage = guns.Damage_;
       CurrentBullet_Count = Bullet_Count;
  
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if (!Started)
        {
            OnPlayerBulletCountChanged?.Invoke(CurrentBullet_Count, Bullet_Count);
            Started = true; 
        }
        if (Input.GetKey(KeyCode.R))
        {
            Invoke("Reload", reload_time);
           
        }
        ray = new Ray(GunPoint.position,playerMovement.Direction);
       

       
       
     
           

       
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsTimeOut)
        {
            if ( CurrentBullet_Count > 0)
            {
                Attack(Damage);
                
                IsAttacking = true;
                
            }
        }
        else
        {
            IsAttacking = false;
        }

        if (IsAttacking)
        {
            AttackStarted?.Invoke();
        }

       
    }

    void Attack(int damage)
    {
        OnGunShotParticles?.Invoke();
        IsTimeOut = true;
        CurrentBullet_Count--;
        OnPlayerBulletCountChanged?.Invoke(CurrentBullet_Count, Bullet_Count);
        Debug.Log(damage);
       
        RaycastHit hit;
        
        Debug.Log("attack Function");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponentInParent<IDamagable>() != null)
            {
                // hit.gameobject.damage events  
                if (hit.collider.CompareTag("Zombie"))
                {
                    hit.collider.GetComponentInParent<Health>().TakeDamage(damage,false);
                    Debug.Log(hit.collider.name + "damage taken");
                }
            }
        }
        if (IsTimeOut)
        {
            Invoke("TimeOut", TimeOut_GunTime);
        }
    }
    void TimeOut(){
        IsTimeOut = false;
        
    }

    void Reload()
    {
        CurrentBullet_Count = guns.bullet_count;
        OnPlayerBulletCountChanged?.Invoke(CurrentBullet_Count, Bullet_Count);
    }
   
 
}
