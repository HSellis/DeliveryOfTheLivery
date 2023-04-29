using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Enemy closeEnemy;
    private ThirdPersonController thirdPersonController;

    public int liveriesStolen = 0;

    public GameObject Score;
    TextMeshProUGUI counter_text;

    public Transform Chest;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        counter_text = Score.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        counter_text.text = liveriesStolen.ToString();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closeEnemy != null && !closeEnemy.isAggro)
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
        GameObject newLivery = enemy.Clothing;
        if (newLivery != null)
        {
            liveriesStolen++;

            newLivery.transform.parent = Chest;
            newLivery.transform.localRotation = Quaternion.identity;
            newLivery.transform.localPosition = new Vector3(0, -1.9f, 0);
            float liveryScale = 1 + liveriesStolen * 0.1f;
            newLivery.transform.localScale = new Vector3(liveryScale, liveryScale, liveryScale);
            enemy.StealLivery();

            thirdPersonController.MoveSpeed *= 0.9f;
            thirdPersonController.SprintSpeed *= 0.9f;
            thirdPersonController.JumpHeight *= 0.9f;
            Debug.Log("Stolen! You now have "+liveriesStolen+" liveries and your speends are now as follows: speed: "+thirdPersonController.MoveSpeed+" sprint: "+thirdPersonController.SprintSpeed+" jump height: "+thirdPersonController.JumpHeight);
        }
        
    }

}
