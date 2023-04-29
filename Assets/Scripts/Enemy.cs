using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float viewAngle = 90f;
    public float viewDistance = 10f;
    public float moveSpeed = 1;
    public float rotationSpeed = 1;

    public LayerMask terrainMask;
    public Transform[] waypoints;

    private Transform playerTrans;
    private Transform target;
    private int currentWaypoint = 0;
    private bool isAggro = false;

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player Center").transform;
        target = playerTrans;
    }

    void Update()
    {
        // Check if player is in FOV
        Vector3 directionToPlayer = playerTrans.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        Debug.Log(angle);
        Debug.DrawRay(transform.position, directionToPlayer, UnityEngine.Color.red);
        if (angle < viewAngle / 2)
        {

            float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, terrainMask))
            {
                Debug.Log("Player in FOV");
                target = playerTrans;
                isAggro = true;
            }
            else
            {
                target = waypoints[currentWaypoint];
                isAggro = false;
            }
        }


        // Move towards target
        Vector3 directionToTarget = target.position - transform.position;
        float waypointAngle = Vector3.Angle(transform.forward, directionToTarget);
        if (waypointAngle > 2)
        {

            //create the rotation we need to be in to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget.normalized);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }


        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget < 1)
        {
            if (isAggro)
            {
                Debug.Log("Player dead");
            }
            else
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length) currentWaypoint = 0;
                target = waypoints[currentWaypoint];
            }
            
        }

    }
}
