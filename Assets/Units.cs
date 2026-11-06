using UnityEngine;

[CreateAssetMenu(fileName = "Units", menuName = "Scriptable Objects/Units")]
public class Units : ScriptableObject
{
    [SerializeField] int health;
    [SerializeField] string name;
    [SerializeField]private Types unittypes;
    [SerializeField] private int speed = 5;
    [SerializeField] private int maxDetectionRange = 5;
    [SerializeField] private float Stop_Range = 2;
    [SerializeField] private int damage = 10;
    public int Unit_Hp{get{return health;}}
    public int Speed{get{return speed;}}
    public float StoppingRange{get{return Stop_Range;}}
    public int MaxDetectionRange{get{return maxDetectionRange;}}
    public int Damage{get{return damage;}}
    enum Types
    {
        Player,
        Zombie,
        JackedZombie
    }
}
