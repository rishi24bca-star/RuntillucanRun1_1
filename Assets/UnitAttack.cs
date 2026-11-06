using System.Net.Mail;
using UnityEngine;
using System;

public class UnitAttack : MonoBehaviour
{
    [SerializeField]private Units units;
    private int AttackDamage;
    private GameObject target;
    private int sphereRadius = 5;
    private int counter;
    private bool Playerfound;
    [SerializeField]private ParticleSystem particles_Attack;
   
   
    void Awake()
    {
       
        AttackDamage = units.Damage;
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }
    public  void Attack()
    {
        Playerfound = false;
         Debug.Log("attack Called");
         Vector3 boxSize = new Vector3(2f, 2f, 2f);

         Vector3 boxCenter =
             transform.position + transform.forward * 2f;
        
         particles_Attack.Play();
         Collider[] hits = Physics.OverlapBox(
             boxCenter,
             boxSize / 2f
         );
             
        // check if shpere detects player 
        foreach(Collider hit in hits)
        {
            if (hit != null )
            {
                if (hit.CompareTag("Player") && !Playerfound)
                {
                    Playerfound = true;
                    Debug.Log("playerFound");
                    if (hit.GetComponentInParent<IDamagable>() != null)
                    {

                        hit.GetComponent<Health>().TakeDamage(AttackDamage,true);
                        Debug.Log(hit.name);
                        


                    }
                }
            }
        }
    }
}
