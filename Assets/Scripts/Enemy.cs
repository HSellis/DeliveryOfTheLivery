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
    public float aggroSpeed = 5f;
    public float normalSpeed = 3f;

    public bool isAggro = false;

    public LayerMask terrainMask;
    public Transform[] waypoints;

    private Transform playerTrans;
    private int currentWaypoint = 0;
    

    public Transform Chest;
    public GameObject Clothing;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player Center").transform;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = playerTrans.position;
    }

    void Update()
    {
        // Check if player is in FOV
        Vector3 directionToPlayer = playerTrans.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        //Debug.Log(angle);
        Debug.DrawRay(transform.position, directionToPlayer, UnityEngine.Color.red);
        if (angle < viewAngle / 2)
        {

            float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, terrainMask))
            {
                Debug.Log("Player in FOV");
                navMeshAgent.destination = playerTrans.position;
                GoAggro();
            }
            else
            {
                navMeshAgent.destination = waypoints[currentWaypoint].position;
                LoseAggro();
            }
        }




        float distanceToTarget = Vector3.Distance(navMeshAgent.destination, transform.position);
        if (distanceToTarget < 2)
        {
            if (isAggro)
            {
                Debug.Log("Player dead");
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
        isAggro = true;
        navMeshAgent.speed = aggroSpeed;
    }

    private void LoseAggro()
    {
        isAggro = false;
        navMeshAgent.speed = normalSpeed;
    }
}
