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

    public bool isAggro = false;

    public LayerMask terrainMask;
    public Transform[] waypoints;

    private Transform playerTrans;
    private int currentWaypoint = 0;
    

    public Transform Chest;
    public GameObject Clothing;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool deAggroCooldownOver = false;

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player Center").transform;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = playerTrans.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if player is in FOV
        Vector3 directionToPlayer = playerTrans.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        //Debug.Log(angle);
        Debug.DrawRay(transform.position, directionToPlayer, UnityEngine.Color.red);
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
            navMeshAgent.destination = playerTrans.position;
            if (!isAggro) GoAggro();
        } else if (isAggro && deAggroCooldownOver)
            {
                navMeshAgent.destination = waypoints[currentWaypoint].position;
                LoseAggro();
            }




        float distanceToTarget = Vector3.Distance(navMeshAgent.destination, transform.position);
        if (distanceToTarget < 1)
        {
            if (isAggro)
            {
                Debug.Log("Player dead");

                LoseAggro();
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length) currentWaypoint = 0;
                navMeshAgent.destination = waypoints[currentWaypoint].position;
            }
            else
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length) currentWaypoint = 0;
                navMeshAgent.destination = waypoints[currentWaypoint].position;
            }
            
        }

    }

    public void StealLivery()
    {
        navMeshAgent.destination = playerTrans.position;
        Clothing = null;
        GoAggro();
    }

    private void GoAggro()
    {
        Debug.Log("Aggro");
        isAggro = true;
        deAggroCooldownOver = false;
        navMeshAgent.speed = aggroSpeed;
        navMeshAgent.angularSpeed = aggroRotationSpeed;
        animator.SetFloat("Speed", aggroSpeed);

        Invoke("aggroCooldownOver", 2);
    }

    private void LoseAggro()
    {
        Debug.Log("Lose aggro");
        isAggro = false;
        navMeshAgent.speed = normalSpeed;
        animator.SetFloat("Speed", normalSpeed);
        navMeshAgent.angularSpeed = normalRotationSpeed;
    }

    private void aggroCooldownOver()
    {
        deAggroCooldownOver = true;
    }
}
