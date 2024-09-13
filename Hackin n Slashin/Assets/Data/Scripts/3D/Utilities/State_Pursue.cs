using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Pursue : State
{
    State_Attack attackState;

    private void Awake()
    {
        attackState = GetComponent<State_Attack>();
    }
    public override State Tick(Enemy_Manager enemy_Manager)
    {
        Debug.Log("in pursue state");
        MoveTowards_CurrentTarget(enemy_Manager);
        RotateTowardsTarget(enemy_Manager);

        if (enemy_Manager.distanceFromCurrentTarget <= enemy_Manager.minAttackDistance)
        {
            return attackState;
        }
        else
        {
          return this;
        }

     
    }

    private void MoveTowards_CurrentTarget(Enemy_Manager enemy_Manager)
    {
        //enable movement via animation blend tree
        enemy_Manager.anim.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
    }
    private void RotateTowardsTarget(Enemy_Manager enemy_Manager)
    {
        enemy_Manager.enemyNavMeshAgent.enabled = true;
        enemy_Manager.enemyNavMeshAgent.SetDestination(enemy_Manager.currentTarget.transform.position);
        enemy_Manager.transform.rotation = Quaternion.Slerp(enemy_Manager.transform.rotation, 
            enemy_Manager.enemyNavMeshAgent.transform.rotation, enemy_Manager.rotationSpeed / Time.deltaTime);
    }
}
