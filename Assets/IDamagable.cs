using UnityEngine;

public interface IDamagable
{

     int health{get;set;}
    void TakeDamage(int damage,bool isPlayer);
    
    
   
}
