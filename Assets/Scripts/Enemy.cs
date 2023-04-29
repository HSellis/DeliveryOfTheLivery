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

    public Transform target;
    public LayerMask terrainMask;

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
    }
}
