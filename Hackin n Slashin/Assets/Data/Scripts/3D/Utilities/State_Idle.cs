using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{
    State_Pursue pursueTarget_State;

    [Header("Line of Sight")]//dtermines where the linecast to find character will emit from
    [SerializeField] float character_EyeLevel = 2f;
    [SerializeField] LayerMask ignoreForLineOfSight;

    [Header("Detection Radius & Layer")]//how far we can detect target
    [SerializeField] bool hasTarget;
    [SerializeField] LayerMask detetctionLayer;
    [SerializeField] float detectionRadius = 5f;

    [Header("Detection Angle Radius")]//being able to SEE the target within our field of view
    [SerializeField] float minimumDetection_RadiusAngle = -50f;
    [SerializeField] float maximumDetection_RadiusAngle = 50f;

    private void Awake()
    {
        pursueTarget_State = GetComponent<State_Pursue>();
    }

    public override State Tick(Enemy_Manager enemy_Manager)
    {
        if (enemy_Manager.currentTarget != null)
        {
            return pursueTarget_State;
        }
        else
        {
            FindTarget_Via_LineOfSight(enemy_Manager);
            return this;
        }
    }

    private void FindTarget_Via_LineOfSight(Enemy_Manager enemy_Manager)
    {
        //look fo colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detetctionLayer);
        Debug.Log("Checking For Colliders:");

        for(int i =  0; i < colliders.Length; i++)//for every collider see if one of them is the player
        {
            BasicBehaviour player = colliders[i].transform.GetComponent<BasicBehaviour>();

            //if found, check for line of sight
            if(player != null)
            {
                Debug.Log("Player Collider: FOUND");

                Vector3 targetDir = player.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDir, transform.forward);
                if(viewableAngle > minimumDetection_RadiusAngle && viewableAngle < maximumDetection_RadiusAngle)
                {
                    Debug.Log("Field Of View Check Passed");

                    //to make sure the raycast does not start at the floor of the character since the logic is calling on the script, not the model itself
                    RaycastHit hit;
                    Vector3 enemyStartPoint = new Vector3(transform.position.x, character_EyeLevel, transform.position.z);
                    Vector3 playerStartPoint = new Vector3(player.transform.position.x,character_EyeLevel, player.transform.position.z);
                    Debug.DrawLine(playerStartPoint, enemyStartPoint, Color.yellow);


                    //check forr obstructions blocking view
                    if (Physics.Linecast(playerStartPoint, enemyStartPoint, out hit, ignoreForLineOfSight))
                    {
                        Debug.Log("FOV Check:Something In The Way");

                        //there is an object in the way, cannot find target
                    }
                    else
                    {
                        Debug.Log("FOV Check:Target Acquired. Switching States");

                        enemy_Manager.currentTarget = player;
                    }
                }
            }
        }
    }
}
