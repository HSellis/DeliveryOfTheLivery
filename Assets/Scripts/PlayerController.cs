using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Enemy closeEnemy;
    private ThirdPersonController thirdPersonController;

    public int liveriesStolen = 0;

    public Transform Chest;

    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.UpdateLiveriesStolen(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closeEnemy != null && closeEnemy.state != 1)
            {
                AttemptStealLivery(closeEnemy);
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

    private void AttemptStealLivery(Enemy enemy)
    {
        GameObject newLiveryPrefab = enemy.Livery;
        if (newLiveryPrefab != null)
        {
            enemy.StealLivery();
            GameObject newLivery = Instantiate(newLiveryPrefab, Chest);
            float liveryScale = newLivery.transform.localScale.x * (1 + liveriesStolen * 0.25f);
            newLivery.transform.localScale = Vector3.one * liveryScale * 2;
            newLivery.transform.DOScale(liveryScale, 0.5f);

            liveriesStolen++;
            thirdPersonController.MoveSpeed *= 0.9f;
            thirdPersonController.SprintSpeed *= 0.9f;
            thirdPersonController.JumpHeight *= 0.9f;

            gameController.UpdateLiveriesStolen(liveriesStolen);
        }
        
    }

}
