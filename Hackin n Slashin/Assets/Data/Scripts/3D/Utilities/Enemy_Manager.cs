using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [Header("States")]
    public State_Idle startingState;
    [SerializeField]private State currentState;

    [Header("Current Target")]
    public BasicBehaviour currentTarget;
    public float distanceFromCurrentTarget;


    [Header("Nav Agent and Animation")]
    public NavMeshAgent enemyNavMeshAgent;
    public Animator anim;

    [Header("Locomotion")]
    public Rigidbody rb;
    public float rotationSpeed = 5;

    [Header("Attack")]
    public float minAttackDistance = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentState = startingState;
        anim = GetComponent<Animator>();
        enemyNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        HandleStateMachine();
    }
    private void Update()
    {
        enemyNavMeshAgent.transform.localPosition = Vector3.zero;
        if(currentTarget != null )
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }

    private void HandleStateMachine()
    {
        State nextState;
        if(currentState != null)
        {
            nextState = currentState.Tick(this);

            if(nextState != null )
            {
                currentState = nextState;
            }
        }
    }
}
