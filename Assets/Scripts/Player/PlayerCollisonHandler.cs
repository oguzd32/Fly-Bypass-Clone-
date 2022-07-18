using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using static Utilities;

public class PlayerCollisonHandler : MonoBehaviour
{
    // cached components
    private PlayerController controller;
    private PlayerMovement movement;

    // private variables
    private GameObject lastTriggeredObj = default;
    
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        movement = GetComponent<PlayerMovement>();
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

                
                break;

            case "Wing":

                if (otherObj.TryGetComponent(out Wing wing))
                {
                    Debug.Log("collect");
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
                GameManager.Instance.EndGame(true);
                break;
        }
    }
}
