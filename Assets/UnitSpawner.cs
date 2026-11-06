using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Units units;

    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform playerpos;
    [SerializeField] private float SpawnRadius = 13;
    private float count = 0;
    [SerializeField] private float MinRange = 6;
    bool Timer = false;
    [SerializeField] private float timeLimit = 5f;
    private float time = 0;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Collider[] zombie = Physics.OverlapSphere(playerpos.position, SpawnRadius);
        count = 0;

        HashSet<GameObject> countedZombies = new HashSet<GameObject>();

        foreach (Collider c in zombie)
        {
            GameObject root = c.transform.root.gameObject; // check in parent of the zombie object collider to find parent gameobject and use it to compare Zombie Tag

            if (root.CompareTag("Zombie") && countedZombies.Add(root))   // here compared gameobject tag and added to it to hashset or unique gameobject only if it returns true count ++ 
            {
                count++;
            }
        }

        if (count < 4 && Timer == false)
        {
            int i = Random.Range(1, 4);
            int num = 1;

            while (num <= i && count < 4)
            {
                Vector3 pos = playerpos.position +
                              Random.insideUnitSphere * SpawnRadius;
                pos.y = playerpos.position.y;

                if (NavMesh.SamplePosition(pos, out NavMeshHit hit, SpawnRadius, NavMesh.AllAreas))
                {
                    float dist = Vector3.Distance(hit.position, playerpos.position);

                    if (dist > MinRange)
                    {
                        GameObject newUnit = Instantiate(unitPrefab, hit.position, Quaternion.identity);
                        num++;
                        count++;
                    }
                }
            }

            Timer = true;
        }

        if (Timer == true)
        {
            time += Time.deltaTime;

            if (time > timeLimit)
            {
                Timer = false;
                time = 0f;
            }
        }
    }
}