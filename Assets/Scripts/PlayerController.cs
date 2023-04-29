using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Enemy closeEnemy;
    private ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closeEnemy != null)
            {
                StealLivery(closeEnemy);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            closeEnemy = enemy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == closeEnemy)
        {
            closeEnemy = null;
        }
    }

    private void StealLivery(Enemy enemy)
    {
        enemy.StealLivery();
        thirdPersonController.MoveSpeed *= 0.9f;
        thirdPersonController.SprintSpeed *= 0.9f;
    }

}
