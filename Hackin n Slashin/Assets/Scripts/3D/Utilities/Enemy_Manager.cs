using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [Header("States")]
    public Idle_State startingState;
    [SerializeField]private State currentState;

    [Header("Current Target")]
    public BasicBehaviour currentTarget;

    [Header("Nav Agent and Animation")]
    public NavMeshAgent enemyNavMeshAgent;
    public Animator anim;

    [Header("Locomotion")]
    public Rigidbody rb;
    public float rotationSpeed = 5;

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
