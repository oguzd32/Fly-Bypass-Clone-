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
    private int place = 0;
    
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

                _GameManager.placeCounter++;
                place = _GameManager.placeCounter;
                movement.inFinal = true;
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
            case "Ground":

                if (transform.position.y < 0)
                {
                    movement.SetMaxForwardSpeed(0);
                }
                break;;
            
            case "Score":

                if(controller.isFinish) return;

                movement.enabled = false;
                controller.isFinish = true;
                controller.SetVictoryAnim();
                UIManager.Instance.SetPlaceText(place);

                if (otherObj.transform.parent.TryGetComponent(out Score score))
                {
                    StartCoroutine(UIManager.Instance.SpawnCoin(transform.position, score.scoreAmount));
                }

                GameManager.Instance.EndGame(true);
                break;
        }
    }
}
