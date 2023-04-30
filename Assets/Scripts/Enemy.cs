using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float viewAngle = 90f;
    public float viewDistance = 10f;
    public float aggroSpeed = 6f;
    public float normalSpeed = 3f;
    public float aggroRotationSpeed = 360;
    public float normalRotationSpeed = 120;


    public int state;
    // 0 - patrolling
    // 1 - currently sees player
    // 2 - going to last known location of player
    // 3 - AFK
    
    public LayerMask terrainMask;
    public Transform[] waypoints;

    private Transform playerTrans;
    private Vector3 lastKnownPlayerLocation;
    private int currentWaypoint = 0;
    

    public Transform Chest;
    public GameObject Livery;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Start()
    {
        Livery = Instantiate(Livery, Chest);
        playerTrans = GameObject.FindGameObjectWithTag("Player Center").transform;
        lastKnownPlayerLocation = playerTrans.position;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMeshAgent.destination = waypoints[0].position;

        state = 0;
        transitionState(0);
    }

    void Update()
    {
        // Check if player is in FOV
        Vector3 directionToPlayer = playerTrans.position - transform.position;
        Vector3 directionToLastknownPlayerLoc = lastKnownPlayerLocation - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        
        Debug.DrawRay(transform.position, directionToPlayer, UnityEngine.Color.red);
        Debug.DrawRay(transform.position, directionToLastknownPlayerLoc, UnityEngine.Color.blue);
        bool playerInVision = false;
        if (angle < viewAngle / 2)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, terrainMask))
            {
                playerInVision = true;
            }
        }

        if (playerInVision)
        {
            lastKnownPlayerLocation = playerTrans.position;
            if (state == 0 || state == 2 || state == 3) transitionState(1);
        } else
        {
            if (state == 1) transitionState(2);
        }



        if (state == 0 || state == 1 || state == 2)
        {
            float distanceToTarget = Vector3.Distance(navMeshAgent.destination, transform.position);
            if (distanceToTarget < 2)
            {
                if (state == 0)
                {
                    // reached waypoint
                    currentWaypoint++;
                    if (currentWaypoint == waypoints.Length) currentWaypoint = 0;
                    navMeshAgent.destination = waypoints[currentWaypoint].position;
                } else if (state == 1)
                {
                    // reached player
                    Debug.Log("Player dead");
                    transitionState(0);
                    
                } else if (state == 2)
                {
                    // reached last known location of player
                    transitionState(3);
                    Invoke("StartPatrol", Random.Range(2.5f, 5));
                }
            }
        }
        

    }

    public void StealLivery()
    {
        Destroy(Livery.gameObject);
        transitionState(3);
        Invoke("EndDisoriented", 1.5f);


    }

    private void transitionState(int toState)
    {
        Debug.Log("transition to " + toState.ToString());
        if (toState == 0)
        {
            // start patrol
            state = 0;
            navMeshAgent.destination = waypoints[currentWaypoint].position;
            LoseAggro();
        } else if (toState == 1)
        {
            // notice player
            state = 1;
            navMeshAgent.destination = playerTrans.position;
            GoAggro();
        } else if (toState == 2)
        {
            // start going to last known player location
            state = 2;
            navMeshAgent.destination = lastKnownPlayerLocation;
            GoAggro();
        } else if (toState == 3)
        {
            // wait for a bit
            state = 3;
            navMeshAgent.destination = transform.position;
            LoseAggro();
            animator.SetFloat("Speed", 0);
        }
    }

    private void GoAggro()
    {
        Debug.Log("Aggro");
        
        navMeshAgent.speed = aggroSpeed;
        navMeshAgent.angularSpeed = aggroRotationSpeed;
        animator.SetFloat("Speed", 1);
    }

    private void LoseAggro()
    {
        Debug.Log("Lose aggro");
        navMeshAgent.speed = normalSpeed;
        animator.SetFloat("Speed", 0.15f);
        navMeshAgent.angularSpeed = normalRotationSpeed;
    }

    private void EndDisoriented()
    {
        lastKnownPlayerLocation = playerTrans.position;
        transitionState(1);
    }

    private void StartPatrol()
    {
        transitionState(0);
    }
}
