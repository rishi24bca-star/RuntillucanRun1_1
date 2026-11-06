using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Guns", menuName = "Scriptable Objects/Guns")]
public class Guns : ScriptableObject
{
    // player attack should use gun   (guns will contain  ammo   gun name ,gun type , reload time , gun prefab,damage etc)    

   
   [SerializeField] private string GunName;
   [SerializeField] private GunTypes gn_type;
   [SerializeField] private  int reload_time = 2;
   [SerializeField] private GameObject GunObject;
   [SerializeField] private int Damage;
   [SerializeField] private int range;
   
   
   [SerializeField] private float TimeOutTime = 1.3f;
   [SerializeField][Range(6,32)] int Bullet_Count = 6;
   
    public int bullet_count{get{return Bullet_Count;}}
    public int Damage_{get{return Damage;}}
    public  int Reload_time
    {
        get { return reload_time; }
    }
    public float Time_{get{return TimeOutTime;}}
    enum GunTypes
    {
        AR,
        Shotgun,
        Rifle
    }

}
