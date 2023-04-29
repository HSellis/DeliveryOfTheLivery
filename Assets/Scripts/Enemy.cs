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

    public Transform target;
    public LayerMask terrainMask;

    public Transform[] waypoints;
    private int currentWaypoint = 0;

    private void Start()
    {
        
    }

    void Update()
    {
        // Find the player
        if (target == null)
        {
            GameObject targetObject = GameObject.FindGameObjectWithTag("Player");
            if (targetObject != null)
            {
                target = targetObject.transform;
            }
        }

        // Check if the player is within the FOV
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            Debug.Log(angle);
            Debug.DrawRay(transform.position, directionToTarget, UnityEngine.Color.red);
            if (angle < viewAngle / 2)
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, terrainMask))
                {
                    Debug.Log("In FOV");
                }
            }
        }


        // Move towards next waypoint
        Vector3 waypointPos = waypoints[currentWaypoint].position;
        Vector3 directionToWaypoint = waypointPos - transform.position;
        float waypointAngle = Vector3.Angle(transform.forward, directionToWaypoint);
        if (waypointAngle > 2)
        {

            //create the rotation we need to be in to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(directionToWaypoint.normalized);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypointPos, moveSpeed * Time.deltaTime);
        }


        float distanceToWaypoint = Vector3.Distance(waypointPos, transform.position);
        if (distanceToWaypoint < 1)
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Length) currentWaypoint = 0;
        }

    }
}
