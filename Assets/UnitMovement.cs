
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private Units units;

    private NavMeshAgent agent;
    private int speed;
    private Transform target;
    private int detectionRange;
    private float StoppingRange;
    private UnitAttack attack;

    private bool Attack;
    private bool Invoked;

    public event Action<bool> OnMovement;
    public event Action OnIdeal;
    public event Action OnAttacked;
    int counter = 0;
    private enum State
    {
        moving,
        stop,
        Attack
    }

    private State state = State.stop;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        agent = GetComponent<NavMeshAgent>();
        attack = GetComponent<UnitAttack>();

        speed = units.Speed;
        detectionRange = units.MaxDetectionRange;
        StoppingRange = units.StoppingRange;
        
       if(target != null){ 
        agent.SetDestination(target.position);
        }
        agent.stoppingDistance = StoppingRange;
    }

    void Update()
    {
        if (target != null)
        {
            // Existing attack cooldown logic
            if (Attack && !Invoked)
            {
                Invoke(nameof(AttackChange), 3f);
                Invoked = true;
            }

            float distance = Vector3.Distance(
                transform.position,
                target.position
            );

            if (distance <= detectionRange)
            {
                Vector3 direction = target.position - transform.position;

                if (distance > StoppingRange)
                {
                    // Movement state
                    if (agent.velocity.sqrMagnitude > 1f)
                    {
                        if (state != State.moving)
                        {
                            OnMovement?.Invoke(true);
                            state = State.moving;
                        }
                    }

                    agent.SetDestination(target.position);
                }
                else
                {
                    OnMovement?.Invoke(false);
                    agent.ResetPath();
                   
                    direction.y = 0f;

                    if (!Attack && direction != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(direction);
                    }

                    if (!Attack)
                    {

                        // Attack state

        

                        if (state != State.Attack)
                        {

                            OnAttacked?.Invoke();
                            int num = counter++;
                            Debug.Log("counter" + num);
                            state = State.Attack;
                        }




                        Attack = true;
                    }
                }
            }
            else
            {
                agent.ResetPath();
                OnMovement?.Invoke(false);
                // Idle state
                if (state != State.stop && !Attack)
                {
                    OnIdeal?.Invoke();
                    state = State.stop;
                }
            }
        }
    }

    void AttackChange()
    {
        Attack = false;
        state = State.stop;
        Invoked = false;
    }
}
