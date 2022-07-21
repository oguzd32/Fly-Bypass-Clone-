using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisonHandler : MonoBehaviour
{
    // cached components
    private EnemyController controller;
    private EnemyMovement movement;

    // private variables
    private GameObject lastTriggeredObj = default;
    
    private void Start()
    {
        controller = GetComponent<EnemyController>();
        movement = GetComponent<EnemyMovement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        // check same object multiple trigger
        if(lastTriggeredObj == otherObj) return;
        lastTriggeredObj = otherObj;
        
        switch (otherObj.tag)
        {
            case "Finish":
                GameManager.Instance.placeCounter++;
                break;
            
            case "Wing":

                if (otherObj.TryGetComponent(out Wing wing))
                {
                    controller.WingCount += 3;
                    wing.Collect();
                }
                
                break;;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObj = collision.gameObject;

        switch (otherObj.tag)
        {
            case "Score":

                movement.enabled = false;
                controller.isFinish = true;
                controller.SetVictoryAnim();
                break;
        }
    }
}
