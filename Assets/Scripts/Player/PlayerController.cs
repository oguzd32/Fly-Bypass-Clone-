using System;
using UnityEngine;
using  DG.Tweening;
using static Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private LayerMask layerMask;
    public float WingCount
    {
        get { return wingCount;}
        set { wingCount = value; }
    }

    public bool isFinish { get; set; } = false;
    
    // cached components
    private PlayerMovement movement;
    private Rigidbody m_Rigidbody;

    // private variables
    private float wingCount = 0;
    private bool isFly = false;
    
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        m_Rigidbody = GetComponent<Rigidbody>();
        
        wingCount = 4;
    }

    internal void StartGame()
    {
        movement.StartGame();
        characterAnimator.SetBool("isStarted", true);
    }

    private void Update()
    {
        if(isFinish){return;}
        SetFlyAnimation(!IsGrounded());

        if (Input.GetMouseButton(0) && WingCount > 0 && isFly)
        {
            WingCount = Mathf.Max(0, WingCount - Time.deltaTime);
            m_Rigidbody.AddRelativeForce(Vector3.up * 5);
        }
        
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + Vector3.up * .1f, transform.TransformDirection(Vector3.down), out hit, .5f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
            isFly = false;
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
            return false;
        }
    }

    public void SetVictoryAnim() => characterAnimator.SetTrigger("Finish");
    
    public void SetFlyAnimation(bool value)
    {
        characterAnimator.SetBool("OnGround", !value);
        if (value && !isFly)
        {
            isFly = true;
            transform.DOMoveY(2, 1f);
        }
    }
}
